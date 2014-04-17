using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Booking : Record
    {
        #region Public Constructors

        public Booking() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string BookingId = "BookingId";
            public const string SiteId = "SiteId";
            public const string SiteName = "SiteName";
            public const string RoomId = "RoomId";
            public const string RoomName = "RoomName";
            public const string CustomerId = "CustomerId";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string CustomerName = "CustomerName";
            public const string BookDate = "BookDate";
            public const string BookingStatusId = "BookingStatusId";
            public const string BookingStatus = "BookingStatus";
            public const string Description = "Description";
            public const string RoomPrice = "RoomPrice";
            public const string TotalPrice = "TotalPrice";
            public const string ContractDateSign = "ContractDateSign";
            public const string ContractDateStart = "ContractDateStart";
            public const string ContractDateEnd = "ContractDateEnd";
            public const string ContractTotalPrice = "ContractTotalPrice";  
        }

        #endregion

        #region Properties

        private int _bookingId;
        [DataMember]
        public int BookingId { get { return _bookingId; } set { _bookingId = value; RaisePropertyChanged("BookingId"); } }

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

        private int _customerId;
        [DataMember]
        public int CustomerId { get { return _customerId; } set { if (!this.CustomerId.Equals(value)) { _customerId = value; RaisePropertyChanged("CustomerId"); } } }

        private string _CustomerName;
        [DataMember]
        public string CustomerName { get { return _CustomerName; } set { if (!object.ReferenceEquals(this.CustomerName, value)) { _CustomerName = value; RaisePropertyChanged("CustomerName"); } } }

        private string _firstName;
        [DataMember]
        public string FirstName { get { return _firstName; } set { if (!object.ReferenceEquals(this.FirstName, value)) { _firstName = value; RaisePropertyChanged("FirstName"); } } }

        private string _lastName;
        [DataMember]
        public string LastName { get { return _lastName; } set { if (!object.ReferenceEquals(this.LastName, value)) { _lastName = value; RaisePropertyChanged("LastName"); } } }

        private DateTime _bookDate;
        [DataMember]
        public DateTime BookDate { get { return _bookDate; } set { if (!this.BookDate.Equals(value)) { _bookDate = value; RaisePropertyChanged("BookDate"); } } }

        private int _bookingStatusId;
        [DataMember]
        public int BookingStatusId { get { return _bookingStatusId; } set { if (!this.BookingStatusId.Equals(value)) { _bookingStatusId = value; RaisePropertyChanged("BookingStatusId"); } } }

        private string _bookingStatus;
        [DataMember]
        public string BookingStatus { get { return _bookingStatus; } set { if (!object.ReferenceEquals(this.BookingStatus, value)) { _bookingStatus = value; RaisePropertyChanged("BookingStatus"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }


        private decimal? _roomPrice;
        [DataMember]
        public decimal? RoomPrice { get { return _roomPrice; } set { if (!this.RoomPrice.Equals(value)) { _roomPrice = value; RaisePropertyChanged("RoomPrice"); } } }

        private decimal? _totalPrice;
        [DataMember]
        public decimal? TotalPrice { get { return _totalPrice; } set { if (!this.TotalPrice.Equals(value)) { _totalPrice = value; RaisePropertyChanged("TotalPrice"); } } }

        private DateTime? _contractDateSign;
        [DataMember]
        public DateTime? ContractDateSign { get { return _contractDateSign; } set { if (!this.ContractDateSign.Equals(value)) { _contractDateSign = value; RaisePropertyChanged("ContractDateSign"); } } }

        private DateTime? _contractDateStart;
        [DataMember]
        public DateTime? ContractDateStart { get { return _contractDateStart; } set { if (!this.ContractDateStart.Equals(value)) { _contractDateStart = value; RaisePropertyChanged("ContractDateStart"); } } }

        private DateTime? _contractDateEnd;
        [DataMember]
        public DateTime? ContractDateEnd { get { return _contractDateEnd; } set { if (!this.ContractDateEnd.Equals(value)) { _contractDateEnd = value; RaisePropertyChanged("ContractDateEnd"); } } }

        private decimal? _contractTotalPrice;
        [DataMember]
        public decimal? ContractTotalPrice { get { return _contractTotalPrice; } set { if (!this.ContractTotalPrice.Equals(value)) { _contractTotalPrice = value; RaisePropertyChanged("ContractTotalPrice"); } } }

        private List<BookingRoomEquipment> _bookingEquipments;
        [DataMember]
        public List<BookingRoomEquipment> BookingEquipments { get { return _bookingEquipments; } set { if (!object.ReferenceEquals(this.BookingEquipments, value)) { _bookingEquipments = value; RaisePropertyChanged("BookingEquipments"); } } }

        private List<BookingRoomService> _bookingServices;
        [DataMember]
        public List<BookingRoomService> BookingServices { get { return _bookingServices; } set { if (!object.ReferenceEquals(this.BookingServices, value)) { _bookingServices = value; RaisePropertyChanged("BookingServices"); } } }

        private Customer _customerItem;
        [DataMember]
        public Customer CustomerItem { get { return _customerItem; } set { if (!object.ReferenceEquals(this.CustomerItem, value)) { _customerItem = value; RaisePropertyChanged("CustomerItem"); } } }

        #endregion
    }
}




