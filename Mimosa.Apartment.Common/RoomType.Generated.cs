using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class RoomType : Record
    {
        #region Public Constructors

        public RoomType() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RoomTypeId = "RoomTypeId";
            public const string Name = "Name";
            public const string OrganisationId = "OrganisationId";
            public const string SiteId = "SiteId";
            public const string Description = "Description";
            public const string IsLegacy = "IsLegacy";
        }

        #endregion

        #region Properties

        private int _roomTypeId;
        [DataMember]
        public int RoomTypeId { get { return _roomTypeId; } set { _roomTypeId = value; RaisePropertyChanged("RoomTypeId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        private int? _organisationId;
        [DataMember]
        public int? OrganisationId { get { return _organisationId; } set { if (!this.OrganisationId.Equals(value)) { _organisationId = value; RaisePropertyChanged("OrganisationId"); } } }

        private int? _siteId;
        [DataMember]
        public int? SiteId { get { return _siteId; } set { if (!this.SiteId.Equals(value)) { _siteId = value; RaisePropertyChanged("SiteId"); } } }

        private string _description;
        [DataMember]
        public string Description { get { return _description; } set { if (!object.ReferenceEquals(this.Description, value)) { _description = value; RaisePropertyChanged("Description"); } } }

        private bool _isLegacy;
        [DataMember]
        public bool IsLegacy { get { return _isLegacy; } set { if (!this.IsLegacy.Equals(value)) { _isLegacy = value; RaisePropertyChanged("IsLegacy"); } } }


        #endregion
    }
}




