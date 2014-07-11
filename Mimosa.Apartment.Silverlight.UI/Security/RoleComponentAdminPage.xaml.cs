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

    public partial class RoleComponentAdminPage : Page, IComponent
    {        
        private List<Component> _componentItemSource = new List<Component>();
        public List<UserRoleAuth> UserRoleAuths { get; set; }
        
        public RoleComponentAdminPage()
        {
            InitializeComponent();
            UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.RoleComponentPermission);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            btnSave.IsEnabled = Globals.UserLogin.IsUserOrganisationAdministrator 
                            && this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            FillLanguage();

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            uiRoles.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(uiRoles_SelectionChanged);
            FillComboboxData();
        }

        void FillLanguage()
        {
            uiTitle.Text = ResourceHelper.GetReourceValue("RoleComponentAdminPage_uiTitle");
            lblRole.Text = ResourceHelper.GetReourceValue("RoleComponentAdminPage_lblRole");
            lblUserHasRole.Text = ResourceHelper.GetReourceValue("RoleComponentAdminPage_lblUserHasRole");
            btnSave.Content = ResourceHelper.GetReourceValue("Common_btnSave");
            btnCancel.Content = ResourceHelper.GetReourceValue("Common_btnCancel");
        }

        void FillComboboxData()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListComponentAsync(null, ListComponentCompleted);
        }

        void ListComponentCompleted(List<Component> comptList)
        {
            _componentItemSource = comptList;
            DataServiceHelper.ListAspRoleAsync(null, ListRoleCompleted);
        }

        void ListRoleCompleted(List<AspRole> roleList)
        {
            uiRoles.ItemsSource = roleList;
            if (roleList.Count > 0)
                uiRoles.SelectedIndex = 0;
        }

        void uiRoles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RebindUserList();
            RebindRoleComponentData();
        }

        void RebindUserList()
        {
            if (uiRoles.SelectedValue != null)
            {
                Guid roleId = (Guid)uiRoles.SelectedValue;
                DataServiceHelper.ListUserRoleAuthAsync(Globals.UserLogin.UserOrganisationId, null, roleId, ListUserRoleAuthCompleted);
            }
            
        }

        void ListUserRoleAuthCompleted(List<UserRoleAuth> userRoleAuthList)
        {
            //userRoleAuthList = userRoleAuthList.OrderBy(i => i.UserName);
            IEnumerable<string> usernameList = (from i in userRoleAuthList
                                                select i.UserName).OrderBy(i => i.ToLower()).Distinct();
            uiUsersTreeView.ItemsSource = usernameList;                        
        }

        void RebindRoleComponentData()
        {
            if (uiRoles.SelectedValue != null)
            {
                Globals.IsBusy = true;
                Guid roleId = (Guid)uiRoles.SelectedValue;
                DataServiceHelper.ListRoleComponentPermissionAsync(roleId, null, ListRoleComponentCompleted);
            }
        }

        void ListRoleComponentCompleted(List<RoleComponentPermission> roleComptList)
        {
            ucContextComponentTree.RealComponents = _componentItemSource;
            ucContextComponentTree.RoleComponentItemSource = roleComptList;
            ucContextComponentTree.IsReadonly = false;
            ucContextComponentTree.RoleId = (Guid)uiRoles.SelectedValue;
            ucContextComponentTree.BuildTreeView();
            Globals.IsBusy = false;
        }
     

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RebindRoleComponentData();
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Globals.IsBusy = true;
            List<RoleComponentPermission> saveList = ucContextComponentTree.GetSaveList();            
            DataServiceHelper.SaveRoleComponentPermissionAsync(saveList, SaveRoleComponentPermissionCompleted);
        }

        void SaveRoleComponentPermissionCompleted()
        {
            SecurityHelper.LoadGlobalVariable();
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindRoleComponentData();
        }
        #region IComponent Members

        public void BeginRebind(IParam param)
        {
        }

        #endregion

    }
}
