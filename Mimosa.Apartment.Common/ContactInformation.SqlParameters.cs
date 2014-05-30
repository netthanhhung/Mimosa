using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public partial class ContactInformation
    {
        public override SqlParameter[] SqlParameters()
        {
            return new SqlParameter[]
			{
				Utilities.MakeInputOutputParameter(ColumnNames.ContactInformationId, NullableRecordId),
                Utilities.MakeInputParameter(ColumnNames.ContactTypeId, ContactTypeId),
                Utilities.MakeInputParameter(ColumnNames.FirstName, FirstName),
                Utilities.MakeInputParameter(ColumnNames.LastName, LastName),
				Utilities.MakeInputParameter(ColumnNames.Address, Address),
                Utilities.MakeInputParameter(ColumnNames.Address2, Address2),
                Utilities.MakeInputParameter(ColumnNames.District, District),
				Utilities.MakeInputParameter(ColumnNames.City, City),
				Utilities.MakeInputParameter(ColumnNames.State, State),
				Utilities.MakeInputParameter(ColumnNames.Postcode, Postcode),
				Utilities.MakeInputParameter(ColumnNames.CountryId, CountryId),
				Utilities.MakeInputParameter(ColumnNames.PhoneNumber, PhoneNumber),
				Utilities.MakeInputParameter(ColumnNames.FaxNumber, FaxNumber),
                Utilities.MakeInputParameter(ColumnNames.Email, Email),
                Utilities.MakeInputParameter(ColumnNames.DoB, DoB),
                Utilities.MakeInputParameter(ColumnNames.Visa, Visa),
                Utilities.MakeInputParameter(ColumnNames.VisaValidFrom, VisaValidFrom),
                Utilities.MakeInputParameter(ColumnNames.VisaValidTo, VisaValidTo),
			};
        }
    }
}
