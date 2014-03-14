using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Service
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ServiceId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.OrganisationId, OrganisationId)
				, Utilities.MakeInputParameter(ColumnNames.Name, Name)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
                , Utilities.MakeInputParameter(ColumnNames.Price, Price)
			};
        }
    }
}
