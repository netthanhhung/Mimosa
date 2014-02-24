using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
	public partial class ContactInformation : Record
	{
        public static class ColumnNames
        {
            public const string ContactInformationId = "ContactInformationID";
            public const string ContactTypeId = "ContactTypeId";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string Address = "Address";
            public const string Address2 = "Address2";
            public const string City = "City";
            public const string State = "State";
            public const string Postcode = "Postcode";
            public const string CountryId = "CountryID";
            public const string PhoneNumber = "PhoneNumber";
            public const string FaxNumber = "FaxNumber";
            public const string Email = "Email";

        }

		#region Public Constructors
		public ContactInformation() : base () {}
		#endregion

		#region Public Properties

        [DataMember]
        public int ContactInformationId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("ContactInformationId"); } }

        private int _contactTypeId;
        [DataMember]
        public int ContactTypeId { get { return _contactTypeId; } set { if (!this.ContactTypeId.Equals(value)) { _contactTypeId = value; RaisePropertyChanged("ContactTypeId"); } } }

        private string _firstName;
        [DataMember]
        public string FirstName { get { return _firstName; } set { if (!object.ReferenceEquals(this.FirstName, value)) { _firstName = value; RaisePropertyChanged("FirstName"); } } }

        private string _lastName;
        [DataMember]
        public string LastName { get { return _lastName; } set { if (!object.ReferenceEquals(this.LastName, value)) { _lastName = value; RaisePropertyChanged("LastName"); } } }

        private string _address;
        [DataMember]
        public string Address { get { return _address; } set { if (!object.ReferenceEquals(this.Address, value)) { _address = value; RaisePropertyChanged("Address"); } } }

        private string _address2;
        [DataMember]
        public string Address2 { get { return _address2; } set { if (!object.ReferenceEquals(this.Address2, value)) { _address2 = value; RaisePropertyChanged("Address2"); } } }

        private string _city;
        [DataMember]
        public string City { get { return _city; } set { if (!object.ReferenceEquals(this.City, value)) { _city = value; RaisePropertyChanged("City"); } } }

        private string _state;
        [DataMember]
        public string State { get { return _state; } set { if (!object.ReferenceEquals(this.State, value)) { _state = value; RaisePropertyChanged("State"); } } }

        private string _postcode;
        [DataMember]
        public string Postcode { get { return _postcode; } set { if (!object.ReferenceEquals(this.Postcode, value)) { _postcode = value; RaisePropertyChanged("Postcode"); } } }

        private int _countryId;
        [DataMember]
        public int CountryId { get { return _countryId; } set { if (!this.CountryId.Equals(value)) { _countryId = value; RaisePropertyChanged("CountryId"); } } }

        private string _phoneNumber;
        [DataMember]
        public string PhoneNumber { get { return _phoneNumber; } set { if (!object.ReferenceEquals(this.PhoneNumber, value)) { _phoneNumber = value; RaisePropertyChanged("PhoneNumber"); } } }

        private string _faxNumber;
        [DataMember]
        public string FaxNumber { get { return _faxNumber; } set { if (!object.ReferenceEquals(this.FaxNumber, value)) { _faxNumber = value; RaisePropertyChanged("FaxNumber"); } } }

        private string _email;
        [DataMember]
        public string Email { get { return _email; } set { if (!object.ReferenceEquals(this.Email, value)) { _email = value; RaisePropertyChanged("Email"); } } }

		#endregion
	}
}
