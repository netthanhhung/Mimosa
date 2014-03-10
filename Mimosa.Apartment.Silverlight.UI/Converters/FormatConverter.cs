using System;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Automation;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class FormatConverter : IValueConverter
    {
        private const string _paramTemplate = "Date: {0:ddMMyyyy}"; // "Money: {0:c}"

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return String.Format(parameter as string, value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
