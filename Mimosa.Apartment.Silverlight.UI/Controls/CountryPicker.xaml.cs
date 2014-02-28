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
    public partial class CountryPicker : UserControl
    {
        public int SelectedValue
        {
            get { return Utilities.ToInt(uiDataList.SelectedValue); }
            set { uiDataList.SelectedValue = value; }
        }

        public Country SelectedItem
        {
            get { return uiDataList.SelectedItem as Country; }
            set { uiDataList.SelectedItem = value; }
        }

        public List<Country> ItemsSource
        {
            get { return uiDataList.ItemsSource as List<Country>; }
        }

        public CountryPicker()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            DataServiceHelper.ListCountryAsync(null, ListCountryCallback);
        }

        private void ListCountryCallback(List<Country> itemsSource)
        {
            uiDataList.ItemsSource = itemsSource;
        }
    }
}
