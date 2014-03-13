using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Component
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ComponentId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.Name, Name)
			};
        }
    }
}
