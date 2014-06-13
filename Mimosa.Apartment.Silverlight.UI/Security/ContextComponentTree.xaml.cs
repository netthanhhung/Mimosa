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
using System.Windows.Browser;
using Telerik.Windows.Input;
using Telerik.Windows.Controls;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class ContextComponentTree : UserControl
    {
        private static class ModuleNames
        {
            internal const string Home = "Home";
            internal const string HomeSecurity = "Security";
            internal const string HomeAdministration = "Administration";

            internal const string Sales = "Sales";

            internal const string Customer = "Customer";
        }

        public List<Component> RealComponents { get; set; }
        public List<RoleComponentPermission> RoleComponentItemSource { get; set; }
        private List<RoleComponentVM> _flatTreeViewItemSource = new List<RoleComponentVM>();
        public Guid RoleId { get; set; }
        public bool IsReadonly { get; set; }

        public ContextComponentTree()
        {
            InitializeComponent();
        }
        

        public void BuildTreeView()
        {
            chkExpandAll.IsChecked = false;
            _flatTreeViewItemSource.Clear();

            List<RoleComponentVM> treeItemSource = new List<RoleComponentVM>();
            treeItemSource.Add(BuildHomeModuleNode());
            treeItemSource.Add(BuildSaleModuleNode());
            treeItemSource.Add(BuildCustomerdModuleNode());

            uiRoleComponentTreeView.ItemsSource = treeItemSource;            
        }

        private RoleComponentVM BuildHomeModuleNode()
        {
            List<RoleComponentVM> homeChildList = new List<RoleComponentVM>();

            List<RoleComponentVM> securityNodeList = new List<RoleComponentVM>();
            securityNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.RoleAdmin, null));
            securityNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.RoleComponentPermission, null));
            securityNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.UserRoleAuthorisation, null));
            

            List<RoleComponentVM> administrationNodeList = new List<RoleComponentVM>();
            administrationNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.SiteAdmin, null));
            administrationNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.SiteGroupAdmin, null));
            administrationNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.EquipmentAdmin, null));
            administrationNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.ServiceAdmin, null));
            administrationNodeList.Add(BuildOneTreeViewItem(null, LayoutComponentType.RoomAdmin, null));
            homeChildList.Add(BuildOneTreeViewItem(ModuleNames.HomeAdministration, LayoutComponentType.None, administrationNodeList));

            RoleComponentVM result = BuildOneTreeViewItem(ModuleNames.Home, LayoutComponentType.None, homeChildList);

            return result;
        }

        private RoleComponentVM BuildSaleModuleNode()
        {
            List<RoleComponentVM> saleChildList = new List<RoleComponentVM>();
            saleChildList.Add(BuildOneTreeViewItem(null, LayoutComponentType.BookingAdmin, null));

            RoleComponentVM result = BuildOneTreeViewItem(ModuleNames.Sales, LayoutComponentType.None, saleChildList);

            return result;
        }

        private RoleComponentVM BuildCustomerdModuleNode()
        {
            List<RoleComponentVM> customerChildList = new List<RoleComponentVM>();

            customerChildList.Add(BuildOneTreeViewItem(null, LayoutComponentType.CustomerAdmin, null));
            customerChildList.Add(BuildOneTreeViewItem(null, LayoutComponentType.MonthlyPayment, null));
            RoleComponentVM result = BuildOneTreeViewItem(ModuleNames.Customer, LayoutComponentType.None, customerChildList);

            return result;
        }

        private RoleComponentVM BuildOneTreeViewItem(string nodeName, LayoutComponentType type, List<RoleComponentVM> childList)
        {
            RoleComponentVM item1 = new RoleComponentVM();
            item1.RoleComponentChildVMList = childList;
            if (childList == null)
            {
                Component realCompt = GetRealComponent(type);
                RoleComponentPermission roleComptPermission = GetRoleComponentPermission(type);
                item1.Name = realCompt.Name;
                item1.RoleId = this.RoleId;
                item1.ComponentId = (int)type;
                if (roleComptPermission != null)
                {
                    item1.RoleComponentPermissionId = roleComptPermission.RoleComponentPermissionId;
                    item1.HasAccess = true;
                    item1.HasWriteRight = roleComptPermission.WriteRight == true;
                }               
            }
            else
            {
                item1.Name = nodeName;
            }

            item1.IsEnabled = !this.IsReadonly;

            _flatTreeViewItemSource.Add(item1);

            return item1;
        }

        private Component GetRealComponent(LayoutComponentType type)
        {
            Component result = null;
            if (RealComponents != null)
            {
                result = RealComponents.FirstOrDefault(i => i.ComponentId == (int)type);
            }
            return result;
        }

        private RoleComponentPermission GetRoleComponentPermission(LayoutComponentType type)
        {
            RoleComponentPermission result = null;

            if (RoleComponentItemSource != null)
            {
                var list = RoleComponentItemSource.Where(i => i.ComponentId == (int)type);
                foreach (RoleComponentPermission item in list)
                {
                    if (item.WriteRight == true)
                    {
                        result = item;
                        break;
                    }
                }
                if (result == null && list.Count() > 0)
                {
                    result = list.FirstOrDefault();
                }
            }
            return result;
        }    

        private void WriteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkItem = sender as CheckBox;
            if (checkItem != null && checkItem.Tag != null && !string.IsNullOrEmpty(checkItem.Tag.ToString()))
            {
                string[] roleIdCompIdString = checkItem.Tag.ToString().Split(',');
                if (roleIdCompIdString.Length == 2)
                {
                    Guid roleId = new Guid(roleIdCompIdString[0]);
                    int compId = Convert.ToInt32(roleIdCompIdString[1]);
                    RoleComponentVM treeItem = _flatTreeViewItemSource.FirstOrDefault(i => i.RoleId == roleId && i.ComponentId == compId);
                    if (treeItem != null)
                    {
                        if (checkItem.IsChecked == true)
                        {
                            treeItem.HasAccess = true;
                        }
                    }

                }
            }
        }

        private void ChildTreeViewItem_CheckChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkItem = sender as CheckBox;
            if (checkItem != null && checkItem.Tag != null && !string.IsNullOrEmpty(checkItem.Tag.ToString()))
            {
                string[] roleIdCompIdString = checkItem.Tag.ToString().Split(',');
                if (roleIdCompIdString.Length == 2)
                {
                    Guid roleId = new Guid(roleIdCompIdString[0]);
                    int compId = Convert.ToInt32(roleIdCompIdString[1]);
                    RoleComponentVM treeItem = _flatTreeViewItemSource.FirstOrDefault(i => i.RoleId == roleId && i.ComponentId == compId);
                    if (treeItem != null)
                    {
                        if (checkItem.IsChecked != true)
                        {
                            treeItem.HasWriteRight = false;
                        }
                    }
                }
            }
        }

        //public void ExpandAllTreeNodes(bool isExpanded)
        //{
        //    foreach (RoleComponentVM item in _flatTreeViewItemSource)
        //    {
        //        item.IsExpanded = isExpanded;
        //    }
        //}

        private void chkExpandAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (RoleComponentVM item in _flatTreeViewItemSource)
            {
                item.IsExpanded = chkExpandAll.IsChecked == true;
            }
        }

        public List<RoleComponentPermission> GetSaveList()
        {
            List<RoleComponentPermission> saveList = new List<RoleComponentPermission>();
            if (_flatTreeViewItemSource != null && _flatTreeViewItemSource.Count > 0)
            {
                foreach (RoleComponentVM treeItem in _flatTreeViewItemSource)
                {
                    if (treeItem.RoleComponentPermissionId > 0)
                    {
                        RoleComponentPermission originalItem = this.RoleComponentItemSource.FirstOrDefault(i => i.RoleComponentPermissionId == treeItem.RoleComponentPermissionId);
                        if (originalItem != null)
                        {
                            if (treeItem.HasAccess)
                            {
                                if (treeItem.HasWriteRight == false && originalItem.WriteRight == true
                                    || treeItem.HasWriteRight == true && (!originalItem.WriteRight.HasValue || originalItem.WriteRight == false))
                                {
                                    originalItem.WriteRight = treeItem.HasWriteRight;
                                    originalItem.IsChanged = true;
                                    originalItem.UpdatedBy = Globals.UserLogin.UserName;
                                    saveList.Add(originalItem);
                                }
                            }
                            else
                            {
                                originalItem.IsDeleted = true;
                                saveList.Add(originalItem);
                            }
                        }
                    }
                    else
                    {
                        if (treeItem.HasAccess && treeItem.RoleId != Guid.Empty && treeItem.ComponentId > 0)
                        {
                            RoleComponentPermission newItem = new RoleComponentPermission();
                            newItem.ComponentId = treeItem.ComponentId;
                            newItem.RoleId = treeItem.RoleId;
                            newItem.WriteRight = treeItem.HasWriteRight;
                            newItem.IsChanged = true;
                            newItem.CreatedBy = Globals.UserLogin.UserName;
                            saveList.Add(newItem);
                        }
                    }
                }
            }
            return saveList;
        }
    }
}
