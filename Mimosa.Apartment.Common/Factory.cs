using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mimosa.Apartment.Common
{
    public static partial class Factory
    {
        private static void PopulateRecord(Record record, System.Data.IDataReader reader)
        {
            record.Concurrency = Utilities.ToByteArray(reader["Concurrency"]);
            record.DateCreated = Utilities.ToNDateTime(reader["DateCreated"]);
            record.DateUpdated = Utilities.ToNDateTime(reader["DateUpdated"]);
            record.CreatedBy = Utilities.ToString(reader["CreatedBy"]);
            record.UpdatedBy = Utilities.ToString(reader["UpdatedBy"]);
        }

        #region User
        public static AspUser AspUser(System.Data.IDataReader reader)
        {
            AspUser result = null;

            if (null != reader && reader.Read())
            {
                result = new AspUser();
                PopulateAspUser(result, reader);
            }

            return result;
        }

        public static void PopulateAspUser(AspUser input, System.Data.IDataReader reader)
        {
            input.ApplicationId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.ApplicationId]);
            input.UserId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.UserId]);
            input.Password = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.Password]);
            input.PasswordFormat = Utilities.ToInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.PasswordFormat]);
            input.PasswordSalt = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.PasswordSalt]);
            input.MobilePIN = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.MobilePIN]);
            input.Email = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.Email]);

            input.LoweredEmail = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.LoweredEmail]);
            input.PasswordQuestion = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.PasswordQuestion]);
            input.PasswordAnswer = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.PasswordAnswer]);
            input.IsApproved = Utilities.ToBool(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.IsApproved]);
            input.IsLockedOut = Utilities.ToBool(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.IsLockedOut]);
            input.CreationDate = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.CreateDate]);
            input.LastLoginDate = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.LastLoginDate]);
            input.LastPasswordChangedDate = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.LastPasswordChangedDate]);
            input.LastLockoutDate = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.LastLockoutDate]);

            input.FailedPasswordAttemptCount = Utilities.ToInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.FailedPasswordAttemptCount]);
            input.FailedPasswordAttemptWindowStart = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.FailedPasswordAttemptWindowStart]);
            input.FailedPasswordAnswerAttemptCount = Utilities.ToInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.FailedPasswordAnswerAttemptCount]);
            input.FailedPasswordAnswerAttemptWindowStart = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.FailedPasswordAnswerAttemptWindowStart]);
            input.Comment = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.Comment]);
            input.UserName = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.UserName]);
            input.LoweredUserName = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.LoweredUserName]);
            input.MobileAlias = Utilities.ToString(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.MobileAlias]);

            input.IsAnonymous = Utilities.ToBool(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.IsAnonymous]);
            input.LastActivityDate = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.LastActivityDate]);

            input.OrganisationId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.OrganisationId]);
            if (reader.ColumnExists(Mimosa.Apartment.Common.AspUser.ColumnNames.MinRoleLevel))
                input.MinRoleLevel = Utilities.ToNInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.MinRoleLevel]);
            else
                input.MinRoleLevel = 1000;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillAspUserList(List<AspUser> list, System.Data.IDataReader reader)
        {
            list.Clear();
            AspUser item;
            while (true)
            {
                item = Factory.AspUser(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region UserRoleAuth

        public static UserRoleAuth UserRoleAuth(System.Data.IDataReader reader)
        {
            UserRoleAuth result = null;

            if (null != reader && reader.Read())
            {
                result = new UserRoleAuth();
                PopulateUserRoleAuth(result, reader);
            }

            return result;
        }

        public static void PopulateUserRoleAuth(UserRoleAuth input, System.Data.IDataReader reader)
        {
            input.RecordId = input.UserRoleAuthId = Utilities.ToInt(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNames.UserRoleAuthId]);
            input.UserId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNames.UserId]);
            input.RoleId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNames.RoleId]);
            input.WholeOrg = Utilities.ToNBool(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNames.WholeOrg]);
            input.SiteGroupId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNames.SiteGroupId]);
            input.SiteId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNames.SiteId]);
            
            input.UserName = Utilities.ToString(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNamesExtended.UserName]);            
            input.RoleName = Utilities.ToString(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNamesExtended.RoleName]);

            input.SiteGroup = Utilities.ToString(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNamesExtended.SiteGroup]);
            input.Site = Utilities.ToString(reader[Mimosa.Apartment.Common.UserRoleAuth.ColumnNamesExtended.Site]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillUserRoleAuthList(List<UserRoleAuth> list, System.Data.IDataReader reader)
        {
            list.Clear();
            UserRoleAuth item;
            while (true)
            {
                item = Factory.UserRoleAuth(reader);
                if (null == item) break;
                list.Add(item);
            }
        }

        #endregion

        #region ContactInformation
        public static Country Country(System.Data.IDataReader reader)
        {
            Country result = null;

            if (null != reader && reader.Read())
            {
                result = new Country();
                PopulateCountry(result, reader);
            }

            return result;
        }

        public static void PopulateCountry(Country input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.CountryId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Country.ColumnNames.CountryId]);
            input.Name = Utilities.ToString(reader[Mimosa.Apartment.Common.Country.ColumnNames.Name]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCountryList(List<Country> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Country item;
            while (true)
            {
                item = Factory.Country(reader);
                if (null == item) break;
                list.Add(item);
            }
        }

        public static ContactInformation ContactInformation(System.Data.IDataReader reader)
        {
            ContactInformation result = null;

            if (null != reader && reader.Read())
            {
                result = new ContactInformation();
                PopulateContactInformation(result, reader);
            }

            return result;
        }

        public static void PopulateContactInformation(ContactInformation input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.ContactInformationId]);
            input.ContactTypeId = Utilities.ToInt(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.ContactTypeId]);
            input.FirstName = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.FirstName]);
            input.LastName = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.LastName]);
            input.Address = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Address]);
            input.Address2 = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Address2]);
            input.City = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.City]);
            input.State = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.State]);
            input.Postcode = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Postcode]);
            input.CountryId = Utilities.ToInt(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.CountryId]);
            input.PhoneNumber = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.PhoneNumber]);
            input.FaxNumber = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.FaxNumber]);
            input.Email = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Email]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillContactInformationList(List<ContactInformation> list, System.Data.IDataReader reader)
        {
            list.Clear();
            ContactInformation item;
            while (true)
            {
                item = Factory.ContactInformation(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Organisation
        /// <summary>
        /// Populate a Organisation object.
        /// </summary>
        /// <param name="input">The Organisation object to populate.</param>
        /// <param name="reader">The data reader from which to populate the Organisation object.</param>
        public static void PopulateOrganisation(Organisation input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.OrganisationId = Utilities.ToInt(reader["OrganisationID"]);
            input.Name = Utilities.ToString(reader["Name"]);
            input.ContactInformationId = Utilities.ToInt(reader["ContactInformationID"]);
            input.DisplayIndex = Utilities.ToInt(reader["DisplayIndex"]);
            input.IsLegacy = Utilities.ToBool(reader["IsLegacy"]);
            input.AuthorisationCode = Utilities.ToString(reader["AuthorisationCode"]);            
        }

        /// <summary>
        /// Creates a Organisation object using data from a data reader.
        /// </summary>
        /// <param name="reader">A data reader containing a key and corresponding value for each property of a Organisation object.</param>
        /// <returns>A populated Organisation object.</returns>
        public static Organisation PopulateOrganisation(System.Data.IDataReader reader)
        {
            Organisation result = null;
            if (reader != null && reader.Read())
            {
                result = new Organisation();
                PopulateOrganisation(result, reader);
            }
            return result;
        }

        /// <summary>
        /// Populates a generic Organisation list with data from a data reader.
        /// </summary>
        /// <param name="list">The Organisation list to be populated.</param>
        /// <param name="reader">A data reader containing data for zero or more Organisation records.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void PopulateOrganisationList(List<Organisation> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Organisation item;
            while (true)
            {
                item = Factory.PopulateOrganisation(reader);
                if (item == null) break;
                list.Add(item);
            }
        }
        #endregion

        #region Customer
        public static Customer Customer(System.Data.IDataReader reader)
        {
            Customer result = null;

            if (null != reader && reader.Read())
            {
                result = new Customer();
                PopulateCustomer(result, reader);
            }

            return result;
        }

        public static void PopulateCustomer(Customer input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.CustomerId]);
            input.Name = Utilities.ToString(reader[Mimosa.Apartment.Common.Customer.ColumnNames.Name]);
            input.BusinessName = Utilities.ToString(reader[Mimosa.Apartment.Common.Customer.ColumnNames.BusinessName]);
            input.ContactInformationId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.ContactInformationId]);
            input.ShippingContactInformationId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.ShippingContactInformationId]);
            input.IsLegacy = Utilities.ToBool(reader[Mimosa.Apartment.Common.Customer.ColumnNames.IsLegacy]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillCustomerList(List<Customer> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Customer item;
            while (true)
            {
                item = Factory.Customer(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region TagVersions
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void PopulateTagVersionsList(List<TagVersion> list, System.Data.IDataReader reader)
        {
            list.Clear();

            while (reader.Read())
            {
                TagVersion item = new TagVersion();
                PopulateTagVersion(item, reader);

                list.Add(item);
            }
        }

        public static void PopulateTagVersion(TagVersion input, System.Data.IDataReader reader)
        {
            input.TagVersionID = Utilities.ToInt(reader[TagVersion.ColumnNames.TagVersionID]);
            input.TagName = Utilities.ToString(reader[TagVersion.ColumnNames.TagName]);
            input.Version = Utilities.ToString(reader[TagVersion.ColumnNames.Version]);
            input.DateCreated = Utilities.ToDateTime(reader[TagVersion.ColumnNames.DateCreated]);
            input.CreatedBy = Utilities.ToString(reader[TagVersion.ColumnNames.CreatedBy]);
        }

        public static TagVersion PopulateTagVersion(System.Data.IDataReader reader)
        {
            TagVersion result = null;
            if (null != reader && reader.Read())
            {
                result = new TagVersion();
                PopulateTagVersion(result, reader);
            }

            return result;
        }
        #endregion
    }
}
