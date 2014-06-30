using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class District
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.DistrictId, NullableRecordId),
				Utilities.MakeInputParameter(ColumnNames.CityId, CityId),
                Utilities.MakeInputParameter(ColumnNames.Name, Name)
			};
        }
    }
}
