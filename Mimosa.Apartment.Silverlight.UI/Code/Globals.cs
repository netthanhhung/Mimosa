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
using System.Collections.Generic;
using Mimosa.Apartment.Silverlight.UI.ApartmentService;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    internal static class Globals
    {

        internal const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        internal static class UserMessages
        {
            internal const string NoPermission = "You don't have permission to view this area";
            internal const string DataLoading = "Contacting server for data...";
            internal const string DataNotFound = "No data was found for the given options";
            internal const string DataNotLoaded = "";

            internal static string RecordsSaved
            {
                get
                {
                    return "Changes saved: " + Globals.Now.ToShortTimeString();
                }
            }

            internal static string ChangesCancelled
            {
                get
                {
                    return "Changes cancelled: " + Globals.Now.ToShortTimeString();
                }
            }

            internal static string EmailSent
            {
                get
                {
                    return "Emails sent: " + Globals.Now.ToShortTimeString();
                }
            }


            internal const string NewRecord = "New Record.";

            internal const string OperationFailed = "Operation Failed";
            internal const string ConfirmationRequired = "Confirmation Required";
            internal const string ConfirmDropBefore = "Are you sure you want to move '{0}' before '{1}'?";
            internal const string ConfirmDropAfter = "Are you sure you want to move '{0}' after '{1}'?";
            internal const string ConfirmMerge = "Are you sure you want to merge '{0}' into '{1}'?";
            internal const string ConfirmDelete = "Are you sure you want to delete '{0}'?";
            internal const string ConfirmDeleteNoParam = "Are you sure you want to delete?";
            internal const string ConfirmActivateNoParam = "Are you sure you want to activate this feature?";
            internal const string ConfirmActivate = "Are you sure you want to activate this feature for {0}?";
            internal const string ConfirmDeactivateNoParam = "Are you sure you want to deactivate this feature?";
            internal const string ConfirmDeactivate = "Are you sure you want to deactivate this feature for {0}?";
            internal const string RequiredField = "'{0}' is a required field.";
            internal const string RequiredFieldGeneric = "This is a required field.";
            internal const string ValidationError = "Validation Error";
            internal const string SaveComplete = "'{0}' has been saved.";
            internal const string DeleteFailed = "'{0}' could not be deleted.";
            internal const string DeleteFailedBeingUsed = "'{0}' could not be deleted. It is being used.";
            internal const string ItemExist = "This item is already exist";
        }

        internal static class ListConstants
        {
            internal const string SelectAll = "[Select All]";
            internal const string SelectEmpty = "[Select Empty]";
        }

        internal static class DayOfWeekConstants
        {
            internal const string Monday = "Monday";
            internal const string Tuesday = "Tuesday";
            internal const string Wednesday = "Wednesday";
            internal const string Thursday = "Thursday";
            internal const string Friday = "Friday";
            internal const string Saturday = "Saturday";
            internal const string Sunday = "Sunday";
        }

        internal static class EndPointAddresses
        {
            const string name = @"ClientBin/Mimosa.Apartment.Silverlight.UI.xap";

            private static string ReplaceXapName(string replaceWith)
            {
                string result = App.Current.Host.Source.AbsoluteUri.Replace(name, replaceWith);
                return result;
            }
        }

        internal static UserLogin UserLogin { get; set; }

        internal static AppSettings AppSettings { get; set; }

        internal static DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
        }

        internal static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public static bool IsBusy
        {
            get
            {
                MainPage root = Application.Current.RootVisual as MainPage;
                if (root != null)
                {
                    return root.IsBusy;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                MainPage root = Application.Current.RootVisual as MainPage;
                if (root != null)
                {
                    root.IsBusy = value;
                }
            }
        }

        internal static bool IsShowEmployeeSatisfaction
        {
            get;
            set;
        }

        internal const int DayGetEmployeeSatisfaction = 1;

        internal const int MaxShiftDuration = 12;

        internal const int MaxSMSContent = 160;

        internal const int MaxEmailContent = 8000;

        internal static LayoutManager LayoutManager
        {
            get
            {
                return App.Current.Resources["LayoutManager"] as LayoutManager;
            }
        }

        internal static void ApplyTheme(string themeName, bool reloadPage)
        {
            if (Application.Current.Resources.MergedDictionaries.Count == 2)
            {
                ResourceDictionary rd = new ResourceDictionary()
                {
                    Source = new Uri("/Assets/Themes/" + themeName + "/MasterResource.xaml", UriKind.Relative)
                };

                Application.Current.Resources.MergedDictionaries.RemoveAt(1);
                Application.Current.Resources.MergedDictionaries.Add(rd);
            }

            if (reloadPage)
            {
                Application.Current.RootVisual.SetValue(UserControl.ContentProperty, new MainPage());
            }
        }

        internal static ModuleTypes ActiveModule
        {
            get
            {
                if (!string.IsNullOrEmpty(CookiesHelper.GetCookie("ActiveModule")))
                {
                    return Enum<ModuleTypes>.Parse(CookiesHelper.GetCookie("ActiveModule"));
                }
                else
                {
                    return ModuleTypes.Administration;
                }
            }
            set
            {
                CookiesHelper.SetCookie("ActiveModule", value.ToString());
            }


        }
    }
}