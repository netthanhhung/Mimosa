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
using System.Windows.Media.Imaging;
using System.IO;
using common = Mimosa.Apartment.Common;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class BookingNew : UserControl
    {
        private static class UserMessages
        {
            internal const string DateNotValid = "Date End must be greater than Date Start";
            internal const string RoomNotValid = "Please choose a specific room";
            internal const string CustomerNotValid = "Please input customer's name";
        }
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        public int? SiteId { get; set; }
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public BookingNew()
        {
            InitializeComponent();
            ucSitePicker.Init();
            ucSitePicker.InitComplete += new EventHandler(ucSitePicker_InitComplete);
            ucSitePicker.SelectionChanged += new SelectionChangedEventHandler(ucSitePicker_SelectionChanged);
            uiRoom.SelectionChanged += new SelectionChangedEventHandler(uiRoom_SelectionChanged);
        }

        void uiRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selectedRoom = uiRoom.SelectedItem as Room;
            if (selectedRoom != null && selectedRoom.BasePrice > 0)
            {
                uiRoomPrice.Value = Convert.ToDouble(selectedRoom.BasePrice.Value);
            }
        }

        void ucSitePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ucSitePicker.SiteId > 0)
            {
                string roomStatusId = ((int)RoomStatus.Available).ToString();
                
                DataServiceHelper.ListRoomAsync(Globals.UserLogin.UserOrganisationId, ucSitePicker.SiteId, null, null,
                    roomStatusId, null, null, false, ListRoomCompleted);
            }
        }

        void ListRoomCompleted(List<Room> roomList)
        {
            uiRoom.ItemsSource = roomList;
            if (roomList.Count > 0)
            {
                uiRoom.SelectedIndex = 0;
            }
        }

        void ucSitePicker_InitComplete(object sender, EventArgs e)
        {
            ContactInformation newContact = new ContactInformation();
            ucCustomerDetails.ucCntactInfoPanel.DataContext = newContact;

            if (this.SiteId > 0)
            {
                ucSitePicker.SiteId = this.SiteId.Value;
            }
        }

        public bool CheckValidation()
        {
            bool result = true;
            if (uiRoom.SelectedItem == null)
            {
                result = false;
                MessageBox.Show(UserMessages.RoomNotValid, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
            }            
            if (uiDateFrom.SelectedDate.HasValue && uiDateTo.SelectedDate.HasValue && uiDateFrom.SelectedDate.Value > uiDateTo.SelectedDate.Value)
            {
                result = false;
                MessageBox.Show(UserMessages.DateNotValid, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
            }

            if (result)
            {
                result = ucCustomerDetails.CheckValidation();
            }

            return result;
        }

        public Booking GetSavedBooking()
        {
            Room selectedRoom = uiRoom.SelectedItem as Room;
            
            Booking newItem = new Booking();
            newItem.SiteId = selectedRoom.SiteId;
            newItem.SiteName = ucSitePicker.SiteDisplayName;
            newItem.RoomId = selectedRoom.RoomId;
            newItem.RoomName = selectedRoom.RoomName;
            newItem.BookingStatus = BookingStatus.New.ToString();
            newItem.BookingStatusId = (int)BookingStatus.New;
            newItem.RoomPrice = newItem.TotalPrice = newItem.ContractTotalPrice = uiRoomPrice.Value.HasValue ? Convert.ToDecimal(uiRoomPrice.Value.Value) : 0;
            newItem.Description = uiDescription.Text;
            newItem.BookDate = DateTime.Now;
            newItem.ContractDateStart = uiDateFrom.SelectedDate;
            newItem.ContractDateEnd = uiDateTo.SelectedDate;
            newItem.BookingEquipments = new List<BookingRoomEquipment>();
            newItem.BookingServices = new List<BookingRoomService>();
            Customer customer = ucCustomerDetails.CustomerItem;
            if (customer != null)
            {
                newItem.CustomerItem = customer;
                newItem.CustomerId = customer.CustomerId;
                newItem.CustomerName = customer.FullName;
            }

            Customer customer2 = ucCustomerDetails2.CustomerItem;
            if (customer2 != null && !string.IsNullOrEmpty(customer2.FullName.Trim()))
            {
                newItem.CustomerItem2 = customer2;
                newItem.Customer2Id = customer2.CustomerId;
                newItem.Customer2Name = customer2.FullName;
            }

            newItem.IsChanged = true;

            return newItem;
        }

    }
}
