using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Customer
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.CustomerId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.OrganisationId, OrganisationId)
				, Utilities.MakeInputParameter(ColumnNames.FirstName, FirstName)
				, Utilities.MakeInputParameter(ColumnNames.LastName, LastName)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
				, Utilities.MakeInputParameter(ColumnNames.Gender, Gender)
                , Utilities.MakeInputParameter(ColumnNames.Age, Age)
				, Utilities.MakeInputParameter(ColumnNames.ContactInformationId, ContactInformationId)
				

			};
        }
    }
}
