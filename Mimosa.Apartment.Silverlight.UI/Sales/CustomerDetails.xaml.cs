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
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        public int? SiteId { get; set; }
        public int? BoookingId { get; set; }

        public bool ShowOKCancel
        {
            get
            {
                return gridOKCancel.Visibility == Visibility.Visible;
            }
            set
            {
                gridOKCancel.Visibility = ucCntactInfoPanel.btnSaveContact.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                
            }
        }

        public Customer CustomerItem
        {
            get
            {
                Customer result = this.DataContext as Customer;
                if (result != null)
                {
                    result.Gender = radMale.IsChecked == true ? 1 : 0;
                }
                return result;
            }
            set
            {
                if (value != null && value.Gender == 1)
                {
                    radMale.IsChecked = true;
                }
                else
                {
                    radFemale.IsChecked = true;
                }
                if (value.ContactInformation == null)
                {
                    value.ContactInformation = new ContactInformation();
                    value.ContactInformation.ContactTypeId = (int)ContactType.Customer;
                }
                this.DataContext = value;                
                this.ucCntactInfoPanel.DataContext = value.ContactInformation;
            }
        }
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public CustomerDetails()
        {
            InitializeComponent();
            FillLanguage();
            txtFirstName.LostFocus += new RoutedEventHandler(txtFirstName_LostFocus);
            txtLastName.LostFocus += new RoutedEventHandler(txtLastName_LostFocus);
        }

        void FillLanguage()
        {
            lblCustomerTitle.Text = ResourceHelper.GetReourceValue("CustomerDetails_lblCustomerTitle");
            lblFirstName.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblFirstName");
            lblLastName.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblLastName");
            lblAge.Text = ResourceHelper.GetReourceValue("CustomerDetails_lblAge");
            lblGender.Text = ResourceHelper.GetReourceValue("CustomerDetails_lblGender");
            radMale.Content = ResourceHelper.GetReourceValue("CustomerDetails_radMale");
            radFemale.Content = ResourceHelper.GetReourceValue("CustomerDetails_radFemale");

            btnOK.Content = ResourceHelper.GetReourceValue("Common_btnSave");
            btnCancel.Content = ResourceHelper.GetReourceValue("Common_btnCancel");
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
            
            if (string.IsNullOrEmpty(txtFirstName.Text) && string.IsNullOrEmpty(txtLastName.Text))
            {
                result = false;
                MessageBox.Show(ResourceHelper.GetReourceValue("CustomerDetails_RoomNotValid"), ResourceHelper.GetReourceValue("Common_ValidationError"), MessageBoxButton.OK);
            }
            
            return result;
        }

    }
}
