using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Organisation
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.OrganisationId, NullableRecordId),
				Utilities.MakeInputParameter(ColumnNames.Name, Name),
				Utilities.MakeInputParameter(ColumnNames.ContactInformationId, ContactInformationId),
				Utilities.MakeInputParameter(ColumnNames.DisplayIndex, DisplayIndex),
				Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy),
				Utilities.MakeInputParameter(ColumnNames.AuthorisationCode, AuthorisationCode)
			};
        }
    }
}
