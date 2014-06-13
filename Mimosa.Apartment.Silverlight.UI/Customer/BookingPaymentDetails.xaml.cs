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
using System.Text;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class BookingPaymentDetails : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */

        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public BookingPaymentDetails()
        {
            InitializeComponent();
            btnExport.Click += new RoutedEventHandler(btnExport_Click);
            btnEmail.Click += new RoutedEventHandler(btnEmail_Click);
        }

        void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            BookingPayment paymentItem = this.DataContext as BookingPayment;
            if (paymentItem != null)
            {
                List<BookingPayment> paymentList = new List<BookingPayment>();
                paymentList.Add(paymentItem);
                Globals.IsBusy = true;
                DataServiceHelper.SendBookingPaymentMailAsync(paymentList, SendBookingPaymentMailCompleted);
            }
            
        }

        void SendBookingPaymentMailCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.EmailSent);
        }

        void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportPaymentDetails();
        }

        private void ExportPaymentDetails()
        {
            BookingPayment paymentItem = this.DataContext as BookingPayment;
            if (paymentItem != null)
            {
                SaveFileDialog objSFD = new SaveFileDialog()
                {
                    DefaultExt = "csv",
                    Filter = "CSV Files (*.csv)|*.csv|Excel XML (*.xml)|*.xml|All files (*.*)|*.*",
                    FilterIndex = 1
                };
                if (objSFD.ShowDialog() == true)
                {
                    string strFormat = objSFD.SafeFileName.Substring(objSFD.SafeFileName.IndexOf('.') + 1).ToUpper();
                    StringBuilder strBuilder = new StringBuilder();
                    BuildPaymentContent(paymentItem, strBuilder, strFormat);

                    string title = "PaymentDetails_" + DateHelper.DateToString(paymentItem.DateStart, "YYYY-MM-DD") + "_" + DateHelper.DateToString(paymentItem.DateEnd, "YYYY-MM-DD");
                    ReportHelper.ExportStringToFile(objSFD, title, strFormat, strBuilder);
                }
            }
        }

        private void BuildPaymentContent(BookingPayment paymentItem, StringBuilder strBuilder, string strFormat)
        {
            List<string> lstFields = new List<string>();
            lstFields.Add(ReportHelper.FormatField("Site:", strFormat));
            lstFields.Add(ReportHelper.FormatField(paymentItem.SiteName, strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();

            lstFields.Add(ReportHelper.FormatField(lblRoom.Text, strFormat));
            lstFields.Add(ReportHelper.FormatField(paymentItem.RoomName, strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();

            lstFields.Add(ReportHelper.FormatField(lblCustomer1.Text, strFormat));
            lstFields.Add(ReportHelper.FormatField(paymentItem.CustomerName, strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();

            lstFields.Add(ReportHelper.FormatField(lblCustomer2.Text, strFormat));
            lstFields.Add(ReportHelper.FormatField(paymentItem.Customer2Name, strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();
            
            lstFields.Add(ReportHelper.FormatField(lblRoomPrice.Text, strFormat));
            lstFields.Add(ReportHelper.FormatValueColumn(paymentItem.RoomPrice, "0.0", strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();

            lstFields.Add(ReportHelper.FormatField(lblEquipmentPrice.Text, strFormat));
            lstFields.Add(ReportHelper.FormatValueColumn(paymentItem.EquipmentPrice, "0.0", strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();
            
            List<BookingRoomEquipmentDetail> equipmentDetails = gvwEquipmentDetails.ItemsSource as List<BookingRoomEquipmentDetail>;
            if (equipmentDetails != null && equipmentDetails.Count > 0)
            {
                ReportHelper.AddEmptyColumn(lstFields, 1, strFormat);
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[0].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[1].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[2].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[3].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[4].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[5].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[6].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwEquipmentDetails.Columns[7].Header.ToString(), strFormat));
                ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
                lstFields.Clear();

                foreach (BookingRoomEquipmentDetail item in equipmentDetails)
                {
                    ReportHelper.AddEmptyColumn(lstFields, 1, strFormat);
                    lstFields.Add(ReportHelper.FormatField(item.Equipment, strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.DateStart.Value.ToString("dd-MMM-yyyy"), strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.DateEnd.Value.ToString("dd-MMM-yyyy"), strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.Unit, strFormat));
                    lstFields.Add(ReportHelper.FormatValueColumn(item.Quantity, "0.0", strFormat));
                    lstFields.Add(ReportHelper.FormatValueColumn(item.Price, "0.0", strFormat));
                    lstFields.Add(ReportHelper.FormatValueColumn(item.TotalPrice, "0.0", strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.Description, strFormat));
                    ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
                    lstFields.Clear();
                }                
            }

            lstFields.Add(ReportHelper.FormatField(lblServicePrice.Text, strFormat));
            lstFields.Add(ReportHelper.FormatValueColumn(paymentItem.ServicePrice, "0.0", strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();
            List<BookingRoomServiceDetail> serviceDetails = gvwServiceDetails.ItemsSource as List<BookingRoomServiceDetail>;
            if (serviceDetails != null && serviceDetails.Count > 0)
            {
                ReportHelper.AddEmptyColumn(lstFields, 1, strFormat);
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[0].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[1].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[2].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[3].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[4].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[5].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[6].Header.ToString(), strFormat));
                lstFields.Add(ReportHelper.FormatField(gvwServiceDetails.Columns[7].Header.ToString(), strFormat));
                ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
                lstFields.Clear();

                foreach (BookingRoomServiceDetail item in serviceDetails)
                {
                    ReportHelper.AddEmptyColumn(lstFields, 1, strFormat);
                    lstFields.Add(ReportHelper.FormatField(item.Service, strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.DateStart.Value.ToString("dd-MMM-yyyy"), strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.DateEnd.Value.ToString("dd-MMM-yyyy"), strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.Unit, strFormat));
                    lstFields.Add(ReportHelper.FormatValueColumn(item.Quantity, "0.0", strFormat));
                    lstFields.Add(ReportHelper.FormatValueColumn(item.Price, "0.0", strFormat));
                    lstFields.Add(ReportHelper.FormatValueColumn(item.TotalPrice, "0.0", strFormat));
                    lstFields.Add(ReportHelper.FormatField(item.Description, strFormat));
                    ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
                    lstFields.Clear();
                }
            }

            lstFields.Add(ReportHelper.FormatField(lblTotalPrice.Text, strFormat));
            lstFields.Add(ReportHelper.FormatValueColumn(paymentItem.TotalPrice, "0.0", strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();

            lstFields.Add(ReportHelper.FormatField(lblTotalLeft.Text, strFormat));
            lstFields.Add(ReportHelper.FormatValueColumn(paymentItem.MoneyLeft, "0.0", strFormat));
            ReportHelper.BuildStringOfRow(strBuilder, lstFields, strFormat);
            lstFields.Clear();
        }

        public void BindData(BookingPayment paymentItem)
        {
            this.DataContext = paymentItem;
            DataServiceHelper.ListBookingRoomEquipmentDetailAsync(null, null, paymentItem.BookingId, paymentItem.DateStart, paymentItem.DateEnd, ListBookingRoomEquipmentDetailCompleted);
            DataServiceHelper.ListBookingRoomServiceDetailAsync(null, null, paymentItem.BookingId, paymentItem.DateStart, paymentItem.DateEnd, ListBookingRoomServiceDetailCompleted);
        }

        void ListBookingRoomEquipmentDetailCompleted(List<BookingRoomEquipmentDetail> equipmentDetails)
        {
            if (equipmentDetails != null && equipmentDetails.Count > 0)
            {
                gvwEquipmentDetails.Visibility = Visibility.Visible;
                gvwEquipmentDetails.ItemsSource = equipmentDetails;
            }
            else
            {
                gvwEquipmentDetails.Visibility = Visibility.Collapsed;
            }
        }

        void ListBookingRoomServiceDetailCompleted(List<BookingRoomServiceDetail> serviceDetails)
        {
            if (serviceDetails != null && serviceDetails.Count > 0)
            {
                gvwServiceDetails.Visibility = Visibility.Visible;
                gvwServiceDetails.ItemsSource = serviceDetails;
            }
            else
            {
                gvwServiceDetails.Visibility = Visibility.Collapsed;
            }
              
        }
    }
}
