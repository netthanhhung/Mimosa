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

        private string _siteName;
        [DataMember]
        public string SiteName { get { return _siteName; } set { if (!object.ReferenceEquals(this.SiteName, value)) { _siteName = value; RaisePropertyChanged("SiteName"); } } }

        private string _roomName;
        [DataMember]
        public string RoomName { get { return _roomName; } set { if (!object.ReferenceEquals(this.SiteName, value)) { _roomName = value; RaisePropertyChanged("RoomName"); } } }

        #endregion
    }
}
