using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
	public partial class District : Record
	{
        public class ColumnNames
        {
            public const string DistrictId = "DistrictId";
            public const string CityId = "CityId";
            public const string Name = "Name";
        }

		#region Public Constructors
		public District() : base () {}
		#endregion

		#region Public Properties

        [DataMember]
        public int DistrictId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("DistrictId"); } }

        private int _cityId;
        [DataMember]
        public int CityId { get { return _cityId; } set { if (!this.CityId.Equals(value)) { _cityId = value; RaisePropertyChanged("CityId"); } } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

		#endregion
	}
}
