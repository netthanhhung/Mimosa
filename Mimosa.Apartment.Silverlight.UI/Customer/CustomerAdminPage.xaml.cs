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
    public partial class CustomerAdminPage : Page
    {
        private const int _defaultCountryId = 13;    

        private int _seletedCustomerId;
        private List<Customer> _originalItemSource = new List<Customer>();
                
        private static class UserMessages
        {
            internal const string NameErrorMessage2 = "The string may not exceed 128 characters in length.";
            
        }
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        public CustomerAdminPage()
        {
            InitializeComponent();

            UserRoleAuths = ucSitePicker.UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.BookingAdmin);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            btnSaveCustomer.IsEnabled = ucCntactInfoPanel.IsEnabled
                = ucBookingAdmin.btnSaveBooking.IsEnabled = ucBookingAdmin.btnSaveBookingEquipment.IsEnabled = ucBookingAdmin.btnSaveBookingService.IsEnabled
                = ucBookingAdmin.btnNewBooking.IsEnabled = ucBookingAdmin.btnInsertBookingEquipment.IsEnabled = ucBookingAdmin.btnInsertBookingService.IsEnabled
                = this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            

            ucSitePicker.Init();
            ucSitePicker.InitComplete += new EventHandler(ucSitePicker_InitComplete);
            btnSaveCustomer.Click += new RoutedEventHandler(btnSaveCustomer_Click);
            btnCancelCustomer.Click += new RoutedEventHandler(btnCancelCustomer_Click);
            gvwCustomers.SelectionChanged += new EventHandler<Telerik.Windows.Controls.SelectionChangeEventArgs>(gvwCustomers_SelectionChanged);
            gvwCustomers.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwCustomers_AddingNewDataItem);            
            gvwCustomers.CellValidating += new EventHandler<Telerik.Windows.Controls.GridViewCellValidatingEventArgs>(gvwCustomers_CellValidating);
            gvwCustomers.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(gvwCustomers_RowEditEnded);

            Dictionary<int, string> genderDic = new Dictionary<int, string>();
            genderDic.Add(-1, string.Empty);
            genderDic.Add((int)Enums.Gender.Male, Enums.Gender.Male.ToString());
            genderDic.Add((int)Enums.Gender.Female, Enums.Gender.Female.ToString());
            ((GridViewComboBoxColumn)this.gvwCustomers.Columns["Gender"]).ItemsSource = genderDic;

            ucCntactInfoPanel.btnSaveContact.Visibility = Visibility.Collapsed;

            btnSearch.Click += new RoutedEventHandler(btnSearch_Click);
            chkHasContract.Checked += new RoutedEventHandler(chkHasContract_CheckChanged);
            chkHasContract.Unchecked += new RoutedEventHandler(chkHasContract_CheckChanged);
            uiDateFrom.SelectedDate = DateTime.Today;

            gridDetails.Visibility = ucCntactInfoPanel.Visibility = System.Windows.Visibility.Collapsed;

            ucBookingAdmin.RebindBookingList +=new EventHandler(ucBookingAdmin_RebindBookingList);

            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerCustomers);
        }

        void ucSitePicker_InitComplete(object sender, EventArgs e)
        {
            BeginRebindCustomer();
        }

        void chkHasContract_CheckChanged(object sender, RoutedEventArgs e)
        {
            panelHasContract.Visibility = chkHasContract.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            BeginRebindCustomer();
        }     

        private void BeginRebindCustomer()
        {
            Globals.IsBusy = true;
            int? siteId = null;
            if (ucSitePicker.SiteId > 0)
            {
                siteId = ucSitePicker.SiteId;
            }
            DataServiceHelper.ListCustomerAsync(Globals.UserLogin.UserOrganisationId, null, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), 
                siteId, chkHasContract.IsChecked == true, uiDateFrom.SelectedDate, uiDateTo.SelectedDate,
                chkShowInactive.IsChecked == true, ListCustomerCompleted);
        }

        void ListCustomerCompleted(List<Customer> customerList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Customer item in customerList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CustomerItem_PropertyChanged);
                if (item.ContactInformation == null)
                {
                    item.ContactInformation = new ContactInformation();
                    item.ContactInformation.ContactTypeId = (int)ContactType.Customer;
                }
                item.ContactInformation.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ContactInformation_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwCustomers.ItemsSource = customerList;
            if (_seletedCustomerId > 0 && customerList.Count(i => i.CustomerId == _seletedCustomerId) > 0)
            {
                gvwCustomers.SelectedItem = customerList.First(i => i.CustomerId == _seletedCustomerId);
            }
            else if (customerList.Count > 0)
            {
                gvwCustomers.SelectedItem = customerList[0];
            }
        }

        #region Customer GridView
        void gvwCustomers_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            gvwCustomers_SelectionChanged(null, null);
        }

        private void gvwCustomers_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "FirstName" || e.Cell.Column.UniqueName == "LastName")
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
            //else if (e.Cell.Column.UniqueName == "DisplayIndex")
            //{
            //    int displayindex;
            //     if (!int.TryParse(e.NewValue.ToString(), out displayindex))
            //    {
            //        e.IsValid = false;
            //        e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
            //    }                
            //}
        }

        //void gvwCustomers_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        //{
        //    if (e.Cell.Column.UniqueName == "NumberOfRooms")
        //    {
        //        e.Cancel = true;
        //    }
        //}

        void gvwCustomers_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Customer newItem = new Customer();
            newItem.RecordId = null;
            newItem.OrganisationId = Globals.UserLogin.UserOrganisationId;
            newItem.CreatedBy = Globals.UserLogin.UserName;
            newItem.DateCreated = Globals.Now;
            newItem.ContactInformation = new ContactInformation();
            newItem.ContactInformation.CountryId = _defaultCountryId;
            newItem.ContactInformation.ContactTypeId = (int)ContactType.Customer;
            newItem.IsChanged = newItem.ContactInformation.IsChanged = true;
            e.NewObject = newItem;

            gridDetails.Visibility = ucCntactInfoPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        void btnCancelCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (gvwCustomers.SelectedItem != null)
                _seletedCustomerId = ((Customer)gvwCustomers.SelectedItem).CustomerId;
            BeginRebindCustomer();
        }

        void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (gvwCustomers.SelectedItem != null)
                _seletedCustomerId = ((Customer)gvwCustomers.SelectedItem).CustomerId;
            List<Customer> oldList = (List<Customer>)gvwCustomers.ItemsSource;
			List<Customer> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null || d.ContactInformation.IsChanged)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveCustomerAsync(saveList, SaveCustomerCompleted);

            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }

        void SaveCustomerCompleted(List<Customer> savedList)
        {
            List<Customer> oldList = (List<Customer>)gvwCustomers.ItemsSource;
            List<Customer> newList = oldList.Where(d => (!d.IsChanged && d.NullableRecordId > 0 && !d.ContactInformation.IsChanged)).ToList();
            savedList.AddRange(newList);
            ListCustomerCompleted(savedList);
        }

        void gvwCustomers_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (gvwCustomers.SelectedItem != null)
            {
                Customer selectedCustomer = gvwCustomers.SelectedItem as Customer;
                gridDetails.Visibility = ucCntactInfoPanel.Visibility = System.Windows.Visibility.Collapsed;
                
                if (selectedCustomer != null)
                {
                    _seletedCustomerId = selectedCustomer.CustomerId;
                    if (selectedCustomer.ContactInformation == null)
                    {
                        selectedCustomer.ContactInformation = new ContactInformation();
                        selectedCustomer.ContactInformation.IsChanged = true;
                        selectedCustomer.ContactInformation.CountryId = _defaultCountryId;
                    }
                    ucCntactInfoPanel.DataContext = selectedCustomer.ContactInformation;

                    gridDetails.Visibility = ucCntactInfoPanel.Visibility = System.Windows.Visibility.Visible;

                    ucBookingAdmin.CustomerItem = selectedCustomer;
                    RebindBookingData();
                }
            }
        }

        void ucBookingAdmin_RebindBookingList(object sender, EventArgs e)
        {
            RebindBookingData();
        }

        private void RebindBookingData()
        {
            if (_seletedCustomerId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListBookingAsync(Globals.UserLogin.UserOrganisationId, null, null, null, null, null, _seletedCustomerId, null, null, null, ListBookingCompleted);
            }
        }

        void ListBookingCompleted(List<Booking> itemSource)
        {
            Globals.IsBusy = false;
            ucBookingAdmin.BindBookingList(itemSource);
        }


        void CustomerItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Customer item = sender as Customer;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }

        void ContactInformation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ContactInformation item = sender as ContactInformation;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }
        #endregion


    }
}
