using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Mimosa.Apartment.Silverlight.UI;
using System.Windows.Browser;
using System.Collections.Generic;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using System.Windows;
using common = Mimosa.Apartment.Common;
using Mimosa.Apartment.Silverlight.UI.ApartmentService;

namespace Mimosa.Apartment.Silverlight.UI
{
    public static class SecurityHelper
    {
        public static Guid PortalAdministratorRoleId { get { return new Guid("0FEA5EAE-C34F-4E6B-817F-8ABA46697F19"); } }
        public static Guid OrganisationAdministratorRoleId { get { return new Guid("0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB"); } }
        public static Guid SiteAdministratorRoleId { get { return new Guid("4030222F-1655-42D7-9C2A-4278E105228C"); } }
        public static Guid SecurityAdminRoleId { get { return new Guid("FC788FA1-367C-4433-86B3-B88137E9C11F"); } }
        
        internal static bool IsBuildInRole(Guid roleId)
        {
            bool isBuildInRoles = roleId == SecurityHelper.PortalAdministratorRoleId
                                || roleId == SecurityHelper.OrganisationAdministratorRoleId
                                || roleId == SecurityHelper.SiteAdministratorRoleId
                                || roleId == SecurityHelper.SecurityAdminRoleId;
            return isBuildInRoles;
        }        

        internal static bool HasRole(List<common.UserRoleAuth> userRoleAuths, Guid roleId)
        {
            return userRoleAuths != null && userRoleAuths.Count(i => i.RoleId.Equals(roleId)) > 0;
        }

        internal static Information GetNoPermissionInfoPanel()
        {
            Information infoPanel = new Information();
            infoPanel.Height = 30;
            infoPanel.InfoMessage = Globals.UserMessages.NoPermission;
            return infoPanel;
        }

        internal static List<common.UserRoleAuth> GetSupervisorUserRoleAuths(List<common.UserRoleAuth> oldList)
        {
            if (oldList != null)
            {
                return oldList.Where(i => i.RoleId == SecurityHelper.OrganisationAdministratorRoleId
                                    || i.RoleId == SecurityHelper.SiteAdministratorRoleId).ToList();
            }
            else
            {
                return oldList;
            }
        }

        internal static List<common.UserRoleAuth> GetUserRoleAuths(int componentId) 
        {
            List<common.UserRoleAuth> result = null;
            if (Globals.UserLogin.UserRoleAuths != null && Globals.UserLogin.RoleComponentPermissions != null)
            {
                var list = (from rcp in Globals.UserLogin.RoleComponentPermissions
                          join ura in Globals.UserLogin.UserRoleAuths on rcp.RoleId equals ura.RoleId
                          where rcp.ComponentId == componentId
                          select ura);
                foreach (common.UserRoleAuth userRole in list)
                {
                    userRole.WriteRight = Globals.UserLogin.RoleComponentPermissions.Count(i => i.RoleId == userRole.RoleId
                                                && i.ComponentId == componentId && i.WriteRight == true) > 0;
                }
                if (list.Count() > 0)
                    result = list.ToList();
               
            }
            //If not have license to this component : 
            if (!HasLicenseComponentModule(componentId))
                return null;
            return result;
        }

        internal static bool HasLicenseComponentModule(int componentId)
        {
            bool result = true;
                
            return result;
        }

        internal static void LoadGlobalVariable()
        {
            MembershipServiceHelper.GetGlobalVariableAsync(GetGlobalVariableCompleted);
        }

        internal static void GetGlobalVariableCompleted(GlobalVariable itemsSource)
        {
            if (itemsSource == null)
            {
                DataServiceHelper.SelectSessionId();
            }
            else
            {
                Globals.UserLogin = itemsSource.UserLogin;
                Globals.AppSettings = itemsSource.AppSettings;
                Globals.ApplicationCurrentDateTime = itemsSource.ApplicationCurrentDateTime;

                //Security section
                Globals.UserLogin.IsUserOrganisationAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.OrganisationAdministratorRoleId);
                Globals.UserLogin.IsUserSiteAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.SiteAdministratorRoleId);
                Globals.UserLogin.IsUserPortalAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.PortalAdministratorRoleId);
                Globals.UserLogin.IsUserSecurityAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.SecurityAdminRoleId);
                                                
            }
        }

        internal static List<common.AspUser> FilterUserList(List<common.AspUser> userList, List<common.SiteGroup> allSiteGroups)
        {
            List<common.AspUser> result = new List<common.AspUser>();
            if (Globals.UserLogin.UserRoleAuths != null && Globals.UserLogin.UserRoleAuths.Count > 0
               && Globals.UserLogin.IsUserSecurityAdministrator)
            {
                var auths = Globals.UserLogin.UserRoleAuths.Where(i => i.RoleId == SecurityHelper.SecurityAdminRoleId);
                foreach (common.AspUser user in userList)
                {
                    if (!result.Contains(user))
                    {
                        foreach (common.UserRoleAuth auth in auths)
                        {
                            if (!auth.SiteGroupId.HasValue && !auth.SiteId.HasValue && !auth.DepartmentId.HasValue
                                || auth.SiteGroupId.HasValue && !auth.SiteId.HasValue && !auth.DepartmentId.HasValue && SiteInSiteGroup(user.SiteId.Value, auth.SiteGroupId.Value, allSiteGroups)
                                || auth.SiteId.HasValue && !auth.DepartmentId.HasValue && user.SiteId == auth.SiteId
                                || (!auth.SiteId.HasValue || user.SiteId == auth.SiteId) && auth.DepartmentId.HasValue && !auth.RosterCentreId.HasValue && user.DepartmentId == auth.DepartmentId
                                || (!auth.SiteId.HasValue || user.SiteId == auth.SiteId) && auth.RosterCentreId.HasValue && !auth.CentreId.HasValue && user.RosterCentreId == auth.RosterCentreId
                                || (!auth.SiteId.HasValue || user.SiteId == auth.SiteId) && auth.CentreId.HasValue && auth.CentreId == user.CentreId
                                || !user.SiteId.HasValue)
                            {
                                if (user.UserId == Globals.UserLogin.UserUserId || user.MinRoleLevel > Globals.UserLogin.AspUser.MinRoleLevel)
                                {
                                    result.Add(user);
                                    break;
                                }
                            }

                            //with special case : user is Sale manager
                            //if (Globals.UserLogin.IsUserSaleManager)
                            //{
                            //    if (_allSalePersons.Count(i => i.UserId == user.UserId) > 0)
                            //    {
                            //        if (!auth.SiteGroupId.HasValue
                            //                || auth.SiteGroupId.HasValue && !auth.SiteId.HasValue && SiteInSiteGroup(user.SiteId, auth.SiteGroupId.Value)
                            //                || auth.SiteId.HasValue && !auth.DepartmentId.HasValue && user.SiteId == auth.SiteId
                            //                || user.SiteId == auth.SiteId && auth.DepartmentId.HasValue && !auth.RosterCentreId.HasValue && user.DepartmentId == auth.DepartmentId
                            //                || user.SiteId == auth.SiteId && auth.RosterCentreId.HasValue && !auth.CentreId.HasValue && user.RosterCentreId == auth.RosterCentreId
                            //                || user.SiteId == auth.SiteId && auth.CentreId.HasValue && auth.CentreId == user.CentreId)
                            //        {
                            //            added = true;
                            //            break;
                            //        }
                            //    }
                            //}
                        }
                    }
                }                
            }
            return result;
        }

        internal static bool SiteInSiteGroup(int? userSiteId, int siteGroupId, List<common.SiteGroup> allSiteGroup)
        {
            foreach (common.SiteGroup siteGroup in allSiteGroup)
            {
                if (siteGroup.SiteGroupId == siteGroupId)
                {
                    return siteGroup.Sites.Count(i => i.SiteId == userSiteId) > 0;
                }
            }
            return false;
        }

        internal static List<common.AspRole> FilterRoleList(List<common.AspRole> roleList)
        {
            List<common.AspRole> result = new List<common.AspRole>();
            //List<AspRole> portalList = roleList.Where(i => i.RoleId == PortalAdministratorRoleId).ToList();
            //List<AspRole> orgList = roleList.Where(i => i.RoleId == OrganisationAdministratorRoleId).ToList();
            //List<AspRole> siteList = roleList.Where(i => i.RoleId == SiteAdministratorRoleId).ToList();
            List<common.AspRole> securityList = roleList.Where(i => i.RoleId == SecurityAdminRoleId).ToList();
            
            if (Globals.UserLogin.IsUserOrganisationAdministrator)
            {
                result = roleList.Where(i => i.RoleId != OrganisationAdministratorRoleId
                                            && i.RoleId != PortalAdministratorRoleId).ToList();
            }
            else
            {
                if (Globals.UserLogin.IsUserSiteAdministrator)
                {
                    result.AddRange(securityList);
                }
            }
            return result;
        }


        private static Dictionary<Guid, int> RolesLevel = new Dictionary<Guid, int>
            {
               {new Guid("0FEA5EAE-C34F-4E6B-817F-8ABA46697F19"), 1 }, // Portal Admin
               {new Guid("0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB"), 2 }, // Organisation Admin
               {new Guid("4030222F-1655-42D7-9C2A-4278E105228C"), 3 }, // Site Admin
               {new Guid("FC788FA1-367C-4433-86B3-B88137E9C11F"), 4 }, // Security Administrator
            };


        internal static common.UserRoleAuth GetHighestRole(List<common.UserRoleAuth> list)
        {
            common.UserRoleAuth highestRole = null;
            int higheRoleRank = 0;

            list.ForEach(ura =>
                {
                    if (RolesLevel[ura.RoleId] > higheRoleRank)
                    {
                        highestRole = ura;
                        higheRoleRank = RolesLevel[ura.RoleId];
                    }
                });

            return highestRole;
        }

        internal static Guid GetHighestRole(List<Guid> roles)
        {
            Guid highestRoleId = new Guid();
            int highestRoleRank = int.MaxValue;

            roles.ForEach(role =>
                {
                    if (highestRoleRank > RolesLevel[role])
                    {
                        highestRoleId = role;
                        highestRoleRank = RolesLevel[role];
                    }
                });

            return highestRoleId;
        }

        internal static void InitGlobalsValues(GlobalVariable itemsSource)
        {
            Globals.UserLogin = itemsSource.UserLogin;
            Globals.AppSettings = itemsSource.AppSettings;
            
            //Security section
            Globals.UserLogin.IsUserOrganisationAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.OrganisationAdministratorRoleId);
            Globals.UserLogin.IsUserSiteAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.SiteAdministratorRoleId);
            Globals.UserLogin.IsUserPortalAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.PortalAdministratorRoleId);
            Globals.UserLogin.IsUserSecurityAdministrator = SecurityHelper.HasRole(Globals.UserLogin.UserRoleAuths, SecurityHelper.SecurityAdminRoleId);

        }
    }
}
