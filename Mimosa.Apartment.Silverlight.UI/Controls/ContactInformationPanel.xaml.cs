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
    public partial class ContactInformationPanel : UserControl
    {

        public int ContactTypeId { get; set; }

        public ContactInformationPanel()
        {
            InitializeComponent();
            FillLanguage();
            btnSaveContact.Click += new RoutedEventHandler(btnSaveContact_Click);
            DataServiceHelper.ListCountryAsync(null, ListCountryCompleted);
            uiCountry.SelectionChanged += new SelectionChangedEventHandler(uiCountry_SelectionChanged);
            cbbCity.SelectionChanged += new SelectionChangedEventHandler(cbbCity_SelectionChanged);

        }

        void FillLanguage()
        {
            txtContactInfo.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_txtContactInfo");
            lblFirstName.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblFirstName");
            lblLastName.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblLastName");
            lblAddress.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblAddress");
            lblAddress2.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblAddress2");
            lblDistrict.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblDistrict");
            lblCity.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblCity");
            lblState.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblState");
            lblCountry.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblCountry");
            lblPostCode.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblPostCode");
            lblPhone.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblPhone");
            lblFax.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblFax");
            lblEmail.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblEmail");
            lblDateOfBirth.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblDateOfBirth");
            lblVisa.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblVisa");
            lblVisaValid.Text = ResourceHelper.GetReourceValue("ContactInformationPanel_lblVisaValid");
            lblFrom.Text = ResourceHelper.GetReourceValue("Common_From");
            lblTo.Text = ResourceHelper.GetReourceValue("Common_To");
            btnSaveContact.Content = ResourceHelper.GetReourceValue("Common_btnSave");
        }

        void ListCountryCompleted(List<Country> countryList)
        {
            uiCountry.ItemsSource = countryList;
            uiCountry.SelectedValue = 232;

            if (this.ContactTypeId == (int)ContactType.Site)
            {
                cbbCity.Visibility = cbbDistrict.Visibility = Visibility.Visible;
                txtCity.Visibility = txtDistrict.Visibility = Visibility.Collapsed;
            }
            else
            {
                cbbCity.Visibility = cbbDistrict.Visibility = Visibility.Collapsed;
                txtCity.Visibility = txtDistrict.Visibility = Visibility.Visible;
            }

        }

        void uiCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int countryId = Convert.ToInt32(uiCountry.SelectedValue);
            if (countryId > 0 && this.ContactTypeId == (int)ContactType.Site)
            {
                DataServiceHelper.ListCityAsync(countryId, null, ListCityCompleted);
            }
        }

        void ListCityCompleted(List<City> cityList)
        {
            cbbCity.ItemsSource = cityList;
            ContactInformation saveContact = this.DataContext as ContactInformation;
            if (saveContact != null)
            {
                cbbCity.SelectedValue = saveContact.CityId;
            }            
        }

        void cbbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int cityId = Convert.ToInt32(cbbCity.SelectedValue);            
            if (cityId > 0 && this.ContactTypeId == (int)ContactType.Site)
            {
                DataServiceHelper.ListDistrictAsync(cityId, null, ListDistrictCompleted);
            }
        }

        void ListDistrictCompleted(List<District> districtList)
        {
            cbbDistrict.ItemsSource = districtList;
            ContactInformation saveContact = this.DataContext as ContactInformation;
            if (saveContact != null)
            {
                cbbDistrict.SelectedValue = saveContact.DistrictId;
            } 
        }

        void btnSaveContact_Click(object sender, RoutedEventArgs e)
        {
            ContactInformation saveContact = this.DataContext as ContactInformation;            
            if (saveContact != null)            
            {
                saveContact.IsChanged = true;
                List<ContactInformation> saveList = new List<ContactInformation>();
                saveList.Add(saveContact);
                if (saveContact.ContactInformationId > 0)
                {
                    saveContact.UpdatedBy = Globals.UserLogin.UserName;
                }
                else
                {
                    saveContact.CreatedBy = Globals.UserLogin.UserName;
                }

                Globals.IsBusy = true;
                DataServiceHelper.SaveContactInformationAsync(saveList, SaveContactInformationCompleted);
            }
        }

        void SaveContactInformationCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
