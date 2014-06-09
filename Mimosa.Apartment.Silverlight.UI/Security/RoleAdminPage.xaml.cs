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
    public partial class RoleAdminPage : Page, IComponent
    {
        private List<AspRole> _originalItemSource = new List<AspRole>();
        private static class UserMessages
        {
            internal const string DuplicatedName = "There are duplicated name. Please check again.";
        }
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        public RoleAdminPage()
        {
            InitializeComponent();
            UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.RoleAdmin);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            btnSave.IsEnabled = Globals.UserLogin.IsUserOrganisationAdministrator
                            && this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            
            RebindRoleData();

            gvwRole.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwRole_AddingNewDataItem);
            gvwRole.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwRole_Deleting);
            gvwRole.CellValidating += new EventHandler<GridViewCellValidatingEventArgs>(gvwRole_CellValidating);

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
        }

        void RebindRoleData()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListAspRoleAsync(null, ListRoleCompleted);
        }

        void ListRoleCompleted(List<AspRole> roleList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (AspRole item in roleList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwRole.ItemsSource = roleList ;
        }

        #region GridView event
        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                AspRole item = (AspRole)sender;                
                item.IsChanged = true;
            }
        }

        void gvwRole_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "RoleName")
            {
                if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }
            }
        }

        void gvwRole_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            if (e.Items != null && e.Items.Count() > 0)
            {
                AspRole role = e.Items.First() as AspRole;
                if (role != null)
                {                    
                    if (role.CanDelete && !SecurityHelper.IsBuildInRole(role.RoleId))
                    {
                        MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format(Globals.UserMessages.DeleteFailed, role.RoleName), Globals.UserMessages.OperationFailed, MessageBoxButton.OK);
                        e.Cancel = true;
                    }
                }
            }
        }

        void gvwRole_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            AspRole newItem = new AspRole();
            newItem.IsChanged = true;
            e.NewObject = newItem;
        }

        #endregion

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<AspRole> saveList = (List<AspRole>)gvwRole.ItemsSource;
            //Check valid data : Name should not be duplicated
            if (saveList != null && saveList.Count > 0)
            {
                for (int i = 0; i < saveList.Count - 1; i++)
                {
                    AspRole firstItem = saveList[i];
                    for (int j = i + 1; j < saveList.Count; j++)
                    {
                        AspRole secondItem = saveList[j];
                        if (firstItem.RoleName == secondItem.RoleName)
                        {
                            MessageBox.Show(UserMessages.DuplicatedName, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                            return;
                        }
                    }
                }
            }

            //Get delete items :
            foreach (AspRole oldItem in _originalItemSource)
            {
                bool isDeleted = true;
                foreach (AspRole saveItem in saveList)
                {
                    if (saveItem.RoleId == oldItem.RoleId)
                    {
                        isDeleted = false;
                        break;
                    }
                }
                if (isDeleted)
                {
                    oldItem.IsDeleted = true;
                    saveList.Add(oldItem);
                }
            }
            Globals.IsBusy = true;
            DataServiceHelper.SaveAspRoleAsync(saveList, Globals.UserLogin.UserName, SaveRoleCompleted);

        }

        void SaveRoleCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindRoleData();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RebindRoleData();
        }

        #region IComponent Members

        public void BeginRebind(IParam param)
        {
        }

        #endregion
    }
}
