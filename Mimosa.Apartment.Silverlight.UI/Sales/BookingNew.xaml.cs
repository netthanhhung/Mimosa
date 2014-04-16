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


        

    }
}
