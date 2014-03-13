using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class Component : Record
    {
        #region Public Constructors

        public Component() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ComponentId = "ComponentId";
            public const string Name = "Name";           
        }

        #endregion

        #region Properties

        private int _componentId;
        [DataMember]
        public int ComponentId { get { return _componentId; } set { _componentId = value; RaisePropertyChanged("ComponentId"); } }

        private string _name;
        [DataMember]
        public string Name { get { return _name; } set { if (!object.ReferenceEquals(this.Name, value)) { _name = value; RaisePropertyChanged("Name"); } } }

        #endregion
    }
}




