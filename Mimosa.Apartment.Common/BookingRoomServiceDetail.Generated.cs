using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class BookingRoomServiceDetail : Record
    {
        #region Public Constructors

        public BookingRoomServiceDetail() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string BookingRoomServiceDetailId = "BookingRoomServiceDetailId";
            public const string BookingRoomServiceId = "BookingRoomServiceId";
            public const string Quantity = "Quantity";
            public const string DateStart = "DateStart";
            public const string DateEnd = "DateEnd";
            public const string Price = "Price";
            public const string Unit = "Unit";
            public const string TotalPrice = "TotalPrice";
            public const string Payment = "Payment";
            public const string Description = "Description";
            public const string ServiceId = "ServiceId";
            public const string Service = "Service";
        }

        #endregion

        #region Properties
        private int _bookingRoomServiceDetailId;
        [DataMember]
        public int BookingRoomServiceDetailId { get { return _bookingRoomServiceDetailId; } set { _bookingRoomServiceDetailId = value; RaisePropertyChanged("BookingRoomServiceDetailId"); } }

        private int _bookingRoomServiceId;
        [DataMember]
        public int BookingRoomServiceId { get { return _bookingRoomServiceId; } set { if (!this.BookingRoomServiceId.Equals(value)) { _bookingRoomServiceId = value; RaisePropertyChanged("BookingRoomServiceId"); } } }

        private decimal? _quantity;
        [DataMember]
        public decimal? Quantity { get { return _quantity; } set { if (!this.Quantity.Equals(value)) { _quantity = value; RaisePropertyChanged("Quantity"); } } }

        private DateTime? _dateStart;
        [DataMember]
        public DateTime? DateStart { get { return _dateStart; } set { if (!this.DateStart.Equals(value)) { _dateStart = value; RaisePropertyChanged("DateStart"); } } }

        private DateTime? _dateEnd;
        [DataMember]
        public DateTime? DateEnd { get { return _dateEnd; } set { if (!this.DateEnd.Equals(value)) { _dateEnd = value; RaisePropertyChanged("DateEnd"); } } }

        private decimal? _price;
        [DataMember]
        public decimal? Price { get { return _price; } set { if (!this.Price.Equals(value)) { _price = value; RaisePropertyChanged("Price"); } } }

        private decimal? _totalPrice;
        [DataMember]
        public decimal? TotalPrice { get { return _totalPrice; } set { if (!this.TotalPrice.Equals(value)) { _totalPrice = value; RaisePropertyChanged("TotalPrice"); } } }

        private string _unit;
        [DataMember]
        public string Unit { get { return _unit; } set { if (!object.ReferenceEquals(this.Unit, value)) { _unit = value; RaisePropertyChanged("Unit"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private bool _payment;
        [DataMember]
        public bool Payment { get { return _payment; } set { if (!this.Payment.Equals(value)) { _payment = value; RaisePropertyChanged("Payment"); } } }

        private int _serviceId;
        [DataMember]
        public int ServiceId { get { return _serviceId; } set { if (!this.ServiceId.Equals(value)) { _serviceId = value; RaisePropertyChanged("ServiceId"); } } }

        private string _service;
        [DataMember]
        public string Service { get { return _service; } set { if (!object.ReferenceEquals(this.Service, value)) { _service = value; RaisePropertyChanged("Service"); } } }

        #endregion
    }
}




