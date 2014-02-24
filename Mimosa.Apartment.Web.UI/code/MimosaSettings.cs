using System;
using System.Web;
using Mimosa.Apartment.Common;
using System.Configuration;

namespace Mimosa.Apartment.Web.UI
{
    public partial class MimosaSettings
    {
        
        public static int GetModuleCount
        {
            get { return System.Enum.GetNames(typeof(MimosaSettings.Modules)).Length; }
        }

        public static String[] GetModuleNames()
        {
            return System.Enum.GetNames(typeof(MimosaSettings.Modules));
        }

        public static int GetModuleValue(int index)
        {
            return (int)System.Enum.GetValues(typeof(MimosaSettings.Modules)).GetValue(index);
        }

        //public static void CacheSettings()
        //{
        //    _applicationSettings.CacheSettings();
        //}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool IsEnableAutoLoginComplete()
        {            
            return Convert.ToBoolean(ConfigurationSettings.AppSettings["EnableAutoLoginComplete"]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static string PortalAdminOrganisationCode()
        {
            return Convert.ToString(ConfigurationSettings.AppSettings["PortalAdminOrganisationCode"]);

        }
    }
}
