using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Booking
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.BookingId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.RoomId, RoomId)
                , Utilities.MakeInputParameter(ColumnNames.CustomerId, CustomerId)
                , Utilities.MakeInputParameter(ColumnNames.BookDate, BookDate)
                , Utilities.MakeInputParameter(ColumnNames.BookingStatusId, BookingStatusId)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
				, Utilities.MakeInputParameter(ColumnNames.RoomPrice, RoomPrice)
                , Utilities.MakeInputParameter(ColumnNames.TotalPrice, TotalPrice)
                , Utilities.MakeInputParameter(ColumnNames.ContractDateSign, ContractDateSign)
                , Utilities.MakeInputParameter(ColumnNames.ContractDateStart, ContractDateStart)
                , Utilities.MakeInputParameter(ColumnNames.ContractDateEnd, ContractDateEnd)                
                , Utilities.MakeInputParameter(ColumnNames.ContractTotalPrice, ContractTotalPrice)
			};
        }
    }
}
