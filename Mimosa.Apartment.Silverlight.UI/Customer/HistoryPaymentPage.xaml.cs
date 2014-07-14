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
    public partial class HistoryPaymentPage : Page
    {
        
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        public HistoryPaymentPage()
        {
            InitializeComponent();
            UserRoleAuths = ucSitePicker.UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.MonthlyPayment);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            FillLanguage();
            ucSitePicker.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ucSitePicker_SelectionChanged);
            ucSitePicker.Init();
            //ucSitePicker.InitComplete += new EventHandler(ucSitePicker_InitComplete);
            btnSearch.Click += new RoutedEventHandler(btnSearch_Click);

            RefillDate();
            uiDateFrom.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler(uiDateFrom_SelectionChanged);
            uiDateFrom.LostFocus += new RoutedEventHandler(uiDateFrom_LostFocus);

            gvwBookingPayment.CellEditEnded += new EventHandler<GridViewCellEditEndedEventArgs>(gvwBookingPayment_CellEditEnded);
            uiCustomers.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(uiCustomers_SelectionChanged);
        }

        void FillLanguage()
        {
            uiTitle.Text = ResourceHelper.GetReourceValue("HistoryPaymentPage_uiTitle");
            lblSite.Text = ResourceHelper.GetReourceValue("Common_lblSite");
            lblCustomer.Text = ResourceHelper.GetReourceValue("BookingAdminPage_lblCustomer");
            ucSitePicker.InactiveMessage = ResourceHelper.GetReourceValue("Common_chkShowLegacy");
            lblFromDate.Text = ResourceHelper.GetReourceValue("BookingAdminPage_lblFromDate");
            lblToDate.Text = ResourceHelper.GetReourceValue("BookingAdminPage_lblToDate");
            radPaid.Content = ResourceHelper.GetReourceValue("BookingPaymentPage_radPaid");
            radNotPaid.Content = ResourceHelper.GetReourceValue("BookingPaymentPage_radNotPaid");
            radAll.Content = ResourceHelper.GetReourceValue("BookingPaymentPage_radAll");            

            gvwBookingPayment.Columns["RoomName"].Header = ResourceHelper.GetReourceValue("BookingAdmin_RoomName");
            gvwBookingPayment.Columns["CustomerName"].Header = ResourceHelper.GetReourceValue("BookingAdmin_CustomerName");
            gvwBookingPayment.Columns["Customer2Name"].Header = ResourceHelper.GetReourceValue("BookingAdmin_Customer2Name");
            gvwBookingPayment.Columns["FromDate"].Header = ResourceHelper.GetReourceValue("BookingAdminPage_lblFromDate");
            gvwBookingPayment.Columns["ToDate"].Header = ResourceHelper.GetReourceValue("BookingAdminPage_lblToDate");
            gvwBookingPayment.Columns["RoomPrice"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_RoomPrice");
            gvwBookingPayment.Columns["EquipmentPrice"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_EquipmentPrice");
            gvwBookingPayment.Columns["ServicePrice"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_ServicePrice");
            gvwBookingPayment.Columns["TotalPrice"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_TotalPrice");
            gvwBookingPayment.Columns["CustomerPaid"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_CustomerPaid");
            gvwBookingPayment.Columns["MoneyLeft"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_MoneyLeft");
            gvwBookingPayment.Columns["Payment"].Header = ResourceHelper.GetReourceValue("BookingPaymentPage_Payment");

            btnSearch.Content = ResourceHelper.GetReourceValue("Common_btnSearch");
        }

        void gvwBookingPayment_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            if (e.NewData != null && e.Cell.ParentRow.Item != null)
            {
                BookingPayment currentRow = (BookingPayment)e.Cell.ParentRow.Item;
                string columnName = e.Cell.Column.UniqueName;
                if (currentRow != null && columnName == "CustomerPaid")
                {
                    currentRow.MoneyLeft = currentRow.TotalPrice - currentRow.CustomerPaid;
                }
            }
        }

        void uiDateFrom_LostFocus(object sender, RoutedEventArgs e)
        {
            RefillDate();
        }

        void uiDateFrom_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangedEventArgs e)
        {
            RefillDate();
        }

        void RefillDate()
        {
            if (!uiDateFrom.SelectedDate.HasValue)
            {
                DateTime startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                uiDateFrom.SelectedDate = startMonth.AddMonths(-12);
                uiDateTo.SelectedDate = startMonth.AddDays(-1);
            }
            else
            {
                uiDateFrom.SelectedDate = new DateTime(uiDateFrom.SelectedDate.Value.Year, uiDateFrom.SelectedDate.Value.Month, 1);
                DateTime dateTo = new DateTime(uiDateFrom.SelectedDate.Value.Year, uiDateFrom.SelectedDate.Value.Month, 1);
                uiDateTo.SelectedDate = dateTo.AddMonths(1).AddDays(-1);
            }

        }

        void ucSitePicker_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //RebindBookingPaymentData();
            DataServiceHelper.ListCustomerAsync(Globals.UserLogin.UserOrganisationId, null, null, null, ucSitePicker.SiteId, true, null, null, false, ListCustomerCompleted); 
        }

        void ListCustomerCompleted(List<Customer> customers)
        {
            uiCustomers.ItemsSource = customers;
            if (customers.Count > 0)
            {
                uiCustomers.SelectedIndex = 0;
            }
        }

        void uiCustomers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RebindBookingPaymentData();
        }


        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            RebindBookingPaymentData();
        }

        private void RebindBookingPaymentData()
        {            
            if (ucSitePicker.SiteId > 0 && uiDateFrom.SelectedDate.HasValue && uiDateTo.SelectedDate.HasValue && uiCustomers.SelectedValue != null)
            {
                Globals.IsBusy = true;
                int payment = 2;
                if (radPaid.IsChecked == true)
                {
                    payment = 1;
                }
                else if (radNotPaid.IsChecked == true)
                {
                    payment = 0;
                }
                int customerId = Convert.ToInt32(uiCustomers.SelectedValue);
                DataServiceHelper.ListHistoryPaymentAsync(Globals.UserLogin.UserOrganisationId, ucSitePicker.SiteId, null, customerId,
                    uiDateFrom.SelectedDate.Value, uiDateTo.SelectedDate.Value, payment, ListBookingPaymentCompleted);    
            }
        }

        void ListBookingPaymentCompleted(List<BookingPayment> itemSource)
        {
            Globals.IsBusy = false;
            gvwBookingPayment.ItemsSource = itemSource;
            foreach (BookingPayment bookingPaymentItem in itemSource)
            {
                bookingPaymentItem.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(BookingPaymentItem_PropertyChanged);
            }
        }

        void BookingPaymentItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            BookingPayment item = sender as BookingPayment;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
                item.UpdatedBy = Globals.UserLogin.UserName;
            }
        }


        private void BookingPaymentDetail_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton button = sender as HyperlinkButton;
            if (button != null)
            {
                BookingPayment bookingPayment = button.DataContext as BookingPayment;
                if (bookingPayment != null)
                {
                    ucPaymentDetails.BindData(bookingPayment);
                    uiPopupPaymentDetails.Header = string.Format(ResourceHelper.GetReourceValue("BookingPaymentPage_uiPopupPaymentDetails"),
                                                                DateHelper.DateToString(bookingPayment.DateStart), 
                                                                DateHelper.DateToString(bookingPayment.DateEnd));
                    uiPopupPaymentDetails.ShowDialog();
                }
            }
        }
    }
}
