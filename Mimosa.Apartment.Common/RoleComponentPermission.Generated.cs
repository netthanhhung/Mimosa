using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Mimosa.Apartment.Common
{
    [DataContract]
    public partial class RoleComponentPermission : Record
    {
        #region Public Constructors

        public RoleComponentPermission() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string RoleComponentPermissionId = "RoleComponentPermissionId";
            public const string RoleId = "RoleId";
            public const string ComponentId = "ComponentId";
            public const string WriteRight = "WriteRight";            
        }

        #endregion

        #region Properties

        private int _roleComponentPermissionId;
        [DataMember]
        public int RoleComponentPermissionId { get { return _roleComponentPermissionId; } set { _roleComponentPermissionId = value; RaisePropertyChanged("RoleComponentPermissionId"); } }

        private Guid _roleId;
        [DataMember]
        public Guid RoleId { get { return _roleId; } set { if (this.RoleId != value) { _roleId = value; RaisePropertyChanged("RoleId"); } } }

        private int _componentId;
        [DataMember]
        public int ComponentId { get { return _componentId; } set { if (this.ComponentId != value) { _componentId = value; RaisePropertyChanged("ComponentId"); } } }

        private bool? _writeRight;
        [DataMember]
        public bool? WriteRight { get { return _writeRight; } set { if (this.WriteRight != value) { _writeRight = value; RaisePropertyChanged("WriteRight"); } } }

        #endregion
    }
}




