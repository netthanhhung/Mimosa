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

            ucCntactInfoPanel.btnSaveContact.Visibility = Visibility.Collapsed;
            txtFirstName.LostFocus += new RoutedEventHandler(txtFirstName_LostFocus);
            txtLastName.LostFocus += new RoutedEventHandler(txtLastName_LostFocus);
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
        }

        void ucSitePicker_InitComplete(object sender, EventArgs e)
        {
            ContactInformation newContact = new ContactInformation();
            ucCntactInfoPanel.DataContext = newContact;
            if (this.SiteId > 0)
            {
                ucSitePicker.SiteId = this.SiteId.Value;
            }
        }

        void txtLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ucCntactInfoPanel.txtLastName.Text))
            {
                ucCntactInfoPanel.txtLastName.Text = txtLastName.Text;
            }
        }

        void txtFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ucCntactInfoPanel.txtFirstName.Text))
            {
                ucCntactInfoPanel.txtFirstName.Text = txtFirstName.Text;
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
            if (string.IsNullOrEmpty(txtFirstName.Text) && string.IsNullOrEmpty(txtLastName.Text))
            {
                result = false;
                MessageBox.Show(UserMessages.CustomerNotValid, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
            }
            if (uiDateFrom.SelectedDate.HasValue && uiDateTo.SelectedDate.HasValue && uiDateFrom.SelectedDate.Value > uiDateTo.SelectedDate.Value)
            {
                result = false;
                MessageBox.Show(UserMessages.DateNotValid, Globals.UserMessages.ValidationError, MessageBoxButton.OK);
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
            newItem.RoomPrice = newItem.TotalPrice = uiRoomPrice.Value.HasValue ? Convert.ToDecimal(uiRoomPrice.Value.Value) : 0;
            newItem.Description = uiDescription.Text;
            newItem.BookDate = DateTime.Now;
            newItem.ContractDateStart = uiDateFrom.SelectedDate;
            newItem.ContractDateEnd = uiDateTo.SelectedDate;
            newItem.BookingEquipments = new List<BookingRoomEquipment>();
            newItem.BookingServices = new List<BookingRoomService>();
            newItem.CustomerItem = new Customer();
            newItem.CustomerItem.ContactInformation = ucCntactInfoPanel.DataContext as ContactInformation;
            if (newItem.CustomerItem.ContactInformation == null)
            {
                newItem.CustomerItem.ContactInformation = new ContactInformation();
            }
            newItem.CustomerItem.ContactInformation.ContactTypeId = (int)ContactType.Customer;
            newItem.CustomerItem.FirstName = txtFirstName.Text;
            newItem.CustomerItem.LastName = txtLastName.Text;
            newItem.CustomerItem.Gender = radMale.IsChecked == true ? 1 : 0;
            newItem.CustomerItem.Age = uiAge.Value.HasValue ? Convert.ToInt32(uiAge.Value.Value) : 0;
            
            newItem.IsChanged = true;

            return newItem;
        }

    }
}
