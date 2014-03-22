using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Room
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.RoomId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.SiteId, SiteId)
				, Utilities.MakeInputParameter(ColumnNames.RoomName, RoomName)
                , Utilities.MakeInputParameter(ColumnNames.RoomStatusId, RoomStatusId)
                , Utilities.MakeInputParameter(ColumnNames.RoomTypeId, RoomTypeId)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)                
                , Utilities.MakeInputParameter(ColumnNames.Width, Width)
                , Utilities.MakeInputParameter(ColumnNames.Height, Height)
                , Utilities.MakeInputParameter(ColumnNames.MeterSquare, MeterSquare)
                , Utilities.MakeInputParameter(ColumnNames.Floor, Floor)
                , Utilities.MakeInputParameter(ColumnNames.BasePrice, BasePrice)
			};
        }
    }
}
