using System.Windows.Media;
using System.ComponentModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class RoleComponentVM : INotifyPropertyChanged
    {        
        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        public string Name { get; set; }
        public Visibility TextVisibility
        {
            get
            {
                return (RoleComponentChildVMList != null && RoleComponentChildVMList.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility CheckVisibility
        {
            get
            {
                return (RoleComponentChildVMList != null && RoleComponentChildVMList.Count > 0) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        public List<RoleComponentVM> RoleComponentChildVMList { get; set; }


        public int RoleComponentPermissionId { get; set; }
        private bool _hasAccess;
        public bool HasAccess
        {
            get { return _hasAccess; }
            set { if (this.HasAccess != value) { _hasAccess = value; RaisePropertyChanged("HasAccess"); } }
        }

        private bool _hasWriteRight;
        public bool HasWriteRight
        {
            get { return _hasWriteRight; }
            set { if (this.HasWriteRight != value) { _hasWriteRight = value; RaisePropertyChanged("HasWriteRight"); } }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { if (this.IsExpanded != value) { _isExpanded = value; RaisePropertyChanged("IsExpanded"); } }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { if (this.IsEnabled != value) { _isEnabled = value; RaisePropertyChanged("IsEnabled"); } }
        }

        public int ComponentId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleIdCompIdString
        {
            get
            {
                return RoleId.ToString() + "," + ComponentId.ToString();
            }
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
    }

}
