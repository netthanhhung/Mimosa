using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class SitePicker : DataPicker
    {
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal event EventHandler InitComplete;

        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        public List<UserRoleAuth> UserRoleAuths { get; set; }
        public bool AutoSelect { get; set; }
        public static int EmptySiteValue = 0;
        public static int AllSiteValue = -1;

        public int SiteId
        {
            get { return Utilities.ToInt(SelectedValue); }
            set { SelectedValue = value; }
        }

        public string SiteDisplayName
        {
            get
            {
                return SelectedSite == null ? string.Empty : SelectedSite.Name; ;
            }
            set { }
        }

        private List<Site> SiteSource
        {
            get { return ItemsSource as List<Site>; }
            set { ItemsSource = value; }
        }

        public bool ShowAllSitesItem { get; set; }

        public bool ShowEmpty { get; set; }

        public bool ListParameterSetValues { get; set; }


        public Site SelectedSite
        {
            get { return SelectedItem as Site; }
        }

        public string GetSiteIdList()
        {
            string result = string.Empty;

            if (ShowAllSitesItem && SiteId == AllSiteValue)
            {
                result = string.Join(",", (from site in SiteSource
                           where site.SiteId != AllSiteValue
                           select site.SiteId.ToString()));                           
            }
            else
            {
                result = SiteId.ToString();
            }

            return result;
        }

        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public SitePicker()
        {
            DisplayMemberPath = "Name";
            SelectedValuePath = "SiteId";
        }

        internal void SelectSitesComplete(List<Site> itemsSource)
        {
            List<Site> newItemsource = new List<Site>();
            foreach (Site site in itemsSource)
            {
                //if (Globals.UserLogin.UserLicenseKeys.Contains(site.LicenseKey))
                //{
                //    License li = Globals.Licenses.FirstOrDefault(i => i.LicenseKey.Equals(site.LicenseKey));
                //    if (!li.RealEndDate.HasValue || li.RealEndDate.Value >= Globals.Today)
                //    {
                        newItemsource.Add(site);
                //    }
                //}
            }
            
            if (ShowEmpty)
            {
                newItemsource.Insert(0, new Site
                {
                    RecordId = EmptySiteValue,
                    SiteId = EmptySiteValue,
                    Name = Globals.ListConstants.SelectEmpty,
                });
                SelectedValue = EmptySiteValue;
            }

            if (ShowAllSitesItem)
            {
                newItemsource.Insert(0, new Site
                {
                    RecordId = AllSiteValue,
                    SiteId = AllSiteValue,
                    Name = Globals.ListConstants.SelectAll,
                });
            }
            ItemsSource = newItemsource;
            this.SelectedIndex = 0;
            ApplySecurity();
            
            RaiseInitCompleteEvent();
        }

        public void ApplySecurity()
        {
            bool isEnabled = false;
            List<Site> itemsSource = ItemsSource as List<Site>;
            if (this.UserRoleAuths != null && this.UserRoleAuths.Count > 0)
            {
                isEnabled = this.UserRoleAuths.Count(i => !i.SiteId.HasValue) > 0;
                if (!isEnabled)
                {
                    var abc = (from item in this.UserRoleAuths
                               select item.SiteId).Distinct();
                    if (abc.Count() > 1)
                    {
                        List<Site> newItemSource = new List<Site>();
                        foreach (Site site in itemsSource)
                        {
                            if (site.SiteId <= 0 || abc.Contains(site.SiteId))
                            {
                                newItemSource.Add(site);
                            }
                        }
                        isEnabled = true;
                        ItemsSource = newItemSource;
                        SelectedIndex = 0;
                    }
                    else if (abc.Count() == 1)
                    {
                        ItemsSource = itemsSource.Where(i => i.SiteId == abc.First().Value);
                        SiteId = abc.First().Value;
                    }
                    else
                    {
                        SelectedIndex = 0;
                    }
                }
                else if (AutoSelect)
                {
                    SelectedIndex = 0;
                }
            }
            else
            {
                SelectedIndex = 0;
            }
            this.IsEnabled = isEnabled;
        }

        /*  ======================================================================            
         *      PAGE FUNCTIONS
         *  ====================================================================== */
        internal void Init(int orgId)
        {
            DataServiceHelper.ListSiteAsync(orgId, null, this.ShowLegacy, false, SelectSitesComplete);            
        }

        internal void Init()
        {
            if (ItemsSource == null)
            {
                if (this.UserRoleAuths == null || this.UserRoleAuths.Count(i => !i.SiteGroupId.HasValue) > 0)
                {
                    DataServiceHelper.ListSiteAsync(Globals.UserLogin.UserOrganisationId, null, this.ShowLegacy, false, SelectSitesComplete);
                }
                else
                {
                    int siteGroupId = this.UserRoleAuths.First(i => i.SiteGroupId.HasValue).SiteGroupId.Value;
                    DataServiceHelper.ListSiteAsync(Globals.UserLogin.UserOrganisationId, null, this.ShowLegacy, false, SelectSitesComplete);
                }
            }
            else
            {
                RaiseInitCompleteEvent();
            }
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
