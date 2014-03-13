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
    public partial class PortalAdminPage : Page
    {
        private int _seletedOrgId;
        private List<Organisation> _originalItemSource = new List<Organisation>();
        
        //public List<UserRoleAuth> UserRoleAuths { get; set; }

        private static class UserMessages
        {
            internal const string NameErrorMessage2 = "The string may not exceed 128 characters in length.";
            internal const string ContactInfoFor = "Contact information for {0}";
            internal const string AccountInfoFor = "Account information for {0}";
        }

        public PortalAdminPage()
        {
            InitializeComponent();            

            if (!Globals.UserLogin.IsUserPortalAdministrator)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }

            BeginRebindOrg();
            
            //Organisations
            btnSaveOrg.Click += new RoutedEventHandler(btnSaveOrg_Click);
            btnCancelOrg.Click += new RoutedEventHandler(btnCancelOrg_Click);
            gvwOrganisations.SelectionChanged += new EventHandler<Telerik.Windows.Controls.SelectionChangeEventArgs>(gvwOrganisations_SelectionChanged);
            gvwOrganisations.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwOrganisations_AddingNewDataItem);
            //gvwOrganisations.BeginningEdit += new EventHandler<Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs>(gvwOrganisations_BeginningEdit);
            gvwOrganisations.CellValidating += new EventHandler<Telerik.Windows.Controls.GridViewCellValidatingEventArgs>(gvwOrganisations_CellValidating);
            gvwOrganisations.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(gvwOrganisations_RowEditEnded);

            
            //Common
            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerOrganisation);
        }

        void gvwOrganisations_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            gvwOrganisations_SelectionChanged(null, null);
        }

        private void BeginRebindOrg()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListOrganisationAsync(SecurityHelper.OrganisationAdministratorRoleId, ListOrganisationCompleted);
        }

        void ListOrganisationCompleted(List<Organisation> orgList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Organisation item in orgList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OrganisationItem_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwOrganisations.ItemsSource = orgList;
            if (_seletedOrgId > 0 && orgList.Count(i => i.OrganisationId == _seletedOrgId) > 0)
            {
                gvwOrganisations.SelectedItem = orgList.First(i => i.OrganisationId == _seletedOrgId);
            }
            else if (orgList.Count > 0)
            {
                gvwOrganisations.SelectedItem = orgList[0];
            }
        }

        #region Organisation GridView
        private void gvwOrganisations_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "Name"
                || e.Cell.Column.UniqueName == "AuthorisationCode" 
                || e.Cell.Column.UniqueName == "LicenceKey")
            {
                if (e.NewValue.ToString().Length < 1)
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }
                else if (e.NewValue.ToString().Length > 128)
                {
                    e.IsValid = false;
                    e.ErrorMessage = UserMessages.NameErrorMessage2;
                }
            }
            else if (e.Cell.Column.UniqueName == "DisplayIndex")
            {
                int displayindex;
                 if (!int.TryParse(e.NewValue.ToString(), out displayindex))
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }                
            }
        }

        //void gvwOrganisations_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        //{
        //    if (e.Cell.Column.UniqueName == "NumberOfRooms")
        //    {
        //        e.Cancel = true;
        //    }
        //}

        void gvwOrganisations_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Organisation newItem = new Organisation();
            newItem.RecordId = null;
            newItem.CreatedBy = Globals.UserLogin.UserName;
            //newItem.DateCreated = Globals.ServerNow;
            newItem.AuthorisationCode = string.Empty;
            newItem.ContactInformation = new ContactInformation();
            newItem.ContactInformation.ContactTypeId = (int)ContactType.Organisation;
            if (_originalItemSource.Count() > 0)
                newItem.DisplayIndex = _originalItemSource.Max(d => d.DisplayIndex) + 1;
            newItem.IsChanged = true;
            e.NewObject = newItem;

            gridContactAccount.Visibility = System.Windows.Visibility.Collapsed;
        }

        void btnCancelOrg_Click(object sender, RoutedEventArgs e)
        {
            if (gvwOrganisations.SelectedItem != null)
                _seletedOrgId = ((Organisation)gvwOrganisations.SelectedItem).OrganisationId;
            BeginRebindOrg();
        }

        void btnSaveOrg_Click(object sender, RoutedEventArgs e)
        {
            if (gvwOrganisations.SelectedItem != null)
                _seletedOrgId = ((Organisation)gvwOrganisations.SelectedItem).OrganisationId;
            List<Organisation> oldList = (List<Organisation>)gvwOrganisations.ItemsSource;
			List<Organisation> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveOrganisationAsync(saveList, SaveOrganisationCompleted);

        }

        private void SaveOrganisationCompleted()
        {
            Globals.IsBusy = false;
            //Reload data
            BeginRebindOrg();
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }

        void gvwOrganisations_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            Organisation selectedOrg = gvwOrganisations.SelectedItem as Organisation;
            if (selectedOrg != null && selectedOrg.OrganisationId > 0)
            {
                _seletedOrgId = selectedOrg.OrganisationId;
                if (selectedOrg.ContactInformation != null)
                {
                    ucCntactInfoPanel.DataContext = selectedOrg.ContactInformation;
                }
                gridContactAccount.Visibility = System.Windows.Visibility.Visible;
                txtAccountInfo.Text = string.Format(UserMessages.AccountInfoFor, selectedOrg.Name);
                ucUserAccount.RebindData(selectedOrg.OrganisationId);
            }
            else
            {
                gridContactAccount.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void OrganisationItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Organisation item = sender as Organisation;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
            }
        }
        #endregion


    }
}
