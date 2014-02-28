using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class SiteGroup
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.SiteGroupId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.GroupName, GroupName)
			};
        }
    }
}
