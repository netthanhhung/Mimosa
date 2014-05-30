using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Equipment : Record
    {
        #region Public Constructors

        public Equipment() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string EquipmentId = "EquipmentId";
            public const string OrganisationId = "OrganisationID";
            public const string EquipmentName = "EquipmentName";
            public const string Description = "Description";
            public const string IsLegacy = "IsLegacy";
            public const string RealPrice = "RealPrice";
            public const string RentPrice = "RentPrice";
            public const string Unit = "Unit";          
        }

        #endregion

        #region Properties

        private int _equipmentId;
        [DataMember]
        public int EquipmentId { get { return _equipmentId; } set { _equipmentId = value; RaisePropertyChanged("EquipmentId"); } }

        private int _organisationId;
        [DataMember]
        public int OrganisationId { get { return _organisationId; } set { if (!this.OrganisationId.Equals(value)) { _organisationId = value; RaisePropertyChanged("OrganisationId"); } } }

        private string _equipmentName;
        [DataMember]
        public string EquipmentName { get { return _equipmentName; } set { if (!object.ReferenceEquals(this.EquipmentName, value)) { _equipmentName = value; RaisePropertyChanged("EquipmentName"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private decimal? _realPrice;
        [DataMember]
        public decimal? RealPrice { get { return _realPrice; } set { if (!this.RealPrice.Equals(value)) { _realPrice = value; RaisePropertyChanged("RealPrice"); } } }

        private decimal? _rentPrice;
        [DataMember]
        public decimal? RentPrice { get { return _rentPrice; } set { if (!this.RentPrice.Equals(value)) { _rentPrice = value; RaisePropertyChanged("RentPrice"); } } }

        private string _unit;
        [DataMember]
        public string Unit { get { return _unit; } set { if (!object.ReferenceEquals(this.Unit, value)) { _unit = value; RaisePropertyChanged("Unit"); } } }

        #endregion
    }
}




