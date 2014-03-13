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
    public partial class SiteAdminPage : Page
    {
        private int _seletedSiteId;
        private List<Site> _originalItemSource = new List<Site>();
        
        //public List<UserRoleAuth> UserRoleAuths { get; set; }

        private static class UserMessages
        {
            internal const string NameErrorMessage2 = "The string may not exceed 128 characters in length.";
            internal const string ContactInfoFor = "Contact information for {0}";
        }

        public SiteAdminPage()
        {
            InitializeComponent();
            //UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.PortalAdmin);

            if (!Globals.UserLogin.IsUserOrganisationAdministrator)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }

            BeginRebindSite();
            
            //Sites
            btnSaveSite.Click += new RoutedEventHandler(btnSaveSite_Click);
            btnCancelSite.Click += new RoutedEventHandler(btnCancelSite_Click);
            gvwSites.SelectionChanged += new EventHandler<Telerik.Windows.Controls.SelectionChangeEventArgs>(gvwSites_SelectionChanged);
            gvwSites.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwSites_AddingNewDataItem);
            //gvwSites.BeginningEdit += new EventHandler<Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs>(gvwSites_BeginningEdit);
            gvwSites.CellValidating += new EventHandler<Telerik.Windows.Controls.GridViewCellValidatingEventArgs>(gvwSites_CellValidating);
            gvwSites.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(gvwSites_RowEditEnded);

            gridContactAccount.Visibility = System.Windows.Visibility.Collapsed;
            //Common
            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerSite);
        }

        private void BeginRebindSite()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListSiteAsync(Globals.UserLogin.UserOrganisationId, null, true, true, ListSiteCompleted);
        }

        void ListSiteCompleted(List<Site> orgList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Site item in orgList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SiteItem_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwSites.ItemsSource = orgList;
            if (_seletedSiteId > 0 && orgList.Count(i => i.SiteId == _seletedSiteId) > 0)
            {
                gvwSites.SelectedItem = orgList.First(i => i.SiteId == _seletedSiteId);
            }
            else if (orgList.Count > 0)
            {
                gvwSites.SelectedItem = orgList[0];
            }
        }

        #region Site GridView
        void gvwSites_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            gvwSites_SelectionChanged(null, null);
        }

        private void gvwSites_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
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

        //void gvwSites_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        //{
        //    if (e.Cell.Column.UniqueName == "NumberOfRooms")
        //    {
        //        e.Cancel = true;
        //    }
        //}

        void gvwSites_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Site newItem = new Site();
            newItem.RecordId = null;
            newItem.CreatedBy = Globals.UserLogin.UserName;
            newItem.OrganisationId = Globals.UserLogin.UserOrganisationId;
            newItem.ContactInformation = new ContactInformation();
            newItem.ContactInformation.ContactTypeId = (int) ContactType.Site;
            if (_originalItemSource.Count() > 0)
                newItem.DisplayIndex = _originalItemSource.Max(d => d.DisplayIndex) + 1;
            newItem.IsChanged = true;
            e.NewObject = newItem;

            gridContactAccount.Visibility = System.Windows.Visibility.Collapsed;
        }

        void btnCancelSite_Click(object sender, RoutedEventArgs e)
        {
            if (gvwSites.SelectedItem != null)
                _seletedSiteId = ((Site)gvwSites.SelectedItem).SiteId;
            BeginRebindSite();
        }

        void btnSaveSite_Click(object sender, RoutedEventArgs e)
        {
            if (gvwSites.SelectedItem != null)
                _seletedSiteId = ((Site)gvwSites.SelectedItem).SiteId;
            List<Site> oldList = (List<Site>)gvwSites.ItemsSource;
			List<Site> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveSiteAsync(saveList, SaveSiteCompleted);

        }

        private void SaveSiteCompleted()
        {
            Globals.IsBusy = false;
            //Reload data
            BeginRebindSite();
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }

        void gvwSites_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            Site selectedSite = gvwSites.SelectedItem as Site;
            if (selectedSite != null && selectedSite.SiteId > 0)
            {
                _seletedSiteId = selectedSite.SiteId;
                if (selectedSite.ContactInformation != null)
                {
                    ucCntactInfoPanel.DataContext = selectedSite.ContactInformation;
                }
                gridContactAccount.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                gridContactAccount.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void SiteItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Site item = sender as Site;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
            }
        }
        #endregion


    }
}
