using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
	public partial class City : Record
	{
        public class ColumnNames
        {
            public const string CityId = "CityId";
            public const string CountryId = "CountryId";
            public const string Name = "Name";
        }

		#region Public Constructors
		public City() : base () {}
		#endregion

		#region Public Properties

        [DataMember]
        public int CityId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("CityId"); } }

        private int _countryId;
        [DataMember]
        public int CountryId { get { return _countryId; } set { if (!this.CountryId.Equals(value)) { _countryId = value; RaisePropertyChanged("CountryId"); } } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

		#endregion
	}
}
