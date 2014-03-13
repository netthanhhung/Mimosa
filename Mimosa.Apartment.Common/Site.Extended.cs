using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    public partial class Site : Record
    {
        public static class ColumnNamesExtended
        {
            public const string CanDelete = "CanDelete";
        }

        /*  ======================================================================            
         *      CLASS MEMBERS
         *  ====================================================================== */
        private bool _canDelete;
        [DataMember]
        public bool CanDelete { get { return _canDelete; } set { if (!this.CanDelete.Equals(value)) { _canDelete = value; RaisePropertyChanged("CanDelete"); } } }
        
        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (!this.IsChanged.Equals(value)) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

        private bool _isChecked;
        [DataMember]
        public bool IsChecked { get { return _isChecked; } set { if (!this.IsChecked.Equals(value)) { _isChecked = value; RaisePropertyChanged("IsChecked"); } } }

        private ContactInformation _contactInformation;
        [DataMember]
        public ContactInformation ContactInformation { get { return _contactInformation; } set { if (!object.ReferenceEquals(this.ContactInformation, value)) { _contactInformation = value; RaisePropertyChanged("ContactInformation"); } } }
      
    }
}
