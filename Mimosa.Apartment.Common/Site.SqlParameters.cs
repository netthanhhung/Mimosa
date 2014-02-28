using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Site
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.SiteId, NullableRecordId),
				Utilities.MakeInputParameter(ColumnNames.OrganisationId, OrganisationId),
                Utilities.MakeInputParameter(ColumnNames.HotelId, HotelId),                
				Utilities.MakeInputParameter(ColumnNames.Name, Name),
                Utilities.MakeInputParameter(ColumnNames.AbbreviatedName, AbbreviatedName),
                Utilities.MakeInputParameter(ColumnNames.ContactInformationID, ContactInformationID),
                Utilities.MakeInputParameter(ColumnNames.LicenseKey, LicenseKey),
				Utilities.MakeInputParameter(ColumnNames.StarRating, StarRating),
                Utilities.MakeInputParameter(ColumnNames.PropCode, PropCode),
				Utilities.MakeInputParameter(ColumnNames.DisplayIndex, DisplayIndex),
				Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy),
				Utilities.MakeInputParameter(ColumnNames.Availability, Availability)
                
			};
        }
    }
}
