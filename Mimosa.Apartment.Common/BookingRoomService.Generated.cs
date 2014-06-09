using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class BookingRoomService : Record
    {
        #region Public Constructors

        public BookingRoomService() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string BookingRoomServiceId = "BookingRoomServiceId";
            public const string BookingId = "BookingId";
            public const string ServiceId = "ServiceId";
            public const string Service = "Service";
            public const string Description = "Description";
            public const string Price = "Price";
            public const string Unit = "Unit";
        }

        #endregion

        #region Properties

        private int _bookingRoomServiceId;
        [DataMember]
        public int BookingRoomServiceId { get { return _bookingRoomServiceId; } set { _bookingRoomServiceId = value; RaisePropertyChanged("BookingRoomServiceId"); } }

        private int _bookingId;
        [DataMember]
        public int BookingId { get { return _bookingId; } set { if (!this.BookingId.Equals(value)) { _bookingId = value; RaisePropertyChanged("BookingId"); } } }

        private int _serviceId;
        [DataMember]
        public int ServiceId { get { return _serviceId; } set { if (!this.ServiceId.Equals(value)) { _serviceId = value; RaisePropertyChanged("ServiceId"); } } }

        private string _service;
        [DataMember]
        public string Service { get { return _service; } set { if (!object.ReferenceEquals(this.Service, value)) { _service = value; RaisePropertyChanged("Service"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private decimal? _price;
        [DataMember]
        public decimal? Price { get { return _price; } set { if (!this.Price.Equals(value)) { _price = value; RaisePropertyChanged("Price"); } } }

        private string _unit;
        [DataMember]
        public string Unit { get { return _unit; } set { if (!object.ReferenceEquals(this.Unit, value)) { _unit = value; RaisePropertyChanged("Unit"); } } }


        #endregion
    }
}




