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
using Telerik.Windows.Controls;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class SiteGroupAdminPage : Page
    {
        private static class UserMessages
        {
            internal const string SiteAlreadyExist = "This site is already added";
            internal const string DuplicatedName = "There are duplicated name. Please check again.";
        }

        private List<SiteGroup> _originalItemSource = new List<SiteGroup>();
        private string _selectedSiteGroup = string.Empty;
        public List<UserRoleAuth> UserRoleAuths { get; set; }
        
        public SiteGroupAdminPage()
        {
            InitializeComponent();
            UserRoleAuths = ucSitePicker.UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.SiteGroupAdmin);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            btnSave.IsEnabled = this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;

            ucSitePicker.Init();
            RebindSiteGroups();

            gvwSiteGroup.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwSiteGroup_AddingNewDataItem);
            gvwSiteGroup.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwSiteGroup_Deleting);
            gvwSiteGroup.CellValidating += new EventHandler<GridViewCellValidatingEventArgs>(gvwSiteGroup_CellValidating);
            gvwSiteGroup.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(gvwSiteGroup_SelectionChanged);

            gvwSites.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwSites_Deleting);

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            btnAddSite.Click += new RoutedEventHandler(btnAddSite_Click);
        }

        void RebindSiteGroups()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListSiteGroupAsync(Globals.UserLogin.UserOrganisationId, null, ListSiteGroupCompleted);
        }

        void ListSiteGroupCompleted(List<SiteGroup> siteGroups)
        {
            _originalItemSource.Clear();
            foreach (SiteGroup sg in siteGroups)
            {
                sg.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(siteGroup_PropertyChanged);
                _originalItemSource.Add(sg);
            }
            gvwSiteGroup.ItemsSource = siteGroups;
            if (!string.IsNullOrEmpty(_selectedSiteGroup)
                && siteGroups.Count(i => i.GroupName == _selectedSiteGroup) > 0) 
            {
                gvwSiteGroup.SelectedItem = siteGroups.First(i => i.GroupName == _selectedSiteGroup);
            }
            else if (siteGroups.Count > 0)
            {
                gvwSiteGroup.SelectedItem = siteGroups[0];
            }
            Globals.IsBusy = false;
        }

        void siteGroup_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                SiteGroup item = (SiteGroup)sender;
                item.UpdatedBy = Globals.UserLogin.UserName;
                item.IsChanged = true;
            }
        }

        void btnAddSite_Click(object sender, RoutedEventArgs e)
        {
            if (ucSitePicker.SelectedSite != null && ucSitePicker.SelectedSite.SiteId > 0 && gvwSiteGroup.SelectedItem != null)
            {
                SiteGroup sg = gvwSiteGroup.SelectedItem as SiteGroup;
                if (sg != null)
                {
                    if (sg.Sites.Count(i => i.SiteId == ucSitePicker.SelectedSite.SiteId) == 0)
                    {
                        sg.IsChanged = true;
                        sg.Sites.Add(ucSitePicker.SelectedSite);
                        gvwSites.ItemsSource = null;
                        gvwSites.ItemsSource = sg.Sites;
                    }
                    else
                    {
                        MessageBox.Show(UserMessages.SiteAlreadyExist, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                    }
                }
            }
        }

        void gvwSites_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                SiteGroup sg = gvwSiteGroup.SelectedItem as SiteGroup;
                if (sg != null)
                {
                    sg.IsChanged = true;
                }
            }
        }

        #region GridView event
        void gvwSiteGroup_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (gvwSiteGroup.SelectedItem != null)
            {
                SiteGroup sg = gvwSiteGroup.SelectedItem as SiteGroup;
                if (sg != null)
                {
                    _selectedSiteGroup = sg.GroupName;
                    gvwSites.ItemsSource = sg.Sites;
                }
            }
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                SiteGroup item = (SiteGroup)sender;
                item.IsChanged = true;
            }
        }

        void gvwSiteGroup_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "GroupName")
            {
                if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }
            }
        }

        void gvwSiteGroup_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            if (e.Items != null && e.Items.Count() > 0)
            {
                SiteGroup siteGroup = e.Items.First() as SiteGroup;
                if (siteGroup != null)
                {
                    if (siteGroup.CanDelete)
                    {
                        MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show(string.Format(Globals.UserMessages.DeleteFailed, siteGroup.GroupName), Globals.UserMessages.OperationFailed, MessageBoxButton.OK);
                        e.Cancel = true;
                    }
                }
            }
        }

        void gvwSiteGroup_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            SiteGroup newItem = new SiteGroup();
            newItem.CreatedBy = newItem.UpdatedBy = Globals.UserLogin.UserName;
            newItem.IsChanged = true;
            newItem.Sites = new List<Site>();
            e.NewObject = newItem;
        }

        #endregion

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (gvwSiteGroup.SelectedItem != null)
            {
                SiteGroup sg = gvwSiteGroup.SelectedItem as SiteGroup;
                if (sg != null)
                {
                    _selectedSiteGroup = sg.GroupName;
                }
            }

            List<SiteGroup> saveList = (List<SiteGroup>)gvwSiteGroup.ItemsSource;
            //Check valid data : Name should not be duplicated
            if (saveList != null && saveList.Count > 0)
            {
                for (int i = 0; i < saveList.Count - 1; i++)
                {
                    SiteGroup firstItem = saveList[i];
                    for (int j = i + 1; j < saveList.Count; j++)
                    {
                        SiteGroup secondItem = saveList[j];
                        if (firstItem.GroupName == secondItem.GroupName)
                        {
                            MessageBox.Show(UserMessages.DuplicatedName, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
                            return;
                        }
                    }
                }
            }

            //Get delete items :
            foreach (SiteGroup oldItem in _originalItemSource)
            {
                bool isDeleted = true;
                foreach (SiteGroup saveItem in saveList)
                {
                    if (saveItem.SiteGroupId == oldItem.SiteGroupId)
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
            DataServiceHelper.SaveSiteGroupsAsync(saveList, SaveSiteGroupCompleted);

        }

        void SaveSiteGroupCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindSiteGroups();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RebindSiteGroups();
        }
    }
}
