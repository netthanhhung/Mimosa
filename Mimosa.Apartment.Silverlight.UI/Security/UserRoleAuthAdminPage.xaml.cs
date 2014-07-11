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
    public partial class UserRoleAuthAdminPage : Page, IComponent
    {
        private const int _nbrItemsShowed = 100;

        private List<UserRoleAuth> _deleteList = new List<UserRoleAuth>();
        public List<UserRoleAuth> UserRoleAuths { get; set; }
        private List<AspUser> _aspUsers;
        private List<AspRole> _aspRoles = null;
        private List<SiteGroup> _siteGroup = null;
        private List<Component> _realComponents = null;
        private List<RoleComponentPermission> _roleComponentItemSource = new List<RoleComponentPermission>();
        private int _roleCount = 0;
        public UserRoleAuthAdminPage()
        {
            InitializeComponent();
            UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.UserRoleAuthorisation);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            btnSave.IsEnabled = this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            FillLanguage();

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);

            //uiUsers.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(uiUsers_SelectionChanged);
            btnNew.Click += new RoutedEventHandler(btnNew_Click);

            RebindUserList();

            uiUsers.IsFilteringEnabled = true;
            uiUsers.KeyUp += new KeyEventHandler(uiUsers_KeyUp);
            uiUsers.OpenDropDownOnFocus = true;
            uiUsers.TextSearchMode = TextSearchMode.Contains;
            uiUsers.LostFocus += new RoutedEventHandler(uiUsers_LostFocus);
            
            DataServiceHelper.ListComponentAsync(null, ListComponentCompleted);
        }

        void FillLanguage()
        {
            lblSite.Text = ResourceHelper.GetReourceValue("Common_lblSite");
            lblSiteGroup.Text = ResourceHelper.GetReourceValue("UserRoleAuthAdminPage_lblSiteGroup");
            lblRole.Text = ResourceHelper.GetReourceValue("UserRoleAuthAdminPage_lblRole");
            uiTabComponents.Header = ResourceHelper.GetReourceValue("UserRoleAuthAdminPage_uiTabComponents");
            uiTabRoles.Header = ResourceHelper.GetReourceValue("UserRoleAuthAdminPage_uiTabRoles");
            lblUser.Text = ResourceHelper.GetReourceValue("UserRoleAuthAdminPage_lblUser");
            uiTitle.Text = ResourceHelper.GetReourceValue("UserRoleAuthAdminPage_uiTitle");
            btnSave.Content = ResourceHelper.GetReourceValue("Common_btnSave");
            btnCancel.Content = ResourceHelper.GetReourceValue("Common_btnCancel");
        }

        void ListComponentCompleted(List<Component> comptList)
        {
            _realComponents = comptList;
        }

        void uiUsers_KeyUp(object sender, KeyEventArgs e)
        {
            Key keyCode = e.Key;
            if (keyCode == Key.Tab || keyCode == Key.Enter)
            {
                if (keyCode == Key.Enter)
                {
                    scrollViewerPanels.Focus();
                }
                //RebindUserRoleAuthList();
                
            }
        }

        void uiUsers_LostFocus(object sender, RoutedEventArgs e)
        {
            RebindUserRoleAuthList();
        }

        private void RebindUserList()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListSiteGroupAsync(Globals.UserLogin.UserOrganisationId, null, ListSiteGroupCompleted);              
        }

        void ListSiteGroupCompleted(List<SiteGroup> siteGroupList)
        {
            siteGroupList.Insert(0, new SiteGroup());
            _siteGroup = siteGroupList;
            DataServiceHelper.ListAspUserAsync(Globals.UserLogin.UserOrganisationId, null, false, ListAllAspUserCompleted);
            //DataServiceHelper.ListSiteGroupSiteAsync(null, null, ListSiteGroupSiteCompleted);
        }

        void ListAllAspUserCompleted(List<AspUser> userList)
        {
            _aspUsers = RemoveDuplicateUser(SecurityHelper.FilterUserList(userList, _siteGroup));           
            DataServiceHelper.ListAspRoleAsync(null, ListRoleCompleted);
        }

        List<AspUser> RemoveDuplicateUser(List<AspUser> userList)
        {
            Dictionary<Guid, string> userItemSource = new Dictionary<Guid, string>();            
            List<AspUser> result = new List<AspUser>();
            foreach (AspUser user in userList)
            {
                if (!userItemSource.ContainsKey(user.UserId))
                {
                    userItemSource.Add(user.UserId, user.UserName);
                    result.Add(user);
                }
            }

            return result;
        }

        void ListRoleCompleted(List<AspRole> roleList)
        {
            _aspRoles = SecurityHelper.FilterRoleList(roleList);
            uiUsers.DisplayMemberPath = "DisplayName";
            uiUsers.SelectedValuePath = "UserId";
            uiUsers.ItemsSource = _aspUsers;
            Globals.IsBusy = false;
        }

        void RebindUserRoleAuthList()
        {
            _deleteList.Clear();
            if (uiUsers.SelectedValue != null && Guid.Parse(uiUsers.SelectedValue.ToString()) != Guid.Empty)
            {
                Globals.IsBusy = true;
                Guid userId = Guid.Parse(uiUsers.SelectedValue.ToString());
                    DataServiceHelper.ListUserRoleAuthAsync(Globals.UserLogin.UserOrganisationId, userId, null, ListUserRoleAuthCompleted);                
            }            
        }

        void ListUserRoleAuthCompleted(List<UserRoleAuth> userRoleAuthList)
        {
            uiUserRoleAuthList.Items.Clear();
            //userRoleAuthList = (from u in userRoleAuthList
            //                    join role in _aspRoles on u.RoleId equals role.RoleId
            //                    select u).ToList();
            userRoleAuthList = userRoleAuthList.Take(_nbrItemsShowed).ToList();

            foreach (UserRoleAuth dataItem in userRoleAuthList)
            {
                OneUserRoleAuth control = new OneUserRoleAuth();
                control.UserRoleAuthData = dataItem;
                control.AspRoles = _aspRoles;
                control.AspUsers = _aspUsers;
                control.SiteGroups = _siteGroup;
                control.IsUserView = true;
                control.RebindCombobox();
                control.DeleteButtonClicked += new EventHandler(control_DeleteButtonClicked);
                uiUserRoleAuthList.Items.Add(control);
            }

            _roleComponentItemSource.Clear();
            var roleList = (from i in userRoleAuthList
                            select i.RoleId).Distinct();
            _roleCount = roleList.Count();
            foreach (Guid roleId in roleList)
            {
                DataServiceHelper.ListRoleComponentPermissionAsync(roleId, null, ListRoleComponentCompleted);
            }

            Globals.IsBusy = false;
        }

        void ListRoleComponentCompleted(List<RoleComponentPermission> roleComptList)
        {
            _roleComponentItemSource.AddRange(roleComptList);
            _roleCount--;
            if (_roleCount == 0 && _roleComponentItemSource.Count > 0)
            {
                ucContextComponentTree.RealComponents = _realComponents;
                ucContextComponentTree.RoleComponentItemSource = _roleComponentItemSource;
                ucContextComponentTree.RoleId = _roleComponentItemSource.First().RoleId;
                ucContextComponentTree.IsReadonly = true;
                ucContextComponentTree.BuildTreeView();
            }
            
        }

        private bool SiteInSiteGroup(int? userSiteId, int siteGroupId)
        {
            foreach (SiteGroup siteGroup in _siteGroup)
            {
                if (siteGroup.SiteGroupId == siteGroupId)
                {
                    return siteGroup.Sites.Count(i => i.SiteId == userSiteId) > 0;
                }
            }
            return false;
        }

        void control_DeleteButtonClicked(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(ResourceHelper.GetReourceValue("Common_ConfirmDeleteNoParam"), ResourceHelper.GetReourceValue("Common_ConfirmationRequired"), MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                OneUserRoleAuth deletedItem = sender as OneUserRoleAuth;
                if (deletedItem != null)
                {
                    if (deletedItem.UserRoleAuthData != null && deletedItem.UserRoleAuthData.UserRoleAuthId > 0)
                    {
                        deletedItem.UserRoleAuthData.IsDeleted = true;
                        _deleteList.Add(deletedItem.UserRoleAuthData);
                    }
                    uiUserRoleAuthList.Items.Remove(deletedItem);
                }
            }
        }

        void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (uiUsers.SelectedValue != null && Guid.Parse(uiUsers.SelectedValue.ToString()) != Guid.Empty)
            {
                OneUserRoleAuth control = new OneUserRoleAuth();
                control.UserRoleAuthData = new UserRoleAuth();
                control.UserRoleAuthData.UserId = Guid.Parse(uiUsers.SelectedValue.ToString());
                control.AspRoles = _aspRoles;
                control.AspUsers = _aspUsers;
                control.SiteGroups = _siteGroup;
                control.IsUserView = true;

                control.RebindCombobox();
                control.DeleteButtonClicked += new EventHandler(control_DeleteButtonClicked);
                uiUserRoleAuthList.Items.Add(control);
            }

        }
        
        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RebindUserRoleAuthList();
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<UserRoleAuth> saveList = new List<UserRoleAuth>();
            foreach (OneUserRoleAuth control in uiUserRoleAuthList.Items)
            {
                if (control.IsEnabled)
                {
                    control.PrepareSaveData();
                    if (!string.IsNullOrEmpty(control.ValidationMessage))
                    {
                        MessageBox.Show(control.ValidationMessage, ResourceHelper.GetReourceValue("Common_ValidationError"), MessageBoxButton.OK);
                        return;
                    }
                    saveList.Add(control.UserRoleAuthData);
                }
            }
            saveList.AddRange(_deleteList);
            DataServiceHelper.SaveUserRoleAuthAsync(saveList, SaveUserRoleAuthCompleted);
        }

        void SaveUserRoleAuthCompleted()
        {
            SecurityHelper.LoadGlobalVariable();
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindUserRoleAuthList();
        }
        #region IComponent Members

        public void BeginRebind(IParam param)
        {
        }

        #endregion       
    }
}
