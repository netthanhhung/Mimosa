using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class SiteGroupSite : Record
    {
        #region Public Constructors

        public SiteGroupSite() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string SiteGroupSiteId = "SiteGroupSiteId";
            public const string SiteGroupId = "SiteGroupId";
            public const string SiteId = "SiteId";          
        }

        #endregion

        #region Properties

        private int _siteGroupSiteId;
        [DataMember]
        public int SiteGroupSiteId { get { return _siteGroupSiteId; } set { _siteGroupSiteId = value; RaisePropertyChanged("SiteGroupSiteId"); } }

        private int _siteGroupId;
        [DataMember]
        public int SiteGroupId { get { return _siteGroupId; } set { _siteGroupId = value; RaisePropertyChanged("SiteGroupId"); } }

        private int _siteId;
        [DataMember]
        public int SiteId { get { return _siteId; } set { _siteId = value; RaisePropertyChanged("SiteId"); } }        

        #endregion
    }
}




