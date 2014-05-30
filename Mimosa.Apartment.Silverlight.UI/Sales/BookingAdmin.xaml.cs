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
using Telerik.Windows.Controls.GridView;
using Mimosa.Apartment.Common;


namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class BookingAdmin : UserControl
    {
        private static class UserMessages
        {
            internal const string DateNotValid = "Date End must be greater than Date Start";
            internal const string RoomNotValid = "Please choose a specific room";
            internal const string CustomerNotValid = "Please input customer's name";
        }

        internal event EventHandler RebindBookingList;
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */        
        private int _newId = -1;
        private int _selectedBookingId = -1;
        private List<Booking> _originalItemSource = new List<Booking>();
        private List<BookingRoomEquipment> _currentBookingEquipments = new List<BookingRoomEquipment>();
        private List<BookingRoomService> _currentBookingServices = new List<BookingRoomService>();

        public Customer CustomerItem { get; set; }
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public BookingAdmin()
        {
            InitializeComponent();

            gvwBooking.CellValidating += new EventHandler<GridViewCellValidatingEventArgs>(gvwBooking_CellValidating);
            gvwBooking.BeginningEdit += new EventHandler<GridViewBeginningEditRoutedEventArgs>(gvwBooking_BeginningEdit);
            gvwBooking.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(gvwBooking_SelectionChanged);


            btnSaveBooking.Click += new RoutedEventHandler(btnSaveBooking_Click);
            btnCancelBooking.Click += new RoutedEventHandler(btnCancelBooking_Click);
            btnNewBooking.Click += new RoutedEventHandler(btnNewBooking_Click);
            ucBookingNew.btnOK.Click += new RoutedEventHandler(btnOK_Click);
            ucBookingNew.btnCancel.Click += new RoutedEventHandler(btnCancel_Click);

            ucCustomerDetails.btnOK.Click += new RoutedEventHandler(btnCustomerOK_Click);
            ucCustomerDetails.btnCancel.Click += new RoutedEventHandler(btnCustomerCancel_Click);

            Dictionary<int, string> bookingStatusDic = new Dictionary<int, string>();
            bookingStatusDic.Add((int)BookingStatus.New, BookingStatus.New.ToString());
            bookingStatusDic.Add((int)BookingStatus.Processing, BookingStatus.Processing.ToString());
            bookingStatusDic.Add((int)BookingStatus.Contract, BookingStatus.Contract.ToString());
            bookingStatusDic.Add((int)BookingStatus.Cancelled, BookingStatus.Cancelled.ToString());
            ((GridViewComboBoxColumn)this.gvwBooking.Columns["BookingStatus"]).ItemsSource = bookingStatusDic;

            gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Collapsed;
            ucImageUpload.ImageType = ImageType.Room;

            btnSaveBookingEquipment.Click += new RoutedEventHandler(btnSaveBookingEquipment_Click);
            btnCancelBookingEquipment.Click += new RoutedEventHandler(btnCancelBookingEquipment_Click);
            btnInsertBookingEquipment.Click += new RoutedEventHandler(btnInsertBookingEquipment_Click);
            gvwBookingEquipment.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwBookingEquipment_Deleting);

            btnSaveBookingService.Click += new RoutedEventHandler(btnSaveBookingService_Click);
            btnCancelBookingService.Click += new RoutedEventHandler(btnCancelBookingService_Click);
            btnInsertBookingService.Click += new RoutedEventHandler(btnInsertBookingService_Click);
            gvwBookingService.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwBookingService_Deleting);

            ucEquipmentDetails.btnSave.Click += new RoutedEventHandler(Equipmentetails_btnSave_Click);
            ucEquipmentDetails.btnCancel.Click += new RoutedEventHandler(Equipmentetails_btnCancel_Click);
            ucServiceDetails.btnSave.Click += new RoutedEventHandler(Serviceetails_btnSave_Click);
            ucServiceDetails.btnCancel.Click += new RoutedEventHandler(Serviceetails_btnCancel_Click);
        }

        #region Booking

        void RebindBookingData()
        {
            if (RebindBookingList != null)
            {
                RebindBookingList(this, null);
            }
        }

        public void BindBookingList(List<Booking> itemSource)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Booking item in itemSource)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwBooking.ItemsSource = itemSource;

            if (_selectedBookingId > 0 && itemSource.Count(i => i.BookingId == _selectedBookingId) > 0)
            {
                gvwBooking.SelectedItem = itemSource.First(i => i.BookingId == _selectedBookingId);
            }
            else if (itemSource.Count > 0)
            {
                gvwBooking.SelectedItem = itemSource[0];
            }
        }

        void gvwBooking_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            Booking selectedBooking = gvwBooking.SelectedItem as Booking;
            if (selectedBooking != null && selectedBooking.BookingId > 0)
            {
                gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Visible;
                _selectedBookingId = selectedBooking.BookingId;
                ucImageUpload.ItemId = selectedBooking.RoomId;
                ucImageUpload.BeginRebind();

                //Bind Equipment and Service :
                RebindBookingEquipments();
                RebindBookingServices();
            }
            else
            {
                _selectedBookingId = -1;
                gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void gvwBooking_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            Booking rowData = e.Row.Item as Booking;
            //if (rowData.NullableRecordId != null && e.Cell.Column.UniqueName == "DaymarkerType")
            //{
            //    e.Cancel = true;
            //}
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                Booking item = (Booking)sender;
                item.IsChanged = true;
            }
        }

        void gvwBooking_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            //if (e.Cell.Column.UniqueName == "BookingType" 
            //    || e.Cell.Column.UniqueName == "BookingStatus" 
            //    || e.Cell.Column.UniqueName == "BookingName")
            //{
            //    if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
            //    {
            //        e.IsValid = false;
            //        e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
            //    }
            //}
        }

        void btnSaveBooking_Click(object sender, RoutedEventArgs e)
        {
            if (gvwBooking.SelectedItem != null)
                _selectedBookingId = ((Booking)gvwBooking.SelectedItem).BookingId;
            List<Booking> oldList = (List<Booking>)gvwBooking.ItemsSource;
            List<Booking> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveBookingAsync(saveList, SaveBookingCompleted);
        }

        void SaveBookingCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindBookingData();
        }

        void btnCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            RebindBookingData();
        }


        void btnNewBooking_Click(object sender, RoutedEventArgs e)
        {
            if (this.CustomerItem != null)
            {
                ucBookingNew.panelCustomer.DataContext = this.CustomerItem;
                if (this.CustomerItem.ContactInformation != null)
                {
                    ucBookingNew.ucCntactInfoPanel.DataContext = this.CustomerItem.ContactInformation;
                }
                ucBookingNew.panelCustomer.IsEnabled = ucBookingNew.ucCntactInfoPanel.IsEnabled = false;
            }
            uiPopupNewBooking.ShowDialog();
        }


        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            uiPopupNewBooking.Close();
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ucBookingNew.CheckValidation())
            {
                Booking newBooking = ucBookingNew.GetSavedBooking();
                List<Booking> itemSource = gvwBooking.ItemsSource as List<Booking>;
                if (itemSource == null)
                {
                    itemSource = new List<Booking>();
                }
                itemSource.Add(newBooking);
                gvwBooking.ItemsSource = null;
                gvwBooking.ItemsSource = itemSource;
                uiPopupNewBooking.Close();
            }

        }
        #endregion

        #region BookingEquipment
        void gvwBookingEquipment_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }


        private void RebindBookingEquipments()
        {
            Booking selectedBooking = gvwBooking.SelectedItem as Booking;
            if (selectedBooking != null && selectedBooking.RoomId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListRoomEquipmentAsync(null, selectedBooking.RoomId, ListRoomEquipmentCompleted);


            }
        }

        private void ListRoomEquipmentCompleted(List<RoomEquipment> equipments)
        {
            Globals.IsBusy = false;
            uiEquipmentList.ItemsSource = equipments;
            if (equipments.Count > 0)
            {
                uiEquipmentList.SelectedIndex = 0;
            }

            Booking selectedBooking = gvwBooking.SelectedItem as Booking;
            if (selectedBooking != null && selectedBooking.BookingId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListBookingRoomEquipmentAsync(null, selectedBooking.BookingId, null, ListBookingRoomEquipmentCompleted);
            }
        }

        private void ListBookingRoomEquipmentCompleted(List<BookingRoomEquipment> bookingEquipments)
        {
            Globals.IsBusy = false;
            _currentBookingEquipments.Clear();
            foreach (BookingRoomEquipment bookingEquipment in bookingEquipments)
            {
                bookingEquipment.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(BookingEquipment_PropertyChanged);
                _currentBookingEquipments.Add(bookingEquipment);
            }
            gvwBookingEquipment.ItemsSource = bookingEquipments;
        }

        void BookingEquipment_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                BookingRoomEquipment item = (BookingRoomEquipment)sender;
                item.IsChanged = true;
            }
        }

        void btnCancelBookingEquipment_Click(object sender, RoutedEventArgs e)
        {
            RebindBookingEquipments();
        }

        void btnSaveBookingEquipment_Click(object sender, RoutedEventArgs e)
        {
            List<BookingRoomEquipment> saveList = (List<BookingRoomEquipment>)gvwBookingEquipment.ItemsSource;
            //Get delete items :
            foreach (BookingRoomEquipment oldItem in _currentBookingEquipments)
            {
                bool isDeleted = true;
                foreach (BookingRoomEquipment saveItem in saveList)
                {
                    if (saveItem.BookingRoomEquipmentId == oldItem.BookingRoomEquipmentId)
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
            DataServiceHelper.SaveBookingRoomEquipmentAsync(saveList, SaveBookingEquipmentCompleted);
        }

        void SaveBookingEquipmentCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindBookingEquipments();
        }

        void btnInsertBookingEquipment_Click(object sender, RoutedEventArgs e)
        {
            RoomEquipment equipment = uiEquipmentList.SelectedItem as RoomEquipment;
            if (equipment != null && _selectedBookingId > 0)
            {
                List<BookingRoomEquipment> list = (List<BookingRoomEquipment>)gvwBookingEquipment.ItemsSource;
                if (list.Count(i => i.EquipmentId == equipment.EquipmentId) > 0)
                {
                    MessageBox.Show(Globals.UserMessages.ItemExist);
                }
                else
                {
                    BookingRoomEquipment newBookingEquipment = new BookingRoomEquipment();
                    newBookingEquipment.BookingId = _selectedBookingId;
                    newBookingEquipment.RoomEquipmentId = equipment.RoomEquipmentId;
                    newBookingEquipment.EquipmentId = equipment.EquipmentId;
                    newBookingEquipment.Equipment = equipment.Equipment;
                    newBookingEquipment.IsChanged = true;
                    newBookingEquipment.Price = equipment.Price;
                    newBookingEquipment.Description = equipment.Description;
                    list.Add(newBookingEquipment);
                    gvwBookingEquipment.ItemsSource = null;
                    gvwBookingEquipment.ItemsSource = list;
                }
            }
        }


        private void BookingRoomEquipmentDetailButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton button = sender as HyperlinkButton;
            if (button != null && button.Tag != null)
            {
                int bookingEquipementId = Convert.ToInt32(button.Tag);
                ucEquipmentDetails.BookingEquipmentId = bookingEquipementId;
                ucEquipmentDetails.RebindData();

                Booking booking = gvwBooking.SelectedItem as Booking;
                if (booking != null)
                {
                    uiPopupEquipmentDetails.Header = "Equipment Details for " + booking.CustomerName;                    
                }
                uiPopupEquipmentDetails.ShowDialog();
            }
        }


        void Equipmentetails_btnSave_Click(object sender, RoutedEventArgs e)
        {
            ucEquipmentDetails.Save();
        }

        void Equipmentetails_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            uiPopupEquipmentDetails.Close();
        }

        #endregion

        #region BookingService
        void gvwBookingService_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }


        private void RebindBookingServices()
        {
            Booking selectedBooking = gvwBooking.SelectedItem as Booking;
            if (selectedBooking != null && selectedBooking.RoomId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListRoomServiceAsync(null, selectedBooking.RoomId, ListRoomServiceCompleted);


            }
        }

        private void ListRoomServiceCompleted(List<RoomService> services)
        {
            Globals.IsBusy = false;
            uiServiceList.ItemsSource = services;
            if (services.Count > 0)
            {
                uiServiceList.SelectedIndex = 0;
            }

            Booking selectedBooking = gvwBooking.SelectedItem as Booking;
            if (selectedBooking != null && selectedBooking.BookingId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListBookingRoomServiceAsync(null, selectedBooking.BookingId, null, ListBookingRoomServiceCompleted);
            }
        }

        private void ListBookingRoomServiceCompleted(List<BookingRoomService> bookingServices)
        {
            Globals.IsBusy = false;
            _currentBookingServices.Clear();
            foreach (BookingRoomService bookingService in bookingServices)
            {
                bookingService.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(BookingService_PropertyChanged);
                _currentBookingServices.Add(bookingService);
            }
            gvwBookingService.ItemsSource = bookingServices;
        }

        void BookingService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                BookingRoomService item = (BookingRoomService)sender;
                item.IsChanged = true;
            }
        }

        void btnCancelBookingService_Click(object sender, RoutedEventArgs e)
        {
            RebindBookingServices();
        }

        void btnSaveBookingService_Click(object sender, RoutedEventArgs e)
        {
            List<BookingRoomService> saveList = (List<BookingRoomService>)gvwBookingService.ItemsSource;
            //Get delete items :
            foreach (BookingRoomService oldItem in _currentBookingServices)
            {
                bool isDeleted = true;
                foreach (BookingRoomService saveItem in saveList)
                {
                    if (saveItem.BookingRoomServiceId == oldItem.BookingRoomServiceId)
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
            DataServiceHelper.SaveBookingRoomServiceAsync(saveList, SaveBookingServiceCompleted);
        }

        void SaveBookingServiceCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindBookingServices();
        }

        void btnInsertBookingService_Click(object sender, RoutedEventArgs e)
        {
            RoomService service = uiServiceList.SelectedItem as RoomService;
            if (service != null && _selectedBookingId > 0)
            {
                List<BookingRoomService> list = (List<BookingRoomService>)gvwBookingService.ItemsSource;
                if (list.Count(i => i.ServiceId == service.ServiceId) > 0)
                {
                    MessageBox.Show(Globals.UserMessages.ItemExist);
                }
                else
                {
                    BookingRoomService newBookingService = new BookingRoomService();
                    newBookingService.BookingId = _selectedBookingId;
                    newBookingService.RoomServiceId = service.RoomServiceId;
                    newBookingService.ServiceId = service.ServiceId;
                    newBookingService.Service = service.Service;
                    newBookingService.IsChanged = true;
                    newBookingService.Price = service.Price;
                    newBookingService.Description = service.Description;
                    list.Add(newBookingService);
                    gvwBookingService.ItemsSource = null;
                    gvwBookingService.ItemsSource = list;
                }
            }
        }

        private void BookingRoomServiceDetailButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton button = sender as HyperlinkButton;
            if (button != null && button.Tag != null)
            {
                int bookingEquipementId = Convert.ToInt32(button.Tag);
                ucServiceDetails.BookingServiceId = bookingEquipementId;
                ucServiceDetails.RebindData();

                Booking booking = gvwBooking.SelectedItem as Booking;
                if (booking != null)
                {
                    uiPopupServiceDetails.Header = "Service Details for " + booking.CustomerName;
                }
                uiPopupServiceDetails.ShowDialog();
            }
        }


        void Serviceetails_btnSave_Click(object sender, RoutedEventArgs e)
        {
            ucServiceDetails.Save();
        }

        void Serviceetails_btnCancel_Click(object sender, RoutedEventArgs e)
        {
            uiPopupServiceDetails.Close();
        }
        #endregion

        #region Customer
        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton button = sender as HyperlinkButton;
            if (button != null && button.Tag != null)
            {
                int customerId = Convert.ToInt32(button.Tag);
                DataServiceHelper.ListCustomerAsync(Globals.UserLogin.UserOrganisationId, customerId, null, null, null, false, null, null, true, ListCustomerCompleted);
            }
        }        

        private void ListCustomerCompleted(List<Customer> customers)
        {
            if (customers != null && customers.Count > 0)
            {
                ucCustomerDetails.DataContext = customers[0];
                ucCustomerDetails.radMale.IsChecked = customers[0].Gender == 1;
                ucCustomerDetails.radFemale.IsChecked = customers[0].Gender == 0;
                ucCustomerDetails.ucCntactInfoPanel.DataContext = customers[0].ContactInformation;
                uiPopupCustomer.ShowDialog();
            }
        }

        void btnCustomerCancel_Click(object sender, RoutedEventArgs e)
        {
            uiPopupCustomer.Close();
        }

        void btnCustomerOK_Click(object sender, RoutedEventArgs e)
        {
            Customer saveItem = ucCustomerDetails.DataContext as Customer;
            if (saveItem != null)
            {
                List<Customer> saveList = new List<Customer>();
                saveItem.UpdatedBy = Globals.UserLogin.UserName;
                saveItem.IsChanged = true;
                saveItem.Gender = ucCustomerDetails.radMale.IsChecked == true ? 1 : 0;
                if (saveItem.ContactInformation != null)
                {
                    saveItem.ContactInformation.IsChanged = true;
                }
                saveList.Add(saveItem);
                Globals.IsBusy = true;
                DataServiceHelper.SaveCustomerAsync(saveList, SaveCustomerComplelted);

            }

        }

        void SaveCustomerComplelted(List<Customer> saveList)
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            uiPopupCustomer.Close();
        }
        #endregion

    }
}
