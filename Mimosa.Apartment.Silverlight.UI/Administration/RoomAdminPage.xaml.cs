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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class RoomAdminPage : Page
    {
        private static class UserMessages
        {
            internal const string DateNotValid = "Date End must be greater than Date Start";
            internal const string DateIntersect = "There is an intersect between date ranges";

        }

        private int _newId = -1;
        private int _selectedRoomId = -1;
        private List<Room> _originalItemSource = new List<Room>();
        private List<RoomEquipment> _currentRoomEquipments = new List<RoomEquipment>();
        private List<RoomService> _currentRoomServices = new List<RoomService>();
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        public RoomAdminPage()
        {
            InitializeComponent();
            UserRoleAuths = ucSitePicker.UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.RoomAdmin);
            if (this.UserRoleAuths == null)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }
            btnSaveRoom.IsEnabled = btnSaveRoomEquipment.IsEnabled = btnSaveRoomService.IsEnabled = this.UserRoleAuths.Count(i => i.WriteRight == true) > 0;
            ucImageUpload.IsReadOnly = !btnSaveRoom.IsEnabled;

            //ucSitePicker.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ucSitePicker_SelectionChanged);
            ucSitePicker.Init();
            ucSitePicker.InitComplete += new EventHandler(ucSitePicker_InitComplete);
            ucRoomTypes.Init();
            ucRoomTypes.InitComplete += new EventHandler(ucRoomTypes_InitComplete);
            gvwRoom.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwRoom_AddingNewDataItem);
            gvwRoom.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwRoom_Deleting);
            gvwRoom.CellValidating += new EventHandler<GridViewCellValidatingEventArgs>(gvwRoom_CellValidating);
            gvwRoom.BeginningEdit += new EventHandler<GridViewBeginningEditRoutedEventArgs>(gvwRoom_BeginningEdit);
            gvwRoom.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(gvwRoom_SelectionChanged);

            btnSearch.Click += new RoutedEventHandler(btnSearch_Click);
            btnSaveRoom.Click += new RoutedEventHandler(btnSaveRoom_Click);
            btnCancelRoom.Click += new RoutedEventHandler(btnCancelRoom_Click);

            Dictionary<int, string> roomStatusDic = new Dictionary<int, string>();
            roomStatusDic.Add((int)RoomStatus.Available, RoomStatus.Available.ToString());
            roomStatusDic.Add((int)RoomStatus.Occupied, RoomStatus.Occupied.ToString());
            ((GridViewComboBoxColumn)this.gvwRoom.Columns["RoomStatus"]).ItemsSource = roomStatusDic;

            gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Collapsed;
            ucImageUpload.ImageType = ImageType.Room;

            DataServiceHelper.ListEquipmentAsync(Globals.UserLogin.UserOrganisationId, null, false, ListEquipmentCompleted);
            btnSaveRoomEquipment.Click += new RoutedEventHandler(btnSaveRoomEquipment_Click);
            btnCancelRoomEquipment.Click += new RoutedEventHandler(btnCancelRoomEquipment_Click);
            btnInsertRoomEquipment.Click += new RoutedEventHandler(btnInsertRoomEquipment_Click);
            gvwRoomEquipment.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwRoomEquipment_Deleting);

            DataServiceHelper.ListServiceAsync(Globals.UserLogin.UserOrganisationId, null, false, ListServiceCompleted);
            btnSaveRoomService.Click += new RoutedEventHandler(btnSaveRoomService_Click);
            btnCancelRoomService.Click += new RoutedEventHandler(btnCancelRoomService_Click);
            btnInsertRoomService.Click += new RoutedEventHandler(btnInsertRoomService_Click);
            gvwRoomService.Deleting += new EventHandler<GridViewDeletingEventArgs>(gvwRoomService_Deleting);

            //Common
            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerRooms);
        }

        void ucSitePicker_InitComplete(object sender, EventArgs e)
        {
            List<RoomType> roomTypes = ((GridViewComboBoxColumn)this.gvwRoom.Columns["RoomType"]).ItemsSource as List<RoomType>;
            if (roomTypes != null)
            {
                RebindRoomData();
            }
        }

        #region Room

        void ucRoomTypes_InitComplete(object sender, EventArgs e)
        {
            if (ucSitePicker.SiteId > 0)
            {
                RebindRoomData();
            }
            ((GridViewComboBoxColumn)this.gvwRoom.Columns["RoomType"]).ItemsSource = ucRoomTypes.RoomTypeList;
        }

        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            RebindRoomData();
        }

        private void RebindRoomData()
        {
            int siteId = ucSitePicker.SiteId;
            string name = txtName.Text;
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
            }
            else
            {
                name = null;
            }

            int floor = -1000;
            if (!string.IsNullOrEmpty(txtFloor.Text))
            {
                if (!int.TryParse(txtFloor.Text, out floor))
                {
                    floor = 0;
                }
            }
            int? realFloor = null;
            if (floor != -1000)
            {
                realFloor = floor;
            }
            string statusIds = string.Empty;
            if (chkAvailable.IsChecked == true)
            {
                statusIds += ((int)RoomStatus.Available).ToString() + ",";
            }
            if (chkOccupied.IsChecked == true)
            {
                statusIds += ((int)RoomStatus.Occupied).ToString() + ",";
            }
            if (!string.IsNullOrEmpty(statusIds))
            {
                statusIds = statusIds.Substring(0, statusIds.Length - 1);
            }
            else
            {
                statusIds = null;
            }

            string roomTypeIDs = ucRoomTypes.SelectedRoomTypeIds;
            if (string.IsNullOrEmpty(roomTypeIDs))
            {
                roomTypeIDs = null;
            }

            bool showLegacy = chkShowLegacy.IsChecked == true;

            Globals.IsBusy = true;
            DataServiceHelper.ListRoomAsync(Globals.UserLogin.UserOrganisationId, siteId, null, name, statusIds, roomTypeIDs, realFloor, showLegacy,
                ListRoomCompleted);
        }

        void ListRoomCompleted(List<Room> itemSource)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Room item in itemSource)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(item_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwRoom.ItemsSource = itemSource;

            if (_selectedRoomId > 0 && itemSource.Count(i => i.RoomId == _selectedRoomId) > 0)
            {
                gvwRoom.SelectedItem = itemSource.First(i => i.RoomId == _selectedRoomId);
            }
            else if (itemSource.Count > 0)
            {
                gvwRoom.SelectedItem = itemSource[0];
            }
            if (itemSource == null || itemSource.Count == 0)
            {             
                gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


        void gvwRoom_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            Room selectedRoom = gvwRoom.SelectedItem as Room;
            if (selectedRoom != null && selectedRoom.RoomId > 0)
            {
                gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Visible;
                _selectedRoomId = selectedRoom.RoomId;
                ucImageUpload.ItemId = _selectedRoomId;
                ucImageUpload.BeginRebind();

                //Bind Equipment and Service :
                RebindRoomEquipments();
                RebindRoomServices();
            }
            else
            {
                _selectedRoomId = -1;
                gridEquipmentService.Visibility = gridImages.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


        void gvwRoom_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            Room rowData = e.Row.Item as Room;
            //if (rowData.NullableRecordId != null && e.Cell.Column.UniqueName == "DaymarkerType")
            //{
            //    e.Cancel = true;
            //}
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                Room item = (Room)sender;
                item.IsChanged = true;
            }
        }

        void gvwRoom_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "RoomType" 
                || e.Cell.Column.UniqueName == "RoomStatus" 
                || e.Cell.Column.UniqueName == "RoomName")
            {
                if (e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()))
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                int recordId = int.Parse((sender as Button).Tag.ToString());
                List<Room> itemSource = gvwRoom.ItemsSource as List<Room>;
                itemSource = itemSource.Where(i => i.RecordId != recordId).ToList();
                gvwRoom.ItemsSource = null;
                gvwRoom.ItemsSource = itemSource;
            }
            
        }
        
        void gvwRoom_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        void gvwRoom_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Room newItem = new Room();
            newItem.RecordId = _newId--;
            newItem.SiteId = ucSitePicker.SiteId;
            newItem.RoomStatusId = (int)RoomStatus.Available;
            newItem.CreatedBy = newItem.UpdatedBy = Globals.UserLogin.UserName;
            newItem.IsChanged = true;
            e.NewObject = newItem;
        }

        void btnSaveRoom_Click(object sender, RoutedEventArgs e)
        {
            if (gvwRoom.SelectedItem != null)
                _selectedRoomId = ((Room)gvwRoom.SelectedItem).RoomId;
            List<Room> oldList = (List<Room>)gvwRoom.ItemsSource;
            List<Room> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveRoomAsync(saveList, SaveRoomCompleted);
        }

        void SaveRoomCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindRoomData();
        }

        void btnCancelRoom_Click(object sender, RoutedEventArgs e)
        {
            RebindRoomData();
        }
        #endregion

        #region RoomEquipment
        void gvwRoomEquipment_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }


        private void RebindRoomEquipments()
        {
            Room selectedRoom = gvwRoom.SelectedItem as Room;
            if (selectedRoom != null && selectedRoom.RoomId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListRoomEquipmentAsync(null, selectedRoom.RoomId, ListRoomEquipmentCompleted);
            }
        }

        private void ListEquipmentCompleted(List<Equipment> equipments)
        {
            uiEquipmentList.ItemsSource = equipments;
            if (equipments.Count > 0)
            {
                uiEquipmentList.SelectedIndex = 0;
            }
        }

        private void ListRoomEquipmentCompleted(List<RoomEquipment> roomEquipments)
        {
            Globals.IsBusy = false;
            _currentRoomEquipments.Clear();
            foreach (RoomEquipment roomEquipment in roomEquipments)
            {
                roomEquipment.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(roomEquipment_PropertyChanged);
                _currentRoomEquipments.Add(roomEquipment);
            }
            gvwRoomEquipment.ItemsSource = roomEquipments;
        }

        void roomEquipment_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                RoomEquipment item = (RoomEquipment)sender;
                item.IsChanged = true;
            }
        }

        void btnCancelRoomEquipment_Click(object sender, RoutedEventArgs e)
        {
            RebindRoomEquipments();
        }

        void btnSaveRoomEquipment_Click(object sender, RoutedEventArgs e)
        {
            List<RoomEquipment> saveList = (List<RoomEquipment>)gvwRoomEquipment.ItemsSource;
            //Get delete items :
            foreach (RoomEquipment oldItem in _currentRoomEquipments)
            {
                bool isDeleted = true;
                foreach (RoomEquipment saveItem in saveList)
                {
                    if (saveItem.RoomEquipmentId == oldItem.RoomEquipmentId)
                    {
                        isDeleted = false;
                        break;
                    }
                }
                if (isDeleted)
                {
                    oldItem.IsDeleted = true;
                    saveList.Add(oldItem);
                }
            }
            Globals.IsBusy = true;
            DataServiceHelper.SaveRoomEquipmentAsync(saveList, SaveRoomEquipmentCompleted);
        }

        void SaveRoomEquipmentCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindRoomEquipments();
        }

        void btnInsertRoomEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = uiEquipmentList.SelectedItem as Equipment;
            if (equipment != null && _selectedRoomId > 0)
            {
                List<RoomEquipment> list = (List<RoomEquipment>)gvwRoomEquipment.ItemsSource;
                if (list.Count(i => i.EquipmentId == equipment.EquipmentId) > 0)
                {
                    MessageBox.Show(Globals.UserMessages.ItemExist);
                }
                else
                {
                    RoomEquipment newRoomEquipment = new RoomEquipment();
                    newRoomEquipment.RoomId = _selectedRoomId;
                    newRoomEquipment.EquipmentId = equipment.EquipmentId;
                    newRoomEquipment.Equipment = equipment.EquipmentName;
                    newRoomEquipment.Unit = equipment.Unit;
                    newRoomEquipment.Price = equipment.RentPrice;
                    newRoomEquipment.Description = equipment.Description;
                    newRoomEquipment.IsChanged = true;
                    list.Add(newRoomEquipment);
                    gvwRoomEquipment.ItemsSource = null;
                    gvwRoomEquipment.ItemsSource = list;
                }
            }
        }
        #endregion

        #region RoomService
        void gvwRoomService_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Globals.UserMessages.ConfirmDeleteNoParam, Globals.UserMessages.ConfirmationRequired, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void RebindRoomServices()
        {
            Room selectedRoom = gvwRoom.SelectedItem as Room;
            if (selectedRoom != null && selectedRoom.RoomId > 0)
            {
                Globals.IsBusy = true;
                DataServiceHelper.ListRoomServiceAsync(null, selectedRoom.RoomId, ListRoomServiceCompleted);
            }
        }

        private void ListServiceCompleted(List<Service> services)
        {
            uiServiceList.ItemsSource = services;
            if (services.Count > 0)
            {
                uiServiceList.SelectedIndex = 0;
            }
        }

        private void ListRoomServiceCompleted(List<RoomService> roomServices)
        {
            Globals.IsBusy = false;
            _currentRoomServices.Clear();
            foreach (RoomService roomService in roomServices)
            {
                roomService.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(roomService_PropertyChanged);
                _currentRoomServices.Add(roomService);
            }
            gvwRoomService.ItemsSource = roomServices;
        }

        void roomService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged")
            {
                RoomService item = (RoomService)sender;
                item.IsChanged = true;
            }
        }

        void btnCancelRoomService_Click(object sender, RoutedEventArgs e)
        {
            RebindRoomServices();
        }

        void btnSaveRoomService_Click(object sender, RoutedEventArgs e)
        {
            List<RoomService> saveList = (List<RoomService>)gvwRoomService.ItemsSource;
            //Get delete items :
            foreach (RoomService oldItem in _currentRoomServices)
            {
                bool isDeleted = true;
                foreach (RoomService saveItem in saveList)
                {
                    if (saveItem.RoomServiceId == oldItem.RoomServiceId)
                    {
                        isDeleted = false;
                        break;
                    }
                }
                if (isDeleted)
                {
                    oldItem.IsDeleted = true;
                    saveList.Add(oldItem);
                }
            }
            Globals.IsBusy = true;
            DataServiceHelper.SaveRoomServiceAsync(saveList, SaveRoomServiceCompleted);
        }

        void SaveRoomServiceCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
            RebindRoomServices();
        }


        void btnInsertRoomService_Click(object sender, RoutedEventArgs e)
        {
            Service service = uiServiceList.SelectedItem as Service;
            if (service != null && _selectedRoomId > 0)
            {
                List<RoomService> list = (List<RoomService>)gvwRoomService.ItemsSource;
                if (list.Count(i => i.ServiceId == service.ServiceId) > 0)
                {
                    MessageBox.Show(Globals.UserMessages.ItemExist);
                }
                else
                {

                    RoomService newRoomService = new RoomService();
                    newRoomService.RoomId = _selectedRoomId;
                    newRoomService.ServiceId = service.ServiceId;
                    newRoomService.Service = service.Name;
                    newRoomService.IsChanged = true;
                    newRoomService.Unit = service.Unit;
                    newRoomService.Price = service.Price;
                    newRoomService.Description = service.Description;
                    list.Add(newRoomService);
                    gvwRoomService.ItemsSource = null;
                    gvwRoomService.ItemsSource = list;
                }
            }
        }
        #endregion
    }
}
