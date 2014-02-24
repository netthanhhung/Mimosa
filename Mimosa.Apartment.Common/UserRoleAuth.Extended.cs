using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{
    public partial class UserRoleAuth : Record
    {
        public static class ColumnNamesExtended
        {            
            public const string UserName = "UserName";
            public const string RoleName = "RoleName";
            public const string IsCustom = "IsCustom";

            public const string SiteGroup = "SiteGroup";
            public const string Site = "Site";
        }

        private string _userName;
        [DataMember]
        public string UserName { get { return _userName; } set { if (!object.ReferenceEquals(this.UserName, value)) { _userName = value; RaisePropertyChanged("UserName"); } } }

        private string _roleName;
        [DataMember]
        public string RoleName { get { return _roleName; } set { if (!object.ReferenceEquals(this.RoleName, value)) { _roleName = value; RaisePropertyChanged("RoleName"); } } }

        private bool _isCustom;
        [DataMember]
        public bool IsCustom { get { return _isCustom; } set { if (this.IsCustom != value) { _isCustom = value; RaisePropertyChanged("IsCustom"); } } }
        
        private string _siteGroup;
        [DataMember]
		public string SiteGroup { get { return _siteGroup; } set { if (!object.ReferenceEquals(this.SiteGroup, value)) { _siteGroup = value; RaisePropertyChanged("SiteGroup"); } } }
		
        private string _site;
        [DataMember]
		public string Site { get { return _site; } set { if (!object.ReferenceEquals(this.Site, value)) { _site = value; RaisePropertyChanged("Site"); } } }

		private bool? _writeRight;
        [DataMember]
        public bool? WriteRight { get { return _writeRight; } set { if (this.WriteRight != value) { _writeRight = value; RaisePropertyChanged("WriteRight"); } } }

		private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (this.IsDeleted != value) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }
		
		private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (this.IsChanged != value) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

    }
}
