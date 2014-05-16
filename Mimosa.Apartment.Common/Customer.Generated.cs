using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Customer : Record
    {
        #region Public Constructors

        public Customer() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string CustomerId = "CustomerId";
            public const string OrganisationId = "OrganisationID";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string IsLegacy = "IsLegacy";
            public const string Gender = "Gender";
            public const string Age = "Age";
            public const string ContactInformationId = "ContactInformationId";

        }

        #endregion

        #region Properties

        [DataMember]
        public int CustomerId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("CustomerId"); } }

        private int _organisationId;
        [DataMember]
        public int OrganisationId { get { return _organisationId; } set { if (!this.OrganisationId.Equals(value)) { _organisationId = value; RaisePropertyChanged("OrganisationId"); } } }

        private string _firstName;
        [DataMember]
        public string FirstName { get { return _firstName; } set { if (!object.ReferenceEquals(this.FirstName, value)) { _firstName = value; RaisePropertyChanged("FirstName"); } } }

        private string _lastName;
        [DataMember]
        public string LastName { get { return _lastName; } set { if (!object.ReferenceEquals(this.LastName, value)) { _lastName = value; RaisePropertyChanged("LastName"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }

        private int? _gender;
        [DataMember]
        public int? Gender { get { return _gender; } set { if (!object.ReferenceEquals(this.Gender, value)) { _gender = value; RaisePropertyChanged("Gender"); } } }

        private int? _age;
        [DataMember]
        public int? Age { get { return _age; } set { if (!object.ReferenceEquals(this.Age, value)) { _age = value; RaisePropertyChanged("Age"); } } }

        private int? _contactInformationId;
        [DataMember]
        public int? ContactInformationId { get { return _contactInformationId; } set { if (!object.ReferenceEquals(this.ContactInformationId, value)) { _contactInformationId = value; RaisePropertyChanged("ContactInformationId"); } } }

        #endregion

    }
}
