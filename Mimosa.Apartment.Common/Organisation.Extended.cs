using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{    
    public partial class Organisation : Record
    {
        
        #region Constructor(s)

        /// <summary>
        /// Initialises a new instance of the Organisation class.
        /// </summary>
        public Organisation() : base() 
        { 
        }

        #endregion        

        #region IRecordStatus Members

        private ContactInformation _contactInformation;
        [DataMember]
        public ContactInformation ContactInformation { get { return _contactInformation; } set { if (!object.ReferenceEquals(this.ContactInformation, value)) { _contactInformation = value; RaisePropertyChanged("ContactInformation"); } } }
      
        private List<UserRoleAuth> _orgAdminUsers;
        [DataMember]
        public List<UserRoleAuth> OrgAdminUsers { get { return _orgAdminUsers; } set { if (!object.ReferenceEquals(this.OrgAdminUsers, value)) { _orgAdminUsers = value; RaisePropertyChanged("OrgAdminUsers"); } } }
      
        private bool _canDelete;
        [DataMember]
        public bool CanDelete { get { return _canDelete; } set { if (!this.CanDelete.Equals(value)) { _canDelete = value; RaisePropertyChanged("CanDelete"); } } }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (!this.IsDeleted.Equals(value)) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }

        private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (!this.IsChanged.Equals(value)) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

        #endregion
    }
}
