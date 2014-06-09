using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using Telerik.Windows.Input;
using Telerik.Windows.Controls;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class OneUserRoleAuth : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        public event EventHandler DeleteButtonClicked;
        public List<AspUser> AspUsers { get; set; }
        public List<AspRole> AspRoles { get; set; }
        public List<SiteGroup> SiteGroups { get; set; }
        public UserRoleAuth UserRoleAuthData { get; set; }
        public bool IsUserView { get; set; }
        public bool IsFirstLoad { get; set; }
        public string ValidationMessage { get; set; }

        public OneUserRoleAuth()
        {
            InitializeComponent();
            uiSiteGroups.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(uiSiteGroups_SelectionChanged);
            btnDelete.Click += new RoutedEventHandler(btnDelete_Click);
            chkWholeOrg.Checked += new RoutedEventHandler(chkWholeOrg_Checked);
        }

        void chkWholeOrg_Checked(object sender, RoutedEventArgs e)
        {
            if (chkWholeOrg.IsChecked == true)
            {
                uiSiteGroups.SelectedIndex = 0;
            }
        }

        void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteButtonClicked != null)
                DeleteButtonClicked(this, null);
        }

        public void RebindCombobox()
        {
            IsFirstLoad = true;
            Globals.IsBusy = true;
            if (this.IsUserView)
            {
                uiRoles.DisplayMemberPath = "RoleName";
                uiRoles.SelectedValuePath = "RoleId";
                uiRoles.ItemsSource = this.AspRoles;
                if (this.UserRoleAuthData != null && this.AspRoles != null && this.UserRoleAuthData.RoleId != Guid.Empty)
                {
                    if (this.AspRoles.Count(i => i.RoleId == this.UserRoleAuthData.RoleId) > 0)
                    {
                        uiRoles.SelectedValue = this.UserRoleAuthData.RoleId;
                        uiRoles.Visibility = System.Windows.Visibility.Visible;
                        txtRoleName.Visibility = System.Windows.Visibility.Collapsed;
                        this.IsEnabled = true;
                    }
                    else
                    {
                        //uiRoles.SelectedIndex = 0;
                        txtRoleName.Text = this.UserRoleAuthData.RoleName;
                        uiRoles.Visibility = System.Windows.Visibility.Collapsed;
                        txtRoleName.Visibility = System.Windows.Visibility.Visible;
                        this.IsEnabled = false;
                    }
                }
                else
                {
                    uiRoles.SelectedIndex = 0;
                    uiRoles.Visibility = System.Windows.Visibility.Visible;
                    txtRoleName.Visibility = System.Windows.Visibility.Collapsed;
                    this.IsEnabled = true;
                }
            }
            else
            {
                uiRoles.DisplayMemberPath = "DisplayName";
                uiRoles.SelectedValuePath = "UserId";
                uiRoles.ItemsSource = this.AspUsers;
                if (this.UserRoleAuthData != null && this.AspUsers != null
                    && this.AspUsers.Count(i => i.UserId == this.UserRoleAuthData.UserId) > 0)
                {
                    uiRoles.SelectedValue = this.UserRoleAuthData.UserId;
                }
                else
                {
                    uiRoles.SelectedIndex = 0;
                }
            }

            CheckSiteGroupLevelSecurity();

            uiSiteGroups.ItemsSource = this.SiteGroups;
            if (this.UserRoleAuthData != null && this.UserRoleAuthData.SiteGroupId > 0)
            {
                if (this.SiteGroups != null && this.SiteGroups.Count(i => i.SiteGroupId == this.UserRoleAuthData.SiteGroupId) > 0)
                {
                    uiSiteGroups.SelectedValue = this.UserRoleAuthData.SiteGroupId;
                }
                else
                {
                    if (IsFirstLoad)
                    {
                        //this login user are not allowed to see the role of other groups. So, hide this record.
                        this.Visibility = System.Windows.Visibility.Collapsed;                        
                    }
                    else
                    {
                        uiSiteGroups.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                uiSiteGroups.SelectedIndex = 0;
            }

            if (this.UserRoleAuthData != null)
            {
                chkWholeOrg.IsChecked = this.UserRoleAuthData.WholeOrg == true;
            }

            if (!this.UserRoleAuthData.SiteId.HasValue)
            {
                Globals.IsBusy = false;
                IsFirstLoad = false;
            }

        }

        void CheckSiteGroupLevelSecurity()
        {
            var auths = Globals.UserLogin.UserRoleAuths.Where(i => i.RoleId == SecurityHelper.SecurityAdminRoleId);
            if (auths.Count(i => !i.SiteGroupId.HasValue || i.WholeOrg == true) > 0)
            {
                chkWholeOrg.IsEnabled = uiSiteGroups.IsEnabled = true;
            }
            else
            {
                this.SiteGroups = (from s in this.SiteGroups
                                   join n in auths on s.SiteGroupId equals n.SiteGroupId
                                   select s).Distinct().ToList();
                chkWholeOrg.IsEnabled = uiSiteGroups.IsEnabled = this.SiteGroups.Count > 1;                
            }
        }

        void uiSiteGroups_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (uiSiteGroups.SelectedItem != null && Convert.ToInt32(uiSiteGroups.SelectedValue) > 0)
            {
                SiteGroup selectedSiteGroup = (SiteGroup)uiSiteGroups.SelectedItem;
                chkWholeOrg.IsChecked = false;
                List<Site> siteList = new List<Site>();
                siteList.Add(new Site());                
                if (selectedSiteGroup.Sites != null)
                {
                    siteList.AddRange(selectedSiteGroup.Sites);
                }

                //Check site security :
                siteList = CheckSiteLevelSecurity(siteList);

                uiSites.ItemsSource = siteList;
                if (this.UserRoleAuthData != null && this.UserRoleAuthData.SiteId > 0)
                {
                    if (siteList.Count(i => i.SiteId == this.UserRoleAuthData.SiteId) > 0)
                    {
                        uiSites.SelectedValue = this.UserRoleAuthData.SiteId;
                    }
                    else
                    {
                        if (IsFirstLoad)
                        {
                            //this login user are not allowed to see the role of other sites. So, hide this record.
                            this.Visibility = System.Windows.Visibility.Collapsed;                            
                        }
                        else
                        {
                            uiSites.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    uiSites.SelectedIndex = 0;
                }
            }
            else
            {
                uiSites.ItemsSource = null;
            }

        }

        List<Site> CheckSiteLevelSecurity(List<Site> siteList)
        {
            var auths = Globals.UserLogin.UserRoleAuths.Where(i => i.RoleId == SecurityHelper.SecurityAdminRoleId);
            if (auths.Count(i => !i.SiteId.HasValue) > 0)
            {
                uiSites.IsEnabled = true;
            }
            else
            {
                siteList = (from s in siteList
                            join n in auths on s.SiteId equals n.SiteId
                            select s).Distinct().ToList();
                uiSites.IsEnabled = siteList.Count > 1;
            }
            return siteList;
        }

        public void PrepareSaveData()
        {            
            if (this.UserRoleAuthData == null)
            {
                this.UserRoleAuthData = new UserRoleAuth();                
            }
            if(!this.UserRoleAuthData.NullableRecordId.HasValue)
                this.UserRoleAuthData.CreatedBy = Globals.UserLogin.UserName;
            this.UserRoleAuthData.UpdatedBy = Globals.UserLogin.UserName;

            if(this.IsUserView)
                this.UserRoleAuthData.RoleId = Guid.Parse(uiRoles.SelectedValue.ToString());
            else
                this.UserRoleAuthData.UserId = Guid.Parse(uiRoles.SelectedValue.ToString());

            this.UserRoleAuthData.WholeOrg = chkWholeOrg.IsChecked;

            if (uiSiteGroups.SelectedValue != null && Convert.ToInt32(uiSiteGroups.SelectedValue) > 0)
                this.UserRoleAuthData.SiteGroupId = Convert.ToInt32(uiSiteGroups.SelectedValue);
            else
                this.UserRoleAuthData.SiteGroupId = null;

            if (uiSites.SelectedValue != null && Convert.ToInt32(uiSites.SelectedValue) > 0)
                this.UserRoleAuthData.SiteId = Convert.ToInt32(uiSites.SelectedValue);
            else
                this.UserRoleAuthData.SiteId = null;

            this.UserRoleAuthData.IsChanged = true;

            //Check validation :
            ValidationMessage = string.Empty;
            
        }
    }
}
