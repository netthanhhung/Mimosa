using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class RoomService
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.RoomServiceId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.RoomId, RoomId)
                , Utilities.MakeInputParameter(ColumnNames.ServiceId, ServiceId)
				, Utilities.MakeInputParameter(ColumnNames.Price, Price)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                
			};
        }
    }
}
