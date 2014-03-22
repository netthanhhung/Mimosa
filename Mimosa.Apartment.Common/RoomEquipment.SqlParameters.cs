using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class RoomEquipment
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.RoomEquipmentId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.RoomId, RoomId)
                , Utilities.MakeInputParameter(ColumnNames.EquipmentId, EquipmentId)
				, Utilities.MakeInputParameter(ColumnNames.Price, Price)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                
			};
        }
    }
}
