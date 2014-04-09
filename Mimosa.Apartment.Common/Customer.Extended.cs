using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Mimosa.Apartment.Common
{
    public partial class Customer
    {
        #region Properties

        private ContactInformation _contactInformation;
        [DataMember]
        public ContactInformation ContactInformation { get { return _contactInformation; } set { if (!object.ReferenceEquals(this.ContactInformation, value)) { _contactInformation = value; RaisePropertyChanged("ContactInformation"); } } }
        
        [DataMember]
        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
            set { }
        }

        #endregion
    }
}
