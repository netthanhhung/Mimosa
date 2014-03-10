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
using System.Globalization;
using System.Windows.Data;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class LayoutConverter : IValueConverter
    {
        private const string _paramTemplate = "w|-2";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = System.Convert.ToInt32(value);

            string[] param = Utilities.ToString(parameter).Split('|');
            string mode = param[0];
            int adjustValue = 0;
            if (param.Length > 1)
            {
                Int32.TryParse(param[1], out adjustValue);
            }
            switch (mode)
            {
                default: // width
                    result = Globals.LayoutManager.GetLayoutWidth(result) + adjustValue;
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


