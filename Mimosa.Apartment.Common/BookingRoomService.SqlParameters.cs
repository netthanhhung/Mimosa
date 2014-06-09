using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class BookingRoomService
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.BookingRoomServiceId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.BookingId, BookingId)
                , Utilities.MakeInputParameter(ColumnNames.ServiceId, ServiceId)
				, Utilities.MakeInputParameter(ColumnNames.Price, Price)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                
			};
        }
    }
}
