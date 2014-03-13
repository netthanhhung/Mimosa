using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Site : Record
    {
        #region Public Constructors

        public Site() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string SiteId = "SiteID";
            public const string OrganisationId = "OrganisationID";
            public const string HotelId = "HotelID";
            public const string Name = "Name";            
            public const string AbbreviatedName = "AbbreviatedName";
            public const string ContactInformationID = "ContactInformationID";
            public const string StarRating = "StarRating";
            public const string LicenseKey = "LicenseKey";
            public const string PropCode = "PropCode";
            public const string DisplayIndex = "DisplayIndex";
            public const string IsLegacy = "IsLegacy";
            public const string Availability = "Availability";
            
        }

        #endregion

        #region Properties

        [DataMember]
        public int SiteId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("SiteId"); } }

        private int _organisationId;
        [DataMember]
        public int OrganisationId { get { return _organisationId; } set { if (!this.OrganisationId.Equals(value)) { _organisationId = value; RaisePropertyChanged("OrganisationId"); } } }

        private int? _hotelId;
        [DataMember]
        public int? HotelId { get { return _hotelId; } set { if (!this.HotelId.Equals(value)) { _hotelId = value; RaisePropertyChanged("HotelId"); } } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private string _propCode;
        [DataMember]
        public string PropCode { get { return _propCode; } set { if (!object.ReferenceEquals(this.PropCode, value)) { _propCode = value; RaisePropertyChanged("PropCode"); } } }

        private decimal? _starRating;
        [DataMember]
        public decimal? StarRating { get { return _starRating; } set { if (!this.StarRating.Equals(value)) { _starRating = value; RaisePropertyChanged("StarRating"); } } }

        private int _displayIndex;
        [DataMember]
        public int DisplayIndex { get { return _displayIndex; } set { if (!this.DisplayIndex.Equals(value)) { _displayIndex = value; RaisePropertyChanged("DisplayIndex"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private string _abbreviatedName;
        [DataMember]
        public string AbbreviatedName { get { return _abbreviatedName; } set { if (!object.ReferenceEquals(this.AbbreviatedName, value)) { _abbreviatedName = value; RaisePropertyChanged("AbbreviatedName"); } } }
        
        private Guid? _licenseKey;
        [DataMember]
        public Guid? LicenseKey { get { return _licenseKey; } set { if (!this.LicenseKey.Equals(value)) { _licenseKey = value; RaisePropertyChanged("LicenseKey"); } } }

        private int _availability;
        [DataMember]
        public int Availability { get { return _availability; } set { if (!this.Availability.Equals(value)) { _availability = value; RaisePropertyChanged("Availability"); } } }

        private int? _contactInformationID;
        [DataMember]
        public int? ContactInformationID { get { return _contactInformationID; } set { if (!this.ContactInformationID.Equals(value)) { _contactInformationID = value; RaisePropertyChanged("ContactInformationID"); } } }


        #endregion
    }
}
