using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class MultiRoomTypePicker : UserControl
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal event EventHandler InitComplete;

        public double Width
        {
            get { return uiMultiRoomTypes.Width; }
            set { uiMultiRoomTypes.Width = value; }
        }

        public List<RoomType> RoomTypeList
        {
            get { return uiMultiRoomTypes.ItemsSource as List<RoomType>; }
        }

        public string SelectedRoomTypeIds
        {
            get
            {
                string roomTypeIDs = string.Empty;
                List<RoomType> itemsSource = uiMultiRoomTypes.ItemsSource as List<RoomType>;
                foreach (RoomType s in itemsSource)
                {
                    if (s.IsChecked == true)
                    {
                        roomTypeIDs += s.RoomTypeId.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(roomTypeIDs))
                {
                    roomTypeIDs = roomTypeIDs.Substring(0, roomTypeIDs.Length - 1);
                }
                return roomTypeIDs;
            }
            set { }
        }

        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        public List<UserRoleAuth> UserRoleAuths { get; set; }
        
        public MultiRoomTypePicker()
        {
            InitializeComponent();
            
        }

        internal void Init()
        {
            if (uiMultiRoomTypes.ItemsSource == null)
            {
                if (this.UserRoleAuths == null || this.UserRoleAuths.Count(i => !i.SiteGroupId.HasValue) > 0)
                {
                    DataServiceHelper.ListRoomTypeAsync(Globals.UserLogin.UserOrganisationId, null, false, SelectRoomTypesComplete);
                }
                else
                {
                    int siteGroupId = this.UserRoleAuths.First(i => i.SiteGroupId.HasValue).SiteGroupId.Value;
                    DataServiceHelper.ListRoomTypeAsync(Globals.UserLogin.UserOrganisationId, null, false, SelectRoomTypesComplete);
                }
            }
            else
            {
                RaiseInitCompleteEvent();
            }
        }

        internal void SelectRoomTypesComplete(List<RoomType> itemsSource)
        {
            int originalCount = itemsSource.Count;
            itemsSource.ForEach(i => i.IsChecked = true);
            uiMultiRoomTypes.ItemsSource = itemsSource;
            uiMultiRoomTypes.SelectedIndex = 0;
            RaiseInitCompleteEvent();
        }



        private void RaiseInitCompleteEvent()
        {
            if (InitComplete != null)
            {
                InitComplete(this, EventArgs.Empty);
            }
        }
    }
}
