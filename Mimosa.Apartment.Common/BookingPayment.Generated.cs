using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class BookingPayment : Record
    {
        #region Public Constructors

        public BookingPayment() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string BookingPaymentId = "BookingPaymentId";
            public const string BookingId = "BookingId";
            public const string DateStart = "DateStart";
            public const string DateEnd = "DateEnd";
            public const string RoomPrice = "RoomPrice";
            public const string EquipmentPrice = "EquipmentPrice";
            public const string ServicePrice = "ServicePrice";
            public const string TotalPrice = "TotalPrice";
            public const string CustomerPaid = "CustomerPaid";
            public const string Payment = "Payment";

            public const string SiteId = "SiteId";
            public const string SiteName = "SiteName";
            public const string RoomId = "RoomId";
            public const string RoomName = "RoomName";
            public const string CustomerId = "CustomerId";
            public const string Customer2Id = "Customer2Id";
            public const string CustomerName = "CustomerName";
            public const string Customer2Name = "Customer2Name";
            
        }

        #endregion

        #region Properties

        private int _bookingPaymentId;
        [DataMember]
        public int BookingPaymentId { get { return _bookingPaymentId; } set { _bookingPaymentId = value; RaisePropertyChanged("BookingPaymentId"); } }

        private int _bookingId;
        [DataMember]
        public int BookingId { get { return _bookingId; } set { if (!this.BookingId.Equals(value)) { _bookingId = value; RaisePropertyChanged("BookingId"); } } }

        private DateTime _dateStart;
        [DataMember]
        public DateTime DateStart { get { return _dateStart; } set { if (!this.DateStart.Equals(value)) { _dateStart = value; RaisePropertyChanged("DateStart"); } } }

        private DateTime _dateEnd;
        [DataMember]
        public DateTime DateEnd { get { return _dateEnd; } set { if (!this.DateEnd.Equals(value)) { _dateEnd = value; RaisePropertyChanged("DateEnd"); } } }
        
        private decimal _roomPrice;
        [DataMember]
        public decimal RoomPrice { get { return _roomPrice; } set { if (!this.RoomPrice.Equals(value)) { _roomPrice = value; RaisePropertyChanged("RoomPrice"); } } }

        private decimal _equipmentPrice;
        [DataMember]
        public decimal EquipmentPrice { get { return _equipmentPrice; } set { if (!this.EquipmentPrice.Equals(value)) { _equipmentPrice = value; RaisePropertyChanged("EquipmentPrice"); } } }

        private decimal _servicePrice;
        [DataMember]
        public decimal ServicePrice { get { return _servicePrice; } set { if (!this.ServicePrice.Equals(value)) { _servicePrice = value; RaisePropertyChanged("ServicePrice"); } } }

        private decimal _totalPrice;
        [DataMember]
        public decimal TotalPrice { get { return _totalPrice; } set { if (!this.TotalPrice.Equals(value)) { _totalPrice = value; RaisePropertyChanged("TotalPrice"); } } }

        private decimal _customerPaid;
        [DataMember]
        public decimal CustomerPaid { get { return _customerPaid; } set { if (!this.CustomerPaid.Equals(value)) { _customerPaid = value; RaisePropertyChanged("CustomerPaid"); } } }

        private decimal _moneyLeft;
        [DataMember]
        public decimal MoneyLeft { get { return _moneyLeft; } set { if (!this.MoneyLeft.Equals(value)) { _moneyLeft = value; RaisePropertyChanged("MoneyLeft"); } } }

        private bool _payment;
        [DataMember]
        public bool Payment { get { return _payment; } set { if (!this.Payment.Equals(value)) { _payment = value; RaisePropertyChanged("Payment"); } } }


        private int _siteId;
        [DataMember]
        public int SiteId { get { return _siteId; } set { if (!this.SiteId.Equals(value)) { _siteId = value; RaisePropertyChanged("SiteId"); } } }

        private string _siteName;
        [DataMember]
        public string SiteName { get { return _siteName; } set { if (!object.ReferenceEquals(this.SiteName, value)) { _siteName = value; RaisePropertyChanged("SiteName"); } } }

        private int _roomId;
        [DataMember]
        public int RoomId { get { return _roomId; } set { if (!this.RoomId.Equals(value)) { _roomId = value; RaisePropertyChanged("RoomId"); } } }

        private string _roomName;
        [DataMember]
        public string RoomName { get { return _roomName; } set { if (!object.ReferenceEquals(this.RoomName, value)) { _roomName = value; RaisePropertyChanged("RoomName"); } } }

        private int? _customerId;
        [DataMember]
        public int? CustomerId { get { return _customerId; } set { if (!this.CustomerId.Equals(value)) { _customerId = value; RaisePropertyChanged("CustomerId"); } } }

        private int? _customer2Id;
        [DataMember]
        public int? Customer2Id { get { return _customer2Id; } set { if (!this.Customer2Id.Equals(value)) { _customer2Id = value; RaisePropertyChanged("Customer2Id"); } } }

        private string _customerName;
        [DataMember]
        public string CustomerName { get { return _customerName; } set { if (!object.ReferenceEquals(this.CustomerName, value)) { _customerName = value; RaisePropertyChanged("CustomerName"); } } }

        private string _customer2Name;
        [DataMember]
        public string Customer2Name { get { return _customer2Name; } set { if (!object.ReferenceEquals(this.Customer2Name, value)) { _customer2Name = value; RaisePropertyChanged("Customer2Name"); } } }

        #endregion
    }
}




