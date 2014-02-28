using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{
    public partial class SiteGroup : Record
    {
        public static class ColumnNamesExtended
        {
            public const string CanDelete = "CanDelete";
        }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (this.IsDeleted != value) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }

        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (this.IsChanged != value) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }
        
        private List<Site> _sites;
        [DataMember]
        public List<Site> Sites { get { return _sites; } set { if (!object.ReferenceEquals(this.Sites, value)) { _sites = value; RaisePropertyChanged("Sites"); } } }

        private bool _canDelete;
        [DataMember]
        public bool CanDelete { get { return _canDelete; } set { if (this.CanDelete != value) { _canDelete = value; RaisePropertyChanged("CanDelete"); } } }

    }
}
