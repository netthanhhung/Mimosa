using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class SiteGroupSite
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.SiteGroupSiteId, NullableRecordId)
				, Utilities.MakeInputParameter(ColumnNames.SiteId, SiteId)
                , Utilities.MakeInputParameter(ColumnNames.SiteGroupId, SiteGroupId)
			};
        }
    }
}
