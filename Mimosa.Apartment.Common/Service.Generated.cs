using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Service : Record
    {
        #region Public Constructors

        public Service() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ServiceId = "ServiceId";
            public const string OrganisationId = "OrganisationID";
            public const string Name = "Name";
            public const string Description = "Description";
            public const string IsLegacy = "IsLegacy";
            public const string Price = "Price";
            public const string Unit = "Unit";    
        }

        #endregion

        #region Properties

        private int _serviceId;
        [DataMember]
        public int ServiceId { get { return _serviceId; } set { _serviceId = value; RaisePropertyChanged("ServiceId"); } }

        private int _organisationId;
        [DataMember]
        public int OrganisationId { get { return _organisationId; } set { if (!this.OrganisationId.Equals(value)) { _organisationId = value; RaisePropertyChanged("OrganisationId"); } } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private decimal? _price;
        [DataMember]
        public decimal? Price { get { return _price; } set { if (!this.Price.Equals(value)) { _price = value; RaisePropertyChanged("Price"); } } }

        private string _unit;
        [DataMember]
        public string Unit { get { return _unit; } set { if (!object.ReferenceEquals(this.Unit, value)) { _unit = value; RaisePropertyChanged("Unit"); } } }

        #endregion
    }
}




