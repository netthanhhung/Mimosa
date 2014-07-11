using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Browser;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using Mimosa.Apartment.Silverlight.UI.Resources;

namespace Mimosa.Apartment.Silverlight.UI
{
    public static class ResourceHelper
    {
        public static string GetReourceValue(string name)
        {
            if (Globals.AppSettings.GloblaCulture == "vi-vn")
            {
                return StringLibrary_VN.ResourceManager.GetString(name);
            }
            else
            {
                return StringLibrary_EN.ResourceManager.GetString(name);
            }
        }
    }
}
