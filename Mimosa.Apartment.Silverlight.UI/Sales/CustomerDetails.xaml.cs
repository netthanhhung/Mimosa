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
    public partial class CustomerDetails : UserControl
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
        public CustomerDetails()
        {
            InitializeComponent();
            
            ucCntactInfoPanel.btnSaveContact.Visibility = Visibility.Collapsed;
            txtFirstName.LostFocus += new RoutedEventHandler(txtFirstName_LostFocus);
            txtLastName.LostFocus += new RoutedEventHandler(txtLastName_LostFocus);
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
