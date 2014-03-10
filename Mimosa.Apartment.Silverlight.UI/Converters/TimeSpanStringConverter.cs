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
using System.Windows.Data;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class TimeSpanStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return null;
            }

            int h = 0;
            int m = 0;

            string[] timeTokens = value.ToString().Split(':');
            if (timeTokens.Length > 0)
            {
                h = int.Parse(timeTokens[0]);

                if (timeTokens.Length > 1)
                {
                    m = int.Parse(timeTokens[1]);
                }
            }

            TimeSpan result = new TimeSpan(h, m, 0);
            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return null;
            }

            string result = null;

            TimeSpan ts;
            if (TimeSpan.TryParse(value.ToString(), out ts))
            {
                const string format = "{0:D2}:{1:D2}";
                result = string.Format(format, ts.Hours, ts.Minutes);
            }

            return result;
        }
    }
}
