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
    public partial class EquipmentDetails : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        public int BookingEquipmentId { get; set; }
        private BookingRoomEquipment _bookingRoomEquipmentItem;
        private List<BookingRoomEquipmentDetail> _originalList = new List<BookingRoomEquipmentDetail>();
        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public EquipmentDetails()
        {
            InitializeComponent();

            gvwEquipmentDetails.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwEquipmentDetails_AddingNewDataItem);
            gvwEquipmentDetails.Deleting += new EventHandler<Telerik.Windows.Controls.GridViewDeletingEventArgs>(gvwEquipmentDetails_Deleting);
            gvwEquipmentDetails.CellEditEnded += new EventHandler<Telerik.Windows.Controls.GridViewCellEditEndedEventArgs>(gvwEquipmentDetails_CellEditEnded);
        }

        public void RebindData()
        {
            if (this.BookingEquipmentId > 0)
            {
                DataServiceHelper.ListBookingRoomEquipmentAsync(this.BookingEquipmentId, null, null, ListBookingRoomEquipmentCompleted);                
            }
        }

        void ListBookingRoomEquipmentCompleted(List<BookingRoomEquipment> equipments)
        {
            if (equipments != null && equipments.Count > 0)
            {
                _bookingRoomEquipmentItem = equipments[0];
                DataServiceHelper.ListBookingRoomEquipmentDetailAsync(null, this.BookingEquipmentId, ListBookingRoomEquipmentDetailCompleted);
            }
        }

        void ListBookingRoomEquipmentDetailCompleted(List<BookingRoomEquipmentDetail> equipmentDetails)
        {
            _originalList.Clear();
            foreach (BookingRoomEquipmentDetail item in equipmentDetails)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                _originalList.Add(item);
            }
            gvwEquipmentDetails.ItemsSource = equipmentDetails;
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                BookingRoomEquipmentDetail item = (BookingRoomEquipmentDetail)sender;
                item.IsChanged = true;
            }
        }


        void gvwEquipmentDetails_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        void gvwEquipmentDetails_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            if (_bookingRoomEquipmentItem != null)
            {
                BookingRoomEquipmentDetail newItem = new BookingRoomEquipmentDetail();
                newItem.BookingRoomEquipmentId = this.BookingEquipmentId;
                newItem.Unit = _bookingRoomEquipmentItem.Unit;
                newItem.Price = _bookingRoomEquipmentItem.Price;
                newItem.Quantity = 0;
                newItem.TotalPrice = 0;                
                newItem.IsChanged = true;
                e.NewObject = newItem;
            }            
        }

        void gvwEquipmentDetails_CellEditEnded(object sender, Telerik.Windows.Controls.GridViewCellEditEndedEventArgs e)
        {
            if (e.NewData != null && e.Cell.ParentRow.Item != null)
            {
                BookingRoomEquipmentDetail currentRow = (BookingRoomEquipmentDetail)e.Cell.ParentRow.Item;                
                string columnName = e.Cell.Column.UniqueName;
                if (currentRow != null && columnName == "Quantity")
                {
                    currentRow.TotalPrice = currentRow.Price * currentRow.Quantity;
                }
            }
        }

        public void Save()
        {
            List<BookingRoomEquipmentDetail> saveList = (List<BookingRoomEquipmentDetail>)gvwEquipmentDetails.ItemsSource;
            saveList = saveList.Where(i => i.IsChanged == true).ToList();
            //Get delete items :
            foreach (BookingRoomEquipmentDetail oldItem in _originalList)
            {
                bool isDeleted = true;
                foreach (BookingRoomEquipmentDetail saveItem in saveList)
                {
                    if (saveItem.BookingRoomEquipmentDetailId == oldItem.BookingRoomEquipmentDetailId)
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
            
            DataServiceHelper.SaveBookingRoomEquipmentDetailAsync(saveList, SaveBookingEquipmentDetailCompleted);
        }

        void SaveBookingEquipmentDetailCompleted()
        {
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindData();
        }
    }
}
