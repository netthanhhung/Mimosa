using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Room : Record
    {
        #region Public Constructors

        public Room() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RoomId = "RoomId";
            public const string SiteId = "SiteId";
            public const string RoomName = "RoomName";
            public const string RoomStatusId = "RoomStatusId";
            public const string RoomTypeId = "RoomTypeId";
            public const string IsLegacy = "IsLegacy";
            public const string Description = "Description";
            public const string Width = "Width";
            public const string Height = "Height";
            public const string MeterSquare = "MeterSquare";
            public const string Floor = "Floor";
            public const string BasePrice = "BasePrice";

            public const string RoomStatus = "RoomStatus";
            public const string RoomType = "RoomType";          
        }

        #endregion

        #region Properties

        private int _RoomId;
        [DataMember]
        public int RoomId { get { return _RoomId; } set { _RoomId = value; RaisePropertyChanged("RoomId"); } }

        private int _siteId;
        [DataMember]
        public int SiteId { get { return _siteId; } set { if (!this.SiteId.Equals(value)) { _siteId = value; RaisePropertyChanged("SiteId"); } } }

        private string _roomName;
        [DataMember]
        public string RoomName { get { return _roomName; } set { if (!object.ReferenceEquals(this.RoomName, value)) { _roomName = value; RaisePropertyChanged("RoomName"); } } }

        private int _roomStatusId;
        [DataMember]
        public int RoomStatusId { get { return _roomStatusId; } set { if (!this.RoomStatusId.Equals(value)) { _roomStatusId = value; RaisePropertyChanged("RoomStatusId"); } } }

        private string _roomStatus;
        [DataMember]
        public string RoomStatus { get { return _roomStatus; } set { if (!object.ReferenceEquals(this.RoomStatus, value)) { _roomStatus = value; RaisePropertyChanged("RoomStatus"); } } }

        private int _roomTypeId;
        [DataMember]
        public int RoomTypeId { get { return _roomTypeId; } set { if (!this.RoomTypeId.Equals(value)) { _roomTypeId = value; RaisePropertyChanged("RoomTypeId"); } } }

        private string _roomType;
        [DataMember]
        public string RoomType { get { return _roomType; } set { if (!object.ReferenceEquals(this.RoomType, value)) { _roomType = value; RaisePropertyChanged("RoomType"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private decimal? _width;
        [DataMember]
        public decimal? Width { get { return _width; } set { if (!this.Width.Equals(value)) { _width = value; RaisePropertyChanged("Width"); } } }

        private decimal? _height;
        [DataMember]
        public decimal? Height { get { return _height; } set { if (!this.Height.Equals(value)) { _height = value; RaisePropertyChanged("Height"); } } }

        private decimal? _meterSquare;
        [DataMember]
        public decimal? MeterSquare { get { return _meterSquare; } set { if (!this.MeterSquare.Equals(value)) { _meterSquare = value; RaisePropertyChanged("MeterSquare"); } } }

        private int? _floor;
        [DataMember]
        public int? Floor { get { return _floor; } set { if (!this.Floor.Equals(value)) { _floor = value; RaisePropertyChanged("Floor"); } } }

        private decimal? _basePrice;
        [DataMember]
        public decimal? BasePrice { get { return _basePrice; } set { if (!this.BasePrice.Equals(value)) { _basePrice = value; RaisePropertyChanged("BasePrice"); } } }

        private List<RoomEquipment> _roomEquipments;
        [DataMember]
        public List<RoomEquipment> RoomEquipments { get { return _roomEquipments; } set { if (!object.ReferenceEquals(this.RoomEquipments, value)) { _roomEquipments = value; RaisePropertyChanged("RoomEquipments"); } } }

        private List<RoomService> _roomServices;
        [DataMember]
        public List<RoomService> RoomServices { get { return _roomServices; } set { if (!object.ReferenceEquals(this.RoomServices, value)) { _roomServices = value; RaisePropertyChanged("RoomServices"); } } }

        #endregion
    }
}




