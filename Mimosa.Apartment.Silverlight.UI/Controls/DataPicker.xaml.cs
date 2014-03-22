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
using System.ComponentModel;
using System.Collections;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class DataPicker : UserControl
    {
        /*  ======================================================================            
         *      CONTROL MEMBERS
         *  ====================================================================== */

        public bool ShowInactiveCheck
        {
            get { return uiInactive.Visibility == Visibility.Visible; }
            set { uiInactive.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }

        public string InactiveMessage 
        {
            get { return uiInactive.Content.ToString(); }
            set { uiInactive.Content = value; }
        }

        public double DataListWidth
        {
            get { return uiDataList.Width; }
            set { uiDataList.Width = value; }
        }

        public bool ShowLegacy
        {
            get { return uiInactive.IsChecked.HasValue ? uiInactive.IsChecked.Value : false; }
            set { uiInactive.IsChecked = value; }
        }

        public IEnumerable ItemsSource
        {
            get { return uiDataList.ItemsSource; }
            set 
            {
                RemoveSelectionChangedEvent();
                uiDataList.ItemsSource = value;
                AddSelectionChangedEvent();
            }
        }

        public string DisplayMemberPath
        {
            get { return uiDataList.DisplayMemberPath; }
            set { uiDataList.DisplayMemberPath = value; }
        }

        public string SelectedValuePath
        {
            get { return uiDataList.SelectedValuePath; }
            set { uiDataList.SelectedValuePath = value; }
        }

        public int SelectedIndex
        {
            get { return uiDataList.SelectedIndex; }
            set
            {
                if (Count > 0)
                {
                    uiDataList.SelectedIndex = value;
                }
            }
        }

        public int Count
        {
            get 
            {
                if (uiDataList.Items != null)
                {
                    return uiDataList.Items.Count;
                }
                return 0;
            }
        }

        public ItemCollection Items
        {
            get { return uiDataList.Items; }
        }

        public object SelectedValue
        {
            get { return uiDataList.SelectedValue; }
            set { uiDataList.SelectedValue = value; }
        }

        public object SelectedItem
        {
            get { return uiDataList.SelectedItem; }
            set { uiDataList.SelectedItem = value; }
        }

        //public string SelectedDisplayName
        //{
        //    get
        //    {
        //        return ExtensionsTelerikSilverlight.SelectedText(uiDataList);
        //    }
        //    set { }
        //}

        //public KeyValuePair<int, string> SelectedPair
        //{
        //    get
        //    {
        //        return new KeyValuePair<int, string>(Utilities.ToInt(uiDataList.SelectedValue), uiDataList.SelectedText());
        //    }
        //}

        internal event SelectionChangedEventHandler SelectionChanged;
        protected event EventHandler Checked;
        protected event EventHandler Unchecked;

        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */

        public DataPicker()
        {
            InitializeComponent();
            AddSelectionChangedEvent();
            uiInactive.Checked += new RoutedEventHandler(uiInactive_Checked);
            uiInactive.Unchecked += new RoutedEventHandler(uiInactive_Unchecked);
        }

        void uiInactive_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Unchecked != null)
            {
                Unchecked(this, EventArgs.Empty);
            }
        }

        void uiInactive_Checked(object sender, RoutedEventArgs e)
        {
            if (Checked != null)
            {
                Checked(this, EventArgs.Empty);
            }
        }

        void uiDataList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, e);
            }
        }

        public void AddSelectionChangedEvent()
        {
            uiDataList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(uiDataList_SelectionChanged);
        }

        public void RemoveSelectionChangedEvent()
        {
            uiDataList.SelectionChanged -= new System.Windows.Controls.SelectionChangedEventHandler(uiDataList_SelectionChanged);            
        }
    }
}
