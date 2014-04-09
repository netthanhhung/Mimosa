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
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class EquipmentAdminPage : Page
    {
        private int _selectedEquipmentId;
        private List<Equipment> _originalItemSource = new List<Equipment>();
        
        //public List<UserRoleAuth> UserRoleAuths { get; set; }

        private static class UserMessages
        {
            internal const string NameErrorMessage2 = "The string may not exceed 128 characters in length.";
        }

        public EquipmentAdminPage()
        {
            InitializeComponent();
            //UserRoleAuths = SecurityHelper.GetUserRoleAuths((int)LayoutComponentType.RoleAdmin);

            if (!Globals.UserLogin.IsUserOrganisationAdministrator)
            {
                this.Content = SecurityHelper.GetNoPermissionInfoPanel();
                return;
            }

            BeginRebindEquipment();
            
            //Equipments
            btnSaveEquipment.Click += new RoutedEventHandler(btnSaveEquipment_Click);
            btnCancelEquipment.Click += new RoutedEventHandler(btnCancelEquipment_Click);
            gvwEquipments.AddingNewDataItem += new EventHandler<Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs>(gvwEquipments_AddingNewDataItem);
            //gvwEquipments.BeginningEdit += new EventHandler<Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs>(gvwEquipments_BeginningEdit);
            gvwEquipments.CellValidating += new EventHandler<Telerik.Windows.Controls.GridViewCellValidatingEventArgs>(gvwEquipments_CellValidating);
            gvwEquipments.SelectionChanged += new EventHandler<SelectionChangeEventArgs>(gvwEquipments_SelectionChanged);

            gridImages.Visibility = System.Windows.Visibility.Collapsed;
            ucImageUpload.ImageType = ImageType.Equipment;
            
            //Common
            UiHelper.ApplyMouseWheelScrollViewer(scrollViewerEquipment);
        }

        private void BeginRebindEquipment()
        {
            Globals.IsBusy = true;
            DataServiceHelper.ListEquipmentAsync(Globals.UserLogin.UserOrganisationId, null, true, ListEquipmentCompleted);
        }

        void ListEquipmentCompleted(List<Equipment> orgList)
        {
            Globals.IsBusy = false;
            _originalItemSource.Clear();
            foreach (Equipment item in orgList)
            {
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(EquipmentItem_PropertyChanged);
                _originalItemSource.Add(item);
            }
            gvwEquipments.ItemsSource = orgList;
            if (_selectedEquipmentId > 0 && orgList.Count(i => i.EquipmentId == _selectedEquipmentId) > 0)
            {
                gvwEquipments.SelectedItem = orgList.First(i => i.EquipmentId == _selectedEquipmentId);
            }
            else if (orgList.Count > 0)
            {
                gvwEquipments.SelectedItem = orgList[0];
            }
        }

        #region Equipment GridView
        private void gvwEquipments_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.UniqueName == "EquipmentName")
            {
                if (e.NewValue.ToString().Length < 1)
                {
                    e.IsValid = false;
                    e.ErrorMessage = Globals.UserMessages.RequiredFieldGeneric;
                }
                else if (e.NewValue.ToString().Length > 128)
                {
                    e.IsValid = false;
                    e.ErrorMessage = UserMessages.NameErrorMessage2;
                }
            }
        }


        void gvwEquipments_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            Equipment newItem = new Equipment();
            newItem.RecordId = null;
            newItem.CreatedBy = Globals.UserLogin.UserName;
            newItem.OrganisationId = Globals.UserLogin.UserOrganisationId;
            newItem.IsChanged = true;
            e.NewObject = newItem;

        }

        void btnCancelEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (gvwEquipments.SelectedItem != null)
                _selectedEquipmentId = ((Equipment)gvwEquipments.SelectedItem).EquipmentId;
            BeginRebindEquipment();
        }

        void btnSaveEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (gvwEquipments.SelectedItem != null)
                _selectedEquipmentId = ((Equipment)gvwEquipments.SelectedItem).EquipmentId;
            List<Equipment> oldList = (List<Equipment>)gvwEquipments.ItemsSource;
			List<Equipment> saveList = oldList.Where(d => (d.IsChanged || d.NullableRecordId == null)).ToList();
            Globals.IsBusy = true;
            DataServiceHelper.SaveEquipmentAsync(saveList, SaveEquipmentCompleted);

        }

        private void SaveEquipmentCompleted()
        {
            Globals.IsBusy = false;
            //Reload data
            BeginRebindEquipment();
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }


        void EquipmentItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Equipment item = sender as Equipment;
            if (item != null && e.PropertyName != "IsChanged")
            {
                item.IsChanged = true;
            }
        }


        void gvwEquipments_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            Equipment selectedEquipment = gvwEquipments.SelectedItem as Equipment;
            if (selectedEquipment != null && selectedEquipment.EquipmentId > 0)
            {
                gridImages.Visibility = System.Windows.Visibility.Visible;
                _selectedEquipmentId = selectedEquipment.EquipmentId;
                ucImageUpload.ItemId = _selectedEquipmentId;
                ucImageUpload.BeginRebind();

            }
            else
            {
                _selectedEquipmentId = -1;
                gridImages.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #endregion


    }
}
