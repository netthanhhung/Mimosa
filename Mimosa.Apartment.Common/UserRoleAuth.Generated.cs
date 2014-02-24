using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class UserRoleAuth : Record
    {
        #region Public Constructors

        public UserRoleAuth() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string UserRoleAuthId = "UserRoleAuthId";
            public const string UserId = "UserId";
            public const string RoleId = "RoleId";
            public const string WholeOrg = "WholeOrg";
            public const string SiteGroupId = "SiteGroupId";
            public const string SiteId = "SiteId";
        }

        #endregion

        #region Properties

        private int _userRoleAuthId;
        [DataMember]
        public int UserRoleAuthId { get { return _userRoleAuthId; } set { _userRoleAuthId = value; RaisePropertyChanged("UserRoleAuthId"); } }

        private Guid _userId;
        [DataMember]
        public Guid UserId { get { return _userId; } set { if (this.UserId != value) { _userId = value; RaisePropertyChanged("UserId"); } } }

        private Guid _roleId;
        [DataMember]
        public Guid RoleId { get { return _roleId; } set { if (this.RoleId != value) { _roleId = value; RaisePropertyChanged("RoleId"); } } }
        
        private bool? _wholeOrg;
        [DataMember]
        public bool? WholeOrg { get { return _wholeOrg; } set { if (this.WholeOrg != value) { _wholeOrg = value; RaisePropertyChanged("WholeOrg"); } } }

        private int? _siteGroupId;
        [DataMember]
        public int? SiteGroupId { get { return _siteGroupId; } set { if (this.SiteGroupId != value) { _siteGroupId = value; RaisePropertyChanged("SiteGroupId"); } } }

        private int? _siteId;
        [DataMember]
        public int? SiteId { get { return _siteId; } set { if (this.SiteId != value) { _siteId = value; RaisePropertyChanged("SiteId"); } } }

        #endregion
    }
}




