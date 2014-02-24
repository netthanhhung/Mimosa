using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    /// <summary>
    /// Defines a Organisation object.
    /// </summary>
    [DataContract]
    public partial class Organisation : Record
    {
        public static class ColumnNames
        {
            public const string OrganisationId = "OrganisationID";
            public const string Name = "Name";
            public const string ContactInformationId = "ContactInformationID";
            public const string DisplayIndex = "DisplayIndex";
            public const string IsLegacy = "IsLegacy";
            public const string AuthorisationCode = "AuthorisationCode";
        }

        #region Public Properties
        
        private int _organisationId;
        [DataMember]
        public int OrganisationId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("OrganisationId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private int _contactInformationId;
        [DataMember]
        public int ContactInformationId { get { return _contactInformationId; } set { if (!this.ContactInformationId.Equals(value)) { _contactInformationId = value; RaisePropertyChanged("ContactInformationId"); } } }

        private int _displayIndex;
        [DataMember]
        public int DisplayIndex { get { return _displayIndex; } set { if (!this.DisplayIndex.Equals(value)) { _displayIndex = value; RaisePropertyChanged("DisplayIndex"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private string _authorisationCode;
        [DataMember]
        public string AuthorisationCode { get { return _authorisationCode; } set { if (!object.ReferenceEquals(this.AuthorisationCode, value)) { _authorisationCode = value; RaisePropertyChanged("AuthorisationCode"); } } }

        #endregion
    }
}
