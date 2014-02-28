using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Mimosa.Apartment.Common
{
    public partial class RoleComponentPermission : Record
    {
        public static class ColumnNamesExtended
        {            
            public const string RoleName = "RoleName";
            public const string IsCustom = "IsCustom";
            public const string ComponentName = "ComponentName";

        }

        private string _roleName;
        [DataMember]
        public string RoleName { get { return _roleName; } set { if (!object.ReferenceEquals(this.RoleName, value)) { _roleName = value; RaisePropertyChanged("RoleName"); } } }

        private bool _isCustom;
        [DataMember]
        public bool IsCustom { get { return _isCustom; } set { if (this.IsCustom != value) { _isCustom = value; RaisePropertyChanged("IsCustom"); } } }

        private string _componentName;
        [DataMember]
        public string ComponentName { get { return _componentName; } set { if (!object.ReferenceEquals(this.ComponentName, value)) { _componentName = value; RaisePropertyChanged("ComponentName"); } } }

        private bool _isDeleted;
        [DataMember]
        public bool IsDeleted { get { return _isDeleted; } set { if (this.IsDeleted != value) { _isDeleted = value; RaisePropertyChanged("IsDeleted"); } } }
		
		private bool _isChanged;
        [DataMember]
        public bool IsChanged { get { return _isChanged; } set { if (this.IsChanged != value) { _isChanged = value; RaisePropertyChanged("IsChanged"); } } }

    }
}
