using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class Image
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ImageId, NullableRecordId)
                , Utilities.MakeInputParameter(ColumnNames.ImageTypeId, ImageTypeId)
                , Utilities.MakeInputParameter(ColumnNames.ItemId, ItemId)
				, Utilities.MakeInputParameter(ColumnNames.FileName, FileName)                
                , Utilities.MakeInputParameter(ColumnNames.ImageContent, ImageContent)
                , Utilities.MakeInputParameter(ColumnNames.ImageSmallContent, ImageSmallContent)
                , Utilities.MakeInputParameter(ColumnNames.DisplayIndex, DisplayIndex)
                , Utilities.MakeInputParameter(ColumnNames.Description, Description)
			};
        }
    }
}
