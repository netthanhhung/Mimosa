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
        
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        public BookingAdminPage()
        {
            InitializeComponent();
            UserRoleAuths = ucSitePicker.UserRoleAuths = ucBookingAdmin.ucBookingNew.ucSitePicker.UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.BookingAdmin);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            ucBookingAdmin.btnSaveBooking.IsEnabled = ucBookingAdmin.btnSaveBookingEquipment.IsEnabled = ucBookingAdmin.btnSaveBookingService.IsEnabled
                = ucBookingAdmin.btnNewBooking.IsEnabled = ucBookingAdmin.btnInsertBookingEquipment.IsEnabled = ucBookingAdmin.btnInsertBookingService.IsEnabled
                = this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            
            //ucSitePicker.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ucSitePicker_SelectionChanged);
            ucSitePicker.Init();
            ucSitePicker.InitComplete += new EventHandler(ucSitePicker_InitComplete);
            btnSearch.Click += new RoutedEventHandler(btnSearch_Click);
            uiDateFrom.SelectedDate = DateTime.Today.AddMonths(-1);

            ucBookingAdmin.RebindBookingList += new EventHandler(ucBookingAdmin_RebindBookingList);

            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerBookings);
        }

        void ucSitePicker_InitComplete(object sender, EventArgs e)
        {
            RebindBookingData();
        }

        void ucBookingAdmin_RebindBookingList(object sender, EventArgs e)
        {
            RebindBookingData();
        }

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
            DataServiceHelper.ListBookingAsync(Globals.UserLogin.UserOrganisationId, siteId, null, null, null, statusIds, null,
                customer, fromDate, toDate, ListBookingCompleted);
        }

        void ListBookingCompleted(List<Booking> itemSource)
        {
            Globals.IsBusy = false;
            ucBookingAdmin.BindBookingList(itemSource);
        }


    }
}
