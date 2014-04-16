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
        public ContactInformationPanel()
        {
            InitializeComponent();

            btnSaveContact.Click += new RoutedEventHandler(btnSaveContact_Click);
            DataServiceHelper.ListCountryAsync(null, ListCountryCompleted);
        }

        void ListCountryCompleted(List<Country> countryList)
        {
            uiCountry.ItemsSource = countryList;
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
