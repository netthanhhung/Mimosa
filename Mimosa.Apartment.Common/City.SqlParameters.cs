using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class City
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.CityId, NullableRecordId),
				Utilities.MakeInputParameter(ColumnNames.CountryId, CountryId),
                Utilities.MakeInputParameter(ColumnNames.Name, Name)
			};
        }
    }
}
