using System;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Web.UI
{
    public partial class MimosaSettings
    {
        [Flags()]
        public enum Modules
        {
            OrgAdmin = 1,
            Sales = 2,
            Yield = 3,
            Energy = 4,
            Employees = 5,
            Operations = 6,
            Guests = 7,
            Operation = 8
        }

        public enum Setting
        {
            EnableAutoLoginComplete,
            PortalAdminOrganisationCode,
            GloblaCulture,
            NumberFormatCulture,
            DateTimeFormatCulture
        }

    }
}
