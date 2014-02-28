using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class SiteGroup : Record
    {
        #region Public Constructors

        public SiteGroup() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string SiteGroupId = "SiteGroupId";
            public const string GroupName = "GroupName";           
        }

        #endregion

        #region Properties

        private int _siteGroupId;
        [DataMember]
        public int SiteGroupId { get { return _siteGroupId; } set { _siteGroupId = value; RaisePropertyChanged("SiteGroupId"); } }

		private string _groupName;
        [DataMember]
		public string GroupName { get { return _groupName; } set { if (!object.ReferenceEquals(this.GroupName, value)) { _groupName = value; RaisePropertyChanged("GroupName"); } } }

        #endregion
    }
}




