using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ServiceModel;
using common = Mimosa.Apartment.Common;
using Mimosa.Apartment.Common;
using Mimosa.Apartment.Silverlight.UI.ApartmentService;

namespace Mimosa.Apartment.Silverlight.UI
{
    internal static class MembershipServiceHelper
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal delegate void EmptyCallback();
        //internal delegate void SelectUserSiteIdCallback(int userSiteId);
        internal delegate void SelectIsUserOrgAdminCallback(bool userIsOrgAdmin);
        internal delegate void SelectUserOrganisationIdCallback(int userOrganisationId);



        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        private static Dictionary<System.Guid, Delegate> _callbacks = new Dictionary<Guid, Delegate>();
        private static Dictionary<System.Guid, Object[]> _params = new Dictionary<Guid, Object[]>();

        /*  ======================================================================            
		 *      EVENT HANDLERS
		 *  ====================================================================== */
        private static ApartmentServiceClient GetProxy()
        {
            return GetProxy(Guid.Empty, null);
        }

        private static ApartmentServiceClient GetProxy(Guid callerKey, Delegate callback)
        {
            ApartmentServiceClient result = new ApartmentServiceClient();
            // Rely on the endpoint addresses being setup within ServiceReferences.ClientConfig instead.
            // result.Endpoint.Address = new EndpointAddress(Globals.EndPointAddresses.DataService);            
            if (callback != null)
            {
                _callbacks.Add(callerKey, callback);
            }

            return result;
        }

		
        /*  ======================================================================            
         *      PAGE FUNCTIONS
         *  ====================================================================== */        
        //GetUserLogin
        internal delegate void GetUserLoginCallBack(UserLogin itemSource);
        internal static void GetUserLoginAsync(GetUserLoginCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetUserLoginCompleted += new EventHandler<GetUserLoginCompletedEventArgs>(proxy_GetUserLoginCompleted);
            proxy.GetUserLoginAsync(callerKey);
        }

        static void proxy_GetUserLoginCompleted(object sender, GetUserLoginCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                UserLogin itemSource = e.Result;
                ((GetUserLoginCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //Get AspUser
        internal delegate void GetAspUserCallBack(common.AspUser itemSource);
        internal static void GetAspUserAsync(Guid userId, GetAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetAspUserCompleted += new EventHandler<GetAspUserCompletedEventArgs>(proxy_GetAspUserCompleted);
            proxy.GetAspUserAsync(userId, callerKey);
        }

        static void proxy_GetAspUserCompleted(object sender, GetAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                common.AspUser itemSource = e.Result;
                ((GetAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }
        
        //Save AspUser - new (18-Nov-2011)
        internal delegate void SaveAspUserCallBack(common.AspUser result);
        internal static void SaveAspUserAsync(common.AspUser saveUser, SaveAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveAspUserCompleted += new EventHandler<SaveAspUserCompletedEventArgs>(proxy_SaveAspUserCompleted);
            proxy.SaveAspUserAsync(saveUser, callerKey);
        }

        static void proxy_SaveAspUserCompleted(object sender, SaveAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                common.AspUser result = e.Result;
                ((SaveAspUserCallBack)_callbacks[callerKey]).Invoke(result);

                _callbacks.Remove(callerKey);
            }
        }

        //GetAppSettings
        internal delegate void GetAppSettingsCallBack(AppSettings itemSource);
        internal static void GetAppSettingsAsync(GetAppSettingsCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetAppSettingsCompleted += new EventHandler<GetAppSettingsCompletedEventArgs>(proxy_GetAppSettingsCompleted);
            proxy.GetAppSettingsAsync(callerKey);
        }

        static void proxy_GetAppSettingsCompleted(object sender, GetAppSettingsCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                AppSettings itemSource = e.Result;
                ((GetAppSettingsCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }


        //GetGlobalVariableAsync
        internal delegate void GetGlobalVariableCallBack(GlobalVariable itemSource);
        internal static void GetGlobalVariableAsync(GetGlobalVariableCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetGlobalVariableCompleted += new EventHandler<GetGlobalVariableCompletedEventArgs>(proxy_GetGlobalVariableCompleted);
            proxy.GetGlobalVariableAsync(callerKey);
        }

        static void proxy_GetGlobalVariableCompleted(object sender, GetGlobalVariableCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                GlobalVariable itemSource = e.Result;
                ((GetGlobalVariableCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ChangePasswordAsync
        internal delegate void ChangePasswordCallback(int status);
        internal static void ChangePasswordAsync(string oldPassword, string newPassword, ChangePasswordCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ChangePasswordCompleted += new EventHandler<ChangePasswordCompletedEventArgs>(proxy_ChangePasswordCompleted);
            proxy.ChangePasswordAsync(oldPassword, newPassword, callerKey);
        }

        static void proxy_ChangePasswordCompleted(object sender, ChangePasswordCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                int status = e.Result;

                ((ChangePasswordCallback)_callbacks[callerKey]).Invoke(status);

                _callbacks.Remove(callerKey);
            }
        }

        #region Security
        //// ListComponent
        //internal delegate void ListComponentCallBack(List<common.Component> itemSource);
        //internal static void ListComponentAsync(int? componentId, ListComponentCallBack callback)
        //{
        //    Guid callerKey = Guid.NewGuid();

        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.ListComponentCompleted += new EventHandler<ListComponentCompletedEventArgs>(proxy_ListComponentCompleted);
        //    proxy.ListComponentAsync(componentId, callerKey);
        //}

        //static void proxy_ListComponentCompleted(object sender, ListComponentCompletedEventArgs e)
        //{
        //    Guid callerKey = (Guid)e.UserState;
        //    if (_callbacks.ContainsKey(callerKey))
        //    {
        //        List<common.Component> itemSource = e.Result;
        //        ((ListComponentCallBack)_callbacks[callerKey]).Invoke(itemSource);

        //        _callbacks.Remove(callerKey);
        //    }
        //}

        //// ListRoleComponentPermission
        //internal delegate void ListRoleComponentPermissionCallBack(List<common.RoleComponentPermission> itemSource);
        //internal static void ListRoleComponentPermissionAsync(Guid? roleId, int? componentId, ListRoleComponentPermissionCallBack callback)
        //{
        //    Guid callerKey = Guid.NewGuid();

        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.ListRoleComponentPermissionCompleted += new EventHandler<ListRoleComponentPermissionCompletedEventArgs>(proxy_ListRoleComponentPermissionCompleted);
        //    proxy.ListRoleComponentPermissionAsync(roleId, componentId, callerKey);
        //}

        //static void proxy_ListRoleComponentPermissionCompleted(object sender, ListRoleComponentPermissionCompletedEventArgs e)
        //{
        //    Guid callerKey = (Guid)e.UserState;
        //    if (_callbacks.ContainsKey(callerKey))
        //    {
        //        List<common.RoleComponentPermission> itemSource = e.Result;
        //        ((ListRoleComponentPermissionCallBack)_callbacks[callerKey]).Invoke(itemSource);

        //        _callbacks.Remove(callerKey);
        //    }
        //}

        //internal static void SaveRoleComponentPermissionAsync(List<common.RoleComponentPermission> saveList, EmptyCallback callback)
        //{
        //    Guid callerKey = Guid.NewGuid();
        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.SaveRoleComponentPermissionCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
        //    proxy.SaveRoleComponentPermissionAsync(saveList, callerKey);
        //}

        //// ListAspRole
        //internal delegate void ListAspRoleCallBack(List<common.AspRole> itemSource);
        //internal static void ListAspRoleAsync(Guid? roleId, bool? isCustom, ListAspRoleCallBack callback)
        //{
        //    Guid callerKey = Guid.NewGuid();

        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.ListAspRoleCompleted += new EventHandler<ListAspRoleCompletedEventArgs>(proxy_ListAspRoleCompleted);
        //    proxy.ListAspRoleAsync(roleId, isCustom, callerKey);
        //}

        //static void proxy_ListAspRoleCompleted(object sender, ListAspRoleCompletedEventArgs e)
        //{
        //    Guid callerKey = (Guid)e.UserState;
        //    if (_callbacks.ContainsKey(callerKey))
        //    {
        //        List<common.AspRole> itemSource = e.Result;
        //        ((ListAspRoleCallBack)_callbacks[callerKey]).Invoke(itemSource);

        //        _callbacks.Remove(callerKey);
        //    }
        //}

        //internal static void SaveAspRoleAsync(List<common.AspRole> saveList, string currentUser, EmptyCallback callback)
        //{
        //    Guid callerKey = Guid.NewGuid();
        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.SaveAspRoleCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
        //    proxy.SaveAspRoleAsync(saveList, currentUser, callerKey);
        //}

        //// ListUserRoleAuth
        //internal delegate void ListUserRoleAuthCallBack(List<common.UserRoleAuth> itemSource);
        //internal static void ListUserRoleAuthAsync(int? orgId, Guid? userId, Guid? roleId, ListUserRoleAuthCallBack callback)
        //{
        //    Guid callerKey = Guid.NewGuid();

        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.ListUserRoleAuthCompleted += new EventHandler<ListUserRoleAuthCompletedEventArgs>(proxy_ListUserRoleAuthCompleted);
        //    proxy.ListUserRoleAuthAsync(orgId, userId, roleId, callerKey);
        //}

        //static void proxy_ListUserRoleAuthCompleted(object sender, ListUserRoleAuthCompletedEventArgs e)
        //{
        //    Guid callerKey = (Guid)e.UserState;
        //    if (_callbacks.ContainsKey(callerKey))
        //    {
        //        List<common.UserRoleAuth> itemSource = e.Result;
        //        ((ListUserRoleAuthCallBack)_callbacks[callerKey]).Invoke(itemSource);

        //        _callbacks.Remove(callerKey);
        //    }
        //}

        //internal static void SaveUserRoleAuthAsync(List<common.UserRoleAuth> saveList, EmptyCallback callback)
        //{
        //    Guid callerKey = Guid.NewGuid();
        //    ApartmentServiceClient proxy = GetProxy(callerKey, callback);
        //    proxy.SaveUserRoleAuthCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
        //    proxy.SaveUserRoleAuthAsync(saveList, callerKey);
        //}

       

        // ListAspUser
        internal delegate void ListAspUserCallBack(List<common.AspUser> itemSource);
        internal static void ListAspUserAsync(int? orgId, Guid? userId, bool? isLegacy, bool loadCentreId, ListAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListAspUserCompleted += new EventHandler<ListAspUserCompletedEventArgs>(proxy_ListAspUserCompleted);
            proxy.ListAspUserAsync(orgId, userId, isLegacy, callerKey);
        }

        static void proxy_ListAspUserCompleted(object sender, ListAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<common.AspUser> itemSource = e.Result;
                ((ListAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListOrgAdminAspUser
        internal delegate void ListOrgAdminAspUserCallBack(List<common.AspUser> itemSource);
        internal static void ListOrgAdminAspUserAsync(int orgId, Guid roleid, ListOrgAdminAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListOrgAdminAspUserCompleted += new EventHandler<ListOrgAdminAspUserCompletedEventArgs>(proxy_ListOrgAdminAspUserCompleted);
            proxy.ListOrgAdminAspUserAsync(orgId, roleid, callerKey);
        }

        static void proxy_ListOrgAdminAspUserCompleted(object sender, ListOrgAdminAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<common.AspUser> itemSource = e.Result;
                ((ListOrgAdminAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // UnlockAspUser
        internal delegate void UnlockAspUserCallBack(common.AspUser oldUser);
        internal static void UnlockAspUserAsync(common.AspUser oldUser, UnlockAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.UnlockAspUserCompleted +=new EventHandler<UnlockAspUserCompletedEventArgs>(proxy_UnlockAspUserCompleted);
            proxy.UnlockAspUserAsync(oldUser, callerKey);
        }

        static void proxy_UnlockAspUserCompleted(object sender, UnlockAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                common.AspUser itemSource = e.Result;
                ((UnlockAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }
        #endregion

    }
}
