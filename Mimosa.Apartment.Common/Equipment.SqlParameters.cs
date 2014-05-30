using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Equipment
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.EquipmentId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.OrganisationId, OrganisationId)
				, Utilities.MakeInputParameter(ColumnNames.EquipmentName, EquipmentName)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
                , Utilities.MakeInputParameter(ColumnNames.IsLegacy, IsLegacy)
                , Utilities.MakeInputParameter(ColumnNames.RealPrice, RealPrice)
                , Utilities.MakeInputParameter(ColumnNames.RentPrice, RentPrice)
                , Utilities.MakeInputParameter(ColumnNames.Unit, Unit)
			};
        }
    }
}
