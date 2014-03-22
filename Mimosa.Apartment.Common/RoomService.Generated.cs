using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class RoomService : Record
    {
        #region Public Constructors

        public RoomService() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RoomServiceId = "RoomServiceId";
            public const string RoomId = "RoomId";
            public const string ServiceId = "ServiceId";
            public const string Service = "Service";
            public const string Description = "Description";
            public const string Price = "Price";
        }

        #endregion

        #region Properties

        private int _roomServiceId;
        [DataMember]
        public int RoomServiceId { get { return _roomServiceId; } set { _roomServiceId = value; RaisePropertyChanged("RoomServiceId"); } }

        private int _roomId;
        [DataMember]
        public int RoomId { get { return _roomId; } set { if (!this.RoomId.Equals(value)) { _roomId = value; RaisePropertyChanged("RoomId"); } } }

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

        

        #endregion
    }
}




