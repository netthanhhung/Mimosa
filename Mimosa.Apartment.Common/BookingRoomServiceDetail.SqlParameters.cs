using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class BookingRoomServiceDetail
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.BookingRoomServiceDetailId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.BookingRoomServiceId, BookingRoomServiceId)
                , Utilities.MakeInputParameter(ColumnNames.Quantity, Quantity)
                , Utilities.MakeInputParameter(ColumnNames.DateStart, DateStart)
                , Utilities.MakeInputParameter(ColumnNames.DateEnd, DateEnd)
                , Utilities.MakeInputParameter(ColumnNames.TotalPrice, TotalPrice)	
			    , Utilities.MakeInputParameter(ColumnNames.Payment, Payment)	
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                
			};
        }
    }
}
