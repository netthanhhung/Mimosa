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
    public partial class BookingAdminPage : Page
    {
        private static class UserMessages
        {
            internal const string DateNotValid = "Date End must be greater than Date Start";
            internal const string DateIntersect = "There is an intersect between date ranges";

        }

        private int _newId = -1;
        private int _selectedBookingId = -1;
        private List<Booking> _originalItemSource = new List<Booking>();
        private List<BookingRoomEquipment> _currentBookingEquipments = new List<BookingRoomEquipment>();
        private List<BookingRoomService> _currentBookingServices = new List<BookingRoomService>();
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        public BookingAdminPage()
        {
            InitializeComponent();
            //UserRoleAuths = ucSitePicker.UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.DaymarkerAdmin);
            //if (this.UserRoleAuths == null)
            //{
            //    this.Content = SecurityHelper.GetNoPermissionInfoPanel();
            //    return;
            //}
            //btnSaveBooking.IsEnabled = this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            
            //ucSitePicker.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ucSitePicker_SelectionChanged);
            ucSitePicker.Init();
            //gvwBooking.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwBooking_AddingNewDataItem);
            //gvwBooking.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwBooking_Deleting);
            gvwBooking.CellValidating += new EventHandler<GridViewCellValidatingEventArgs>(gvwBooking_CellValidating);
            gvwBooking.BeginningEdit += new EventHandler<GridViewBeginningEditRoutedEventArgs>(gvwBooking_BeginningEdit);
            gvwBooking.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(gvwBooking_SelectionChanged);

            btnSearch.Click += new RoutedEventHandler(btnSearch_Click);
            btnSaveBooking.Click += new RoutedEventHandler(btnSaveBooking_Click);
            btnCancelBooking.Click += new RoutedEventHandler(btnCancelBooking_Click);

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

            //Common
            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerBookings);

            uiDateFrom.SelectedDate = DateTime.Today.AddMonths(-1);
        }
        #region Booking
        
        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            RebindBookingData();
        }

        private void RebindBookingData()
        {
            int? siteId = null;
            if (ucSitePicker.SiteId > 0)
            {
                siteId = ucSitePicker.SiteId;
            }


            DateTime? fromDate = null;
            if (uiDateFrom.SelectedDate.HasValue)
            {
                fromDate = uiDateFrom.SelectedDate.Value;
            }
            DateTime? toDate = null;
            if (uiDateTo.SelectedDate.HasValue)
            {
                toDate = uiDateTo.SelectedDate.Value;
            }

            string statusIds = string.Empty;
            if (chkNew.IsChecked == true)
            {
                statusIds += ((int)BookingStatus.New).ToString() + ",";
            }
            if (chkProcessing.IsChecked == true)
            {
                statusIds += ((int)BookingStatus.Processing).ToString() + ",";
            }
            if (chkContract.IsChecked == true)
            {
                statusIds += ((int)BookingStatus.Contract).ToString() + ",";
            }
            if (chkCancelled.IsChecked == true)
            {
                statusIds += ((int)BookingStatus.Cancelled).ToString() + ",";
            }

            string customer = txtCustomer.Text;
            if (!string.IsNullOrEmpty(customer))
            {
                customer = customer.Trim();
            }
            else
            {
                customer = null;
            }

            if (!string.IsNullOrEmpty(statusIds))
            {
                statusIds = statusIds.Substring(0, statusIds.Length - 1);
            }
            else
            {
                statusIds = null;
            }

            Globals.IsBusy = true;
            DataServiceHelper.ListBookingAsync(Globals.UserLogin.UserOrganisationId, siteId, null, null, null, statusIds,
                customer, fromDate, toDate, ListBookingCompleted);
        }

        void ListBookingCompleted(List<Booking> itemSource)
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
        #endregion

    }
}
