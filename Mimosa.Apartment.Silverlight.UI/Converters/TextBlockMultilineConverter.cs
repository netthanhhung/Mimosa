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
using System.Globalization;
using System.Collections.Generic;

namespace Mimosa.Apartment.Silverlight.UI
{
    public class TextBlockMultilineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inlines = new List<Inline>();
            if (value != null)
            {
                foreach (var line in value.ToString().Split(','))
                {
                    inlines.Add(new Run() { Text = line });
                    inlines.Add(new LineBreak());
                }
                inlines.RemoveAt(inlines.Count - 1);
            }
            return inlines;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
