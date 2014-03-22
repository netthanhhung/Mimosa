using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class RoomEquipment : Record
    {
        #region Public Constructors

        public RoomEquipment() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RoomEquipmentId = "RoomEquipmentId";
            public const string RoomId = "RoomId";
            public const string EquipmentId = "EquipmentId";
            public const string Equipment = "Equipment";
            public const string Description = "Description";
            public const string Price = "Price";
        }

        #endregion

        #region Properties

        private int _RoomEquipmentId;
        [DataMember]
        public int RoomEquipmentId { get { return _RoomEquipmentId; } set { _RoomEquipmentId = value; RaisePropertyChanged("RoomEquipmentId"); } }

        private int _roomId;
        [DataMember]
        public int RoomId { get { return _roomId; } set { if (!this.RoomId.Equals(value)) { _roomId = value; RaisePropertyChanged("RoomId"); } } }

        private int _equipmentId;
        [DataMember]
        public int EquipmentId { get { return _equipmentId; } set { if (!this.EquipmentId.Equals(value)) { _equipmentId = value; RaisePropertyChanged("EquipmentId"); } } }

        private string _equipment;
        [DataMember]
        public string Equipment { get { return _equipment; } set { if (!object.ReferenceEquals(this.Equipment, value)) { _equipment = value; RaisePropertyChanged("Equipment"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }
        
        private decimal? _price;
        [DataMember]
        public decimal? Price { get { return _price; } set { if (!this.Price.Equals(value)) { _price = value; RaisePropertyChanged("Price"); } } }

        

        #endregion
    }
}




