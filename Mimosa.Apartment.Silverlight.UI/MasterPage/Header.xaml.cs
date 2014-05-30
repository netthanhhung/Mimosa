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
using Telerik.Windows.Controls;
using System.Xml.Linq;
using nsTooltips = Silverlight.Controls.ToolTips;
using System.Windows.Media.Imaging;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class Header : UserControl
    {
        private List<SiteMapMenuItem> _siteMap = null;
        public ModuleTypes ModuleType { get; set; }

        public Header()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Header_Loaded);
        }

        void Header_Loaded(object sender, RoutedEventArgs e)
        {
            uiDeploymentInfo.Text = DeploymentInfo.ToString();
            //uiBtnLogo.DataContext = this;
            SiteMapHelper.SelectWeb1SiteMapAsync(LoadMenuItems);
        }


        private void LoadMenuItems(XDocument xdocument)
        {
            _siteMap = (
                from menuitem in xdocument.Root.Elements(SiteMapHelper.XNameSiteMapNode)
                select ChildMenuItem(menuitem)
                ).ToList();


            BuildStandardMenu();

        }

        private SiteMapMenuItem ChildMenuItem(XElement siteMapNode)
        {
            bool isEnabled = (string.IsNullOrEmpty((string)siteMapNode.Attribute("disabled")));

            SiteMapMenuItem result =
                new SiteMapMenuItem()
                {
                    Title = (string)siteMapNode.Attribute("title"),
                    Description = (string)siteMapNode.Attribute("description"),
                    Url = (string)siteMapNode.Attribute("url"),
                    IsEnabled = isEnabled,
                    Target = (string)siteMapNode.Attribute("target"),
                    Items =
                        from childNode in siteMapNode.Elements(SiteMapHelper.XNameSiteMapNode)
                        select ChildMenuItem(childNode)
                };

            return result;
        }        

        private void uiContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadMenuItem radMenuItem = e.OriginalSource as RadMenuItem;
            if (radMenuItem != null)
            {
                SiteMapMenuItem menuItem = radMenuItem.DataContext as SiteMapMenuItem;
                if (menuItem != null && !string.IsNullOrEmpty(menuItem.Url))
                {
                    nsTooltips.ToolTipService.ClearAllToolTip();
                    if (menuItem.Url.Contains(".xaml"))
                    {
                        UriHelper.NavigateTo("#" + menuItem.Url, menuItem.Target);
                    }
                    else
                    {
                        UriHelper.NavigateTo(menuItem.Url, menuItem.Target);
                    }
                }
            }
        }

        void uiChild_MenuItemClicked(object sender, EventArgs e)
        {
            HeaderMenuButton selectedButton = sender as HeaderMenuButton;
            SetHighLightButton(selectedButton);
        }

        private void SetHighLightButton(HeaderMenuButton selectedButton)
        {
            if (!selectedButton.IsHighLight)
            {
                for (int i = 1; i < uiMenuBar.Children.Count; i++)
                {
                    HeaderMenuButton menuButton = uiMenuBar.Children[i] as HeaderMenuButton;
                    if (menuButton != selectedButton)
                    {
                        menuButton.IsHighLight = false;
                    }
                }

                selectedButton.IsHighLight = true;
                Globals.ActiveModule = selectedButton.ModuleType;
            }
        }


        private void EmptyMenu()
        {
            if (uiMenuBar.Children != null)
            {
                int menuCount = uiMenuBar.Children.Count;
                for (int i = 0; i < menuCount - 1; i++)
                {
                    uiMenuBar.Children.RemoveAt(1);
                }
            }
        }

        private void BuildStandardMenu()
        {
            EmptyMenu();

            // set logo
            //uiBtnLogo.Content = moduleType.ToString().ToLower();

            List<SiteMapMenuItem> modules = new List<SiteMapMenuItem>();
            foreach (SiteMapMenuItem parentItem in _siteMap)
            {
                if (parentItem.Title == "Logo")
                {
                    //uiContextMenu.ItemsSource = parentItem.Items.ToList();
                    continue;
                }

                ModuleTypes moduleType = (ModuleTypes)Enum.Parse(typeof(ModuleTypes), parentItem.Description, true);
                bool isModuleEnable = true;
                //if (moduleType == ModuleTypes.Employees)
                //{
                //    isModuleEnable = Globals.UserLogin.UserModuleTypes.Contains(ModuleTypes.Employees)
                //        || Globals.UserLogin.UserModuleTypes.Contains(ModuleTypes.Sales)
                //        || Globals.UserLogin.UserModuleTypes.Contains(ModuleTypes.Operations);
                //}
                //else
                //{
                //    isModuleEnable = Globals.UserLogin.UserModuleTypes.Contains(moduleType);
                //}
                HeaderMenuButton menu = new HeaderMenuButton()
                {
                    InnerText = parentItem.Title,
                    ModuleType = moduleType,
                    IsEnabled = isModuleEnable
                };
                menu.Init(parentItem.Items.ToList(), string.Empty);
                //if (moduleType == Globals.ActiveModule)
                //{
                //    menu.IsHighLight = true;
                //}
                menu.MenuItemClicked += new EventHandler(uiChild_MenuItemClicked);

                uiMenuBar.Children.Add(menu);
            }
        }

        private void BuildModuleMenu(ModuleTypes? moduleType)
        {
            if (moduleType == null && Globals.ActiveModule != ModuleTypes.Administration)
            {
                moduleType = Globals.ActiveModule;
            }

            EmptyMenu();

            int index = 0;

            ModuleTypes currentModuleType = ModuleTypes.Administration;
            List<SiteMapMenuItem> modules = new List<SiteMapMenuItem>();
            foreach (SiteMapMenuItem parentItem in _siteMap)
            {
                currentModuleType = (ModuleTypes)Enum.Parse(typeof(ModuleTypes), parentItem.Description, true);

                // security filter
                //if (!Globals.UserLogin.UserModuleTypes.Contains(currentModuleType))
                //{
                //    continue;
                //}

                index++;
                if (moduleType == null && index == 2)
                {
                    moduleType = currentModuleType;
                }

                if (moduleType.HasValue && parentItem.Title == moduleType.ToString())
                {
                    // set logo
                    //string resourceName = String.Format("Logo{0}SourceUrl", moduleType.ToString());
                    //uiLogo.Source = new BitmapImage(new Uri(App.Current.Resources[resourceName].ToString(), UriKind.Relative));

                    foreach (SiteMapMenuItem item in parentItem.Items)
                    {
                        HeaderMenuButton menu = new HeaderMenuButton()
                        {
                            InnerText = item.Title,
                            ContentWidth = 120 // temporary fix the problem of width
                        };
                        menu.Init(item.Items.ToList(), item.Url);

                        uiMenuBar.Children.Add(menu);
                    }
                }
                else
                {
                    modules.Add(new SiteMapMenuItem()
                    {
                        Title = parentItem.Title,
                        Description = parentItem.Description,
                        Url = parentItem.Url,
                        IsEnabled = parentItem.IsEnabled,
                        Target = parentItem.Target
                    });
                }
            }

            //uiContextMenu.ItemsSource = modules;
        }
    }
}
