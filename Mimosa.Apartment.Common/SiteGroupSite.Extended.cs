using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{
    public partial class SiteGroupSite : Record
    {
        public static class ColumnNamesExtended
        {
            public const string SiteName = "SiteName";
            public const string GroupName = "GroupName"; 
        }

        private string _groupName;
        [DataMember]
        public string GroupName { get { return _groupName; } set { if (this.GroupName != value) { _groupName = value; RaisePropertyChanged("GroupName"); } } }

        private string _siteName;
        [DataMember]
        public string SiteName { get { return _siteName; } set { if (this.SiteName != value) { _siteName = value; RaisePropertyChanged("SiteName"); } } }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (this.IsDeleted != value) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }

        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (this.IsChanged != value) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

    }
}
