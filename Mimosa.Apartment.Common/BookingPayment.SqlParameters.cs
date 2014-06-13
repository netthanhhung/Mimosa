using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class BookingPayment
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.BookingPaymentId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.BookingId, BookingId)
                , Utilities.MakeInputParameter(ColumnNames.DateStart, DateStart)
				, Utilities.MakeInputParameter(ColumnNames.DateEnd, DateEnd)
                , Utilities.MakeInputParameter(ColumnNames.RoomPrice, RoomPrice)
                , Utilities.MakeInputParameter(ColumnNames.EquipmentPrice, EquipmentPrice)
                , Utilities.MakeInputParameter(ColumnNames.ServicePrice, ServicePrice)
                , Utilities.MakeInputParameter(ColumnNames.TotalPrice, TotalPrice)
                , Utilities.MakeInputParameter(ColumnNames.CustomerPaid, CustomerPaid)
                , Utilities.MakeInputParameter(ColumnNames.Payment, Payment)
                
			};
        }
    }
}
