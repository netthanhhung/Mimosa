using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class BookingRoomEquipment
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.BookingRoomEquipmentId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.BookingId, BookingId)
                , Utilities.MakeInputParameter(ColumnNames.RoomEquipmentId, RoomEquipmentId)
				, Utilities.MakeInputParameter(ColumnNames.Price, Price)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                
			};
        }
    }
}
