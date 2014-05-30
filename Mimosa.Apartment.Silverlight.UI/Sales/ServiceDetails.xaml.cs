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
    public partial class ServiceDetails : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        public int BookingServiceId { get; set; }
        private BookingRoomService _bookingRoomServiceItem;
        private List<BookingRoomServiceDetail> _originalList = new List<BookingRoomServiceDetail>();
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public ServiceDetails()
        {
            InitializeComponent();

            gvwServiceDetails.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwServiceDetails_AddingNewDataItem);
            gvwServiceDetails.Deleting += new EventHandler<Telerik.Windows.Controls.GridViewDeletingEventArgs>(gvwServiceDetails_Deleting);
            gvwServiceDetails.CellEditEnded += new EventHandler<Telerik.Windows.Controls.GridViewCellEditEndedEventArgs>(gvwServiceDetails_CellEditEnded);
        }

        public void RebindData()
        {
            if (this.BookingServiceId > 0)
            {
                DataServiceHelper.ListBookingRoomServiceAsync(this.BookingServiceId, null, null, ListBookingRoomServiceCompleted);                
            }
        }

        void ListBookingRoomServiceCompleted(List<BookingRoomService> Services)
        {
            if (Services != null && Services.Count > 0)
            {
                _bookingRoomServiceItem = Services[0];
                DataServiceHelper.ListBookingRoomServiceDetailAsync(null, this.BookingServiceId, ListBookingRoomServiceDetailCompleted);
            }
        }

        void ListBookingRoomServiceDetailCompleted(List<BookingRoomServiceDetail> ServiceDetails)
        {
            _originalList.Clear();
            foreach (BookingRoomServiceDetail item in ServiceDetails)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                _originalList.Add(item);
            }
            gvwServiceDetails.ItemsSource = ServiceDetails;
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                BookingRoomServiceDetail item = (BookingRoomServiceDetail)sender;
                item.IsChanged = true;
            }
        }


        void gvwServiceDetails_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        void gvwServiceDetails_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            if (_bookingRoomServiceItem != null)
            {
                BookingRoomServiceDetail newItem = new BookingRoomServiceDetail();
                newItem.BookingRoomServiceId = this.BookingServiceId;
                newItem.Unit = _bookingRoomServiceItem.Unit;
                newItem.Price = _bookingRoomServiceItem.Price;
                newItem.Quantity = 0;
                newItem.TotalPrice = 0;                
                newItem.IsChanged = true;
                e.NewObject = newItem;
            }            
        }

        void gvwServiceDetails_CellEditEnded(object sender, Telerik.Windows.Controls.GridViewCellEditEndedEventArgs e)
        {
            if (e.NewData != null && e.Cell.ParentRow.Item != null)
            {
                BookingRoomServiceDetail currentRow = (BookingRoomServiceDetail)e.Cell.ParentRow.Item;                
                string columnName = e.Cell.Column.UniqueName;
                if (currentRow != null && columnName == "Quantity")
                {
                    currentRow.TotalPrice = currentRow.Price * currentRow.Quantity;
                }
            }
        }

        public void Save()
        {
            List<BookingRoomServiceDetail> saveList = (List<BookingRoomServiceDetail>)gvwServiceDetails.ItemsSource;
            saveList = saveList.Where(i => i.IsChanged == true).ToList();
            //Get delete items :
            foreach (BookingRoomServiceDetail oldItem in _originalList)
            {
                bool isDeleted = true;
                foreach (BookingRoomServiceDetail saveItem in saveList)
                {
                    if (saveItem.BookingRoomServiceDetailId == oldItem.BookingRoomServiceDetailId)
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
            
            DataServiceHelper.SaveBookingRoomServiceDetailAsync(saveList, SaveBookingServiceDetailCompleted);
        }

        void SaveBookingServiceDetailCompleted()
        {
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindData();
        }
    }
}
