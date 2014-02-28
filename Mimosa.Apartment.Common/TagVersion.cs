using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class TagVersion : INotifyPropertyChanged
    {
        #region Public Constructors

        public TagVersion() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string TagVersionID = "TagVersionID";
            public const string TagName = "TagName";
            public const string Version = "Version";
            public const string DateCreated = "DateCreated";
            public const string CreatedBy = "CreatedBy";   
        }

        #endregion

        #region Properties
        private int _tagVersionID;
        [DataMember]
        public int TagVersionID { get { return _tagVersionID; } set { if (this.TagVersionID != value) { _tagVersionID = value; RaisePropertyChanged("TagVersionID"); } } }

        private string _tagName;
        [DataMember]
        public string TagName { get { return _tagName; } set { if (!object.ReferenceEquals(this.TagName, value)) { _tagName = value; RaisePropertyChanged("TagName"); } } }

        private string _version;
        [DataMember]
        public string Version { get { return _version; } set { if (!object.ReferenceEquals(this.Version, value)) { _version = value; RaisePropertyChanged("Version"); } } }

        private DateTime? _dateCreated;        
        [DataMember]
        public DateTime? DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private string _createdBy;
        [DataMember]
        public string CreatedBy
        {
            get { return _createdBy ?? string.Empty; }
            set { _createdBy = value; }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion
    }
}




