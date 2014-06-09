using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class BookingRoomEquipment : Record
    {
        #region Public Constructors

        public BookingRoomEquipment() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string BookingRoomEquipmentId = "BookingRoomEquipmentId";
            public const string BookingId = "BookingId";
            public const string EquipmentId = "EquipmentId";
            public const string Equipment = "Equipment";
            public const string Description = "Description";
            public const string Price = "Price";
            public const string Unit = "Unit";
        }

        #endregion

        #region Properties

        private int _bookingRoomEquipmentId;
        [DataMember]
        public int BookingRoomEquipmentId { get { return _bookingRoomEquipmentId; } set { _bookingRoomEquipmentId = value; RaisePropertyChanged("BookingRoomEquipmentId"); } }

        private int _bookingId;
        [DataMember]
        public int BookingId { get { return _bookingId; } set { if (!this.BookingId.Equals(value)) { _bookingId = value; RaisePropertyChanged("BookingId"); } } }

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

        private string _unit;
        [DataMember]
        public string Unit { get { return _unit; } set { if (!object.ReferenceEquals(this.Unit, value)) { _unit = value; RaisePropertyChanged("Unit"); } } }


        #endregion
    }
}




