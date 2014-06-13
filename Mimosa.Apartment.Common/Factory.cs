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

        #region Component

        public static Component Component(System.Data.IDataReader reader)
        {
            Component result = null;

            if (null != reader && reader.Read())
            {
                result = new Component();
                PopulateComponent(result, reader);
            }

            return result;
        }

        public static void PopulateComponent(Component input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.ComponentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Component.ColumnNames.ComponentId]);
            input.Name = Utilities.ToString(reader[Mimosa.Apartment.Common.Component.ColumnNames.Name]);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillComponentList(List<Component> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Component item;
            while (true)
            {
                item = Factory.Component(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region RoleComponentPermission

        public static RoleComponentPermission RoleComponentPermission(System.Data.IDataReader reader)
        {
            RoleComponentPermission result = null;

            if (null != reader && reader.Read())
            {
                result = new RoleComponentPermission();
                PopulateRoleComponentPermission(result, reader);
            }

            return result;
        }

        public static void PopulateRoleComponentPermission(RoleComponentPermission input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RoleComponentPermissionId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoleComponentPermission.ColumnNames.RoleComponentPermissionId]);
            input.RoleId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.RoleComponentPermission.ColumnNames.RoleId]);
            input.ComponentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoleComponentPermission.ColumnNames.ComponentId]);
            input.WriteRight = Utilities.ToNBool(reader[Mimosa.Apartment.Common.RoleComponentPermission.ColumnNames.WriteRight]);
            input.RoleName = Utilities.ToString(reader[Mimosa.Apartment.Common.RoleComponentPermission.ColumnNamesExtended.RoleName]);
            input.ComponentName = Utilities.ToString(reader[Mimosa.Apartment.Common.RoleComponentPermission.ColumnNamesExtended.ComponentName]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRoleComponentPermissionList(List<RoleComponentPermission> list, System.Data.IDataReader reader)
        {
            list.Clear();
            RoleComponentPermission item;
            while (true)
            {
                item = Factory.RoleComponentPermission(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region AspRole

        public static AspRole AspRole(System.Data.IDataReader reader)
        {
            AspRole result = null;

            if (null != reader && reader.Read())
            {
                result = new AspRole();
                PopulateAspRole(result, reader);
            }

            return result;
        }

        public static void PopulateAspRole(AspRole input, System.Data.IDataReader reader)
        {
            input.ApplicationId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.AspRole.ColumnNames.ApplicationId]);
            input.RoleId = Utilities.ToGuid(reader[Mimosa.Apartment.Common.AspRole.ColumnNames.RoleId]);
            input.RoleName = Utilities.ToString(reader[Mimosa.Apartment.Common.AspRole.ColumnNames.RoleName]);
            input.LoweredRoleName = Utilities.ToString(reader[Mimosa.Apartment.Common.AspRole.ColumnNames.LoweredRoleName]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.AspRole.ColumnNames.Description]);
            input.CanDelete = Utilities.ToBool(reader[Mimosa.Apartment.Common.AspRole.ColumnNames.CanDelete]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillAspRoleList(List<AspRole> list, System.Data.IDataReader reader)
        {
            list.Clear();
            AspRole item;
            while (true)
            {
                item = Factory.AspRole(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region AspUser

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
            
            input.OrganisationId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.OrganisationId]);
            input.SiteId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.AspUser.ColumnNames.SiteId]);
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
            input.District = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.District]);
            input.City = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.City]);
            input.State = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.State]);
            input.Postcode = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Postcode]);
            input.CountryId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.CountryId]);
            input.PhoneNumber = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.PhoneNumber]);
            input.FaxNumber = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.FaxNumber]);
            input.Email = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Email]);
            input.DoB = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.DoB]);
            input.Visa = Utilities.ToString(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.Visa]);
            input.VisaValidFrom = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.VisaValidFrom]);
            input.VisaValidTo = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.ContactInformation.ColumnNames.VisaValidTo]);
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

        #region SiteGroup

        public static SiteGroup SiteGroup(System.Data.IDataReader reader)
        {
            SiteGroup result = null;

            if (null != reader && reader.Read())
            {
                result = new SiteGroup();
                PopulateSiteGroup(result, reader);
            }

            return result;
        }

        public static void PopulateSiteGroup(SiteGroup input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.SiteGroupId = Utilities.ToInt(reader[Mimosa.Apartment.Common.SiteGroup.ColumnNames.SiteGroupId]);
            input.GroupName = Utilities.ToString(reader[Mimosa.Apartment.Common.SiteGroup.ColumnNames.GroupName]);
            input.CanDelete = Utilities.ToBool(reader[Mimosa.Apartment.Common.SiteGroup.ColumnNamesExtended.CanDelete]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillSiteGroupList(List<SiteGroup> list, System.Data.IDataReader reader)
        {
            list.Clear();
            SiteGroup item;
            while (true)
            {
                item = Factory.SiteGroup(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region SiteGroupSite

        public static SiteGroupSite SiteGroupSite(System.Data.IDataReader reader)
        {
            SiteGroupSite result = null;

            if (null != reader && reader.Read())
            {
                result = new SiteGroupSite();
                PopulateSiteGroupSite(result, reader);
            }

            return result;
        }

        public static void PopulateSiteGroupSite(SiteGroupSite input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.SiteGroupSiteId = Utilities.ToInt(reader[Mimosa.Apartment.Common.SiteGroupSite.ColumnNames.SiteGroupSiteId]);
            input.SiteGroupId = Utilities.ToInt(reader[Mimosa.Apartment.Common.SiteGroupSite.ColumnNames.SiteGroupId]);
            input.SiteId = Utilities.ToInt(reader[Mimosa.Apartment.Common.SiteGroupSite.ColumnNames.SiteId]);

            input.SiteName = Utilities.ToString(reader[Mimosa.Apartment.Common.SiteGroupSite.ColumnNamesExtended.SiteName]);
            input.GroupName = Utilities.ToString(reader[Mimosa.Apartment.Common.SiteGroupSite.ColumnNamesExtended.GroupName]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillSiteGroupSiteList(List<SiteGroupSite> list, System.Data.IDataReader reader)
        {
            list.Clear();
            SiteGroupSite item;
            while (true)
            {
                item = Factory.SiteGroupSite(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion


        #region Site
        /// <summary>
        /// Populate a Site object.
        /// </summary>
        /// <param name="input">The Site object to populate.</param>
        /// <param name="reader">The data reader from which to populate the Site object.</param>
        public static void PopulateSite(Site input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.SiteId = Utilities.ToInt(reader["SiteID"]);
            input.OrganisationId = Utilities.ToInt(reader["OrganisationId"]);
            input.HotelId = Utilities.ToNInt(reader["HotelId"]);
            input.Name = Utilities.ToString(reader["Name"]);
            input.AbbreviatedName = Utilities.ToString(reader["AbbreviatedName"]);
            input.ContactInformationID = Utilities.ToNInt(reader["ContactInformationID"]);
            input.LicenseKey = Utilities.ToNGuid(reader["LicenseKey"]);
            input.StarRating = Utilities.ToNDecimal(reader["StarRating"]);
            input.PropCode = Utilities.ToString(reader["PropCode"]);
            input.DisplayIndex = Utilities.ToInt(reader["DisplayIndex"]);
            input.IsLegacy = Utilities.ToBool(reader["IsLegacy"]);
            input.Availability = Utilities.ToInt(reader["Availability"]);
        }

        /// <summary>
        /// Creates a Site object using data from a data reader.
        /// </summary>
        /// <param name="reader">A data reader containing a key and corresponding value for each property of a Site object.</param>
        /// <returns>A populated Site object.</returns>
        public static Site PopulateSite(System.Data.IDataReader reader)
        {
            Site result = null;
            if (reader != null && reader.Read())
            {
                result = new Site();
                PopulateSite(result, reader);
            }
            return result;
        }

        /// <summary>
        /// Populates a generic Site list with data from a data reader.
        /// </summary>
        /// <param name="list">The Site list to be populated.</param>
        /// <param name="reader">A data reader containing data for zero or more Site records.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void PopulateSiteList(List<Site> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Site item;
            while (true)
            {
                item = Factory.PopulateSite(reader);
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
            input.OrganisationId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.OrganisationId]);
            input.FirstName = Utilities.ToString(reader[Mimosa.Apartment.Common.Customer.ColumnNames.FirstName]);
            input.LastName = Utilities.ToString(reader[Mimosa.Apartment.Common.Customer.ColumnNames.LastName]);
            input.IsLegacy = Utilities.ToBool(reader[Mimosa.Apartment.Common.Customer.ColumnNames.IsLegacy]);
            input.Gender = Utilities.ToNInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.Gender]);
            input.Age = Utilities.ToNInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.Age]);
            input.ContactInformationId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Customer.ColumnNames.ContactInformationId]);
            input.SiteName = Utilities.ToString(reader[Mimosa.Apartment.Common.Customer.ColumnNames.SiteName]);
            input.RoomName = Utilities.ToString(reader[Mimosa.Apartment.Common.Customer.ColumnNames.RoomName]);            
            
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

        #region Equipment

        public static Equipment Equipment(System.Data.IDataReader reader)
        {
            Equipment result = null;

            if (null != reader && reader.Read())
            {
                result = new Equipment();
                PopulateEquipment(result, reader);
            }

            return result;
        }

        public static void PopulateEquipment(Equipment input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.EquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.EquipmentId]);
            input.OrganisationId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.OrganisationId]);
            input.EquipmentName = Utilities.ToString(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.EquipmentName]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.Description]);
            input.IsLegacy = Utilities.ToBool(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.IsLegacy]);
            input.RealPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.RealPrice]);
            input.RentPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.RentPrice]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.Unit]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillEquipmentList(List<Equipment> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Equipment item;
            while (true)
            {
                item = Factory.Equipment(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Service

        public static Service Service(System.Data.IDataReader reader)
        {
            Service result = null;

            if (null != reader && reader.Read())
            {
                result = new Service();
                PopulateService(result, reader);
            }

            return result;
        }

        public static void PopulateService(Service input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.ServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Service.ColumnNames.ServiceId]);
            input.OrganisationId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Service.ColumnNames.OrganisationId]);
            input.Name = Utilities.ToString(reader[Mimosa.Apartment.Common.Service.ColumnNames.Name]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.Service.ColumnNames.Description]);
            input.IsLegacy = Utilities.ToBool(reader[Mimosa.Apartment.Common.Service.ColumnNames.IsLegacy]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Service.ColumnNames.Price]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.Equipment.ColumnNames.Unit]);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillServiceList(List<Service> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Service item;
            while (true)
            {
                item = Factory.Service(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region RoomType

        public static RoomType RoomType(System.Data.IDataReader reader)
        {
            RoomType result = null;

            if (null != reader && reader.Read())
            {
                result = new RoomType();
                PopulateRoomType(result, reader);
            }

            return result;
        }

        public static void PopulateRoomType(RoomType input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RoomTypeId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomType.ColumnNames.RoomTypeId]);            
            input.Name = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomType.ColumnNames.Name]);
            input.OrganisationId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.RoomType.ColumnNames.OrganisationId]);
            input.SiteId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.RoomType.ColumnNames.SiteId]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomType.ColumnNames.Description]);
            input.IsLegacy = Utilities.ToBool(reader[Mimosa.Apartment.Common.RoomType.ColumnNames.IsLegacy]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRoomTypeList(List<RoomType> list, System.Data.IDataReader reader)
        {
            list.Clear();
            RoomType item;
            while (true)
            {
                item = Factory.RoomType(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Room

        public static Room Room(System.Data.IDataReader reader)
        {
            Room result = null;

            if (null != reader && reader.Read())
            {
                result = new Room();
                PopulateRoom(result, reader);
            }

            return result;
        }

        public static void PopulateRoom(Room input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RoomId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Room.ColumnNames.RoomId]);
            input.SiteId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Room.ColumnNames.SiteId]);
            input.RoomName = Utilities.ToString(reader[Mimosa.Apartment.Common.Room.ColumnNames.RoomName]);
            input.RoomStatusId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Room.ColumnNames.RoomStatusId]);
            input.RoomStatus = Utilities.ToString(reader[Mimosa.Apartment.Common.Room.ColumnNames.RoomStatus]);
            input.RoomTypeId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Room.ColumnNames.RoomTypeId]);
            input.RoomType = Utilities.ToString(reader[Mimosa.Apartment.Common.Room.ColumnNames.RoomType]);
            input.IsLegacy = Utilities.ToBool(reader[Mimosa.Apartment.Common.Room.ColumnNames.IsLegacy]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.Room.ColumnNames.Description]);
            input.Width = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Room.ColumnNames.Width]);
            input.Height = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Room.ColumnNames.Height]);
            input.MeterSquare = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Room.ColumnNames.MeterSquare]);
            input.Floor = Utilities.ToNInt(reader[Mimosa.Apartment.Common.Room.ColumnNames.Floor]);
            input.BasePrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Room.ColumnNames.BasePrice]);

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRoomList(List<Room> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Room item;
            while (true)
            {
                item = Factory.Room(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region RoomEquipment

        public static RoomEquipment RoomEquipment(System.Data.IDataReader reader)
        {
            RoomEquipment result = null;

            if (null != reader && reader.Read())
            {
                result = new RoomEquipment();
                PopulateRoomEquipment(result, reader);
            }

            return result;
        }

        public static void PopulateRoomEquipment(RoomEquipment input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RoomEquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.RoomEquipmentId]);
            input.RoomId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.RoomId]);
            input.EquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.EquipmentId]);
            input.Equipment = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.Equipment]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.Price]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.Description]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomEquipment.ColumnNames.Unit]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRoomEquipmentList(List<RoomEquipment> list, System.Data.IDataReader reader)
        {
            list.Clear();
            RoomEquipment item;
            while (true)
            {
                item = Factory.RoomEquipment(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region RoomService

        public static RoomService RoomService(System.Data.IDataReader reader)
        {
            RoomService result = null;

            if (null != reader && reader.Read())
            {
                result = new RoomService();
                PopulateRoomService(result, reader);
            }

            return result;
        }

        public static void PopulateRoomService(RoomService input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.RoomServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.RoomServiceId]);
            input.RoomId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.RoomId]);
            input.ServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.ServiceId]);
            input.Service = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.Service]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.Price]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.Description]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.RoomService.ColumnNames.Unit]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillRoomServiceList(List<RoomService> list, System.Data.IDataReader reader)
        {
            list.Clear();
            RoomService item;
            while (true)
            {
                item = Factory.RoomService(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Image

        public static Image Image(System.Data.IDataReader reader)
        {
            Image result = null;

            if (null != reader && reader.Read())
            {
                result = new Image();
                PopulateImage(result, reader);
            }

            return result;
        }

        public static void PopulateImage(Image input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.ImageId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Image.ColumnNames.ImageId]);
            input.ImageTypeId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Image.ColumnNames.ImageTypeId]);
            input.ItemId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Image.ColumnNames.ItemId]);
            input.FileName = Utilities.ToString(reader[Mimosa.Apartment.Common.Image.ColumnNames.FileName]);
            input.ImageContent = Utilities.ToByteArray(reader[Mimosa.Apartment.Common.Image.ColumnNames.ImageContent]);
            input.ImageSmallContent = Utilities.ToByteArray(reader[Mimosa.Apartment.Common.Image.ColumnNames.ImageSmallContent]);
            input.DisplayIndex = Utilities.ToNInt(reader[Mimosa.Apartment.Common.Image.ColumnNames.DisplayIndex]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.Image.ColumnNames.Description]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillImageList(List<Image> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Image item;
            while (true)
            {
                item = Factory.Image(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region Booking

        public static Booking Booking(System.Data.IDataReader reader)
        {
            Booking result = null;

            if (null != reader && reader.Read())
            {
                result = new Booking();
                PopulateBooking(result, reader);
            }

            return result;
        }

        public static void PopulateBooking(Booking input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.BookingId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Booking.ColumnNames.BookingId]);
            input.SiteId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Booking.ColumnNames.SiteId]);
            input.SiteName = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.SiteName]);
            input.RoomId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Booking.ColumnNames.RoomId]);
            input.RoomName = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.RoomName]);
            input.CustomerId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Booking.ColumnNames.CustomerId]);
            input.FirstName = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.FirstName]);
            input.LastName = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.LastName]);            
            input.CustomerName = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.CustomerName]);
            input.Customer2Id = Utilities.ToNInt(reader[Mimosa.Apartment.Common.Booking.ColumnNames.Customer2Id]);
            input.Customer2Name = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.Customer2Name]);
            input.BookDate = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.Booking.ColumnNames.BookDate]);
            input.BookingStatusId = Utilities.ToInt(reader[Mimosa.Apartment.Common.Booking.ColumnNames.BookingStatusId]);
            input.BookingStatus = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.BookingStatus]);
            
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.Booking.ColumnNames.Description]);
            input.RoomPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Booking.ColumnNames.RoomPrice]);
            input.TotalPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Booking.ColumnNames.TotalPrice]);
            input.ContractDateSign = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.Booking.ColumnNames.ContractDateSign]);
            input.ContractDateStart = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.Booking.ColumnNames.ContractDateStart]);
            input.ContractDateEnd = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.Booking.ColumnNames.ContractDateEnd]);
            input.RoomPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Booking.ColumnNames.RoomPrice]);
            input.RoomPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Booking.ColumnNames.RoomPrice]);
            input.ContractTotalPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.Booking.ColumnNames.ContractTotalPrice]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillBookingList(List<Booking> list, System.Data.IDataReader reader)
        {
            list.Clear();
            Booking item;
            while (true)
            {
                item = Factory.Booking(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region BookingPayment

        public static BookingPayment BookingPayment(System.Data.IDataReader reader)
        {
            BookingPayment result = null;

            if (null != reader && reader.Read())
            {
                result = new BookingPayment();
                PopulateBookingPayment(result, reader);
            }

            return result;
        }

        public static void PopulateBookingPayment(BookingPayment input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = Utilities.ToNInt(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.BookingPaymentId]);
            input.BookingId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.BookingId]);
            input.DateStart = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.DateStart]);
            input.DateEnd = Utilities.ToDateTime(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.DateEnd]);
            input.RoomPrice = Utilities.ToDecimal(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.RoomPrice]);
            input.EquipmentPrice = Utilities.ToDecimal(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.EquipmentPrice]);
            input.ServicePrice = Utilities.ToDecimal(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.ServicePrice]);
            input.TotalPrice = Utilities.ToDecimal(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.TotalPrice]);
            input.CustomerPaid = Utilities.ToDecimal(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.CustomerPaid]);
            input.Payment = Utilities.ToBool(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.Payment]);
            input.MoneyLeft = input.TotalPrice - input.CustomerPaid;
            input.SiteId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.SiteId]);
            input.SiteName = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.SiteName]);
            input.RoomId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.RoomId]);
            input.RoomName = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.RoomName]);
            input.CustomerId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.CustomerId]);
            input.CustomerName = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.CustomerName]);
            input.Customer2Id = Utilities.ToNInt(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.Customer2Id]);
            input.Customer2Name = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingPayment.ColumnNames.Customer2Name]);

            if (input.RecordId > 0)
            {
                input.BookingPaymentId = Convert.ToInt32(input.RecordId.Value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillBookingPaymentList(List<BookingPayment> list, System.Data.IDataReader reader)
        {
            list.Clear();
            BookingPayment item;
            while (true)
            {
                item = Factory.BookingPayment(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region BookingRoomEquipment

        public static BookingRoomEquipment BookingRoomEquipment(System.Data.IDataReader reader)
        {
            BookingRoomEquipment result = null;

            if (null != reader && reader.Read())
            {
                result = new BookingRoomEquipment();
                PopulateBookingRoomEquipment(result, reader);
            }

            return result;
        }

        public static void PopulateBookingRoomEquipment(BookingRoomEquipment input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.BookingRoomEquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.BookingRoomEquipmentId]);
            input.BookingId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.BookingId]);
            input.EquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.EquipmentId]);
            input.Equipment = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.Equipment]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.Price]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.Description]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.Unit]);
            input.CanDelete = Utilities.ToBool(reader["CanDelete"]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillBookingRoomEquipmentList(List<BookingRoomEquipment> list, System.Data.IDataReader reader)
        {
            list.Clear();
            BookingRoomEquipment item;
            while (true)
            {
                item = Factory.BookingRoomEquipment(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region BookingRoomEquipmentDetail

        public static BookingRoomEquipmentDetail BookingRoomEquipmentDetail(System.Data.IDataReader reader)
        {
            BookingRoomEquipmentDetail result = null;

            if (null != reader && reader.Read())
            {
                result = new BookingRoomEquipmentDetail();
                PopulateBookingRoomEquipmentDetail(result, reader);
            }

            return result;
        }

        public static void PopulateBookingRoomEquipmentDetail(BookingRoomEquipmentDetail input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.BookingRoomEquipmentDetailId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.BookingRoomEquipmentDetailId]);
            input.BookingRoomEquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.BookingRoomEquipmentId]);
            input.Quantity = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.Quantity]);
            input.DateStart = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.DateStart]);
            input.DateEnd = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.DateEnd]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.Unit]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.Price]);
            input.TotalPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.TotalPrice]);
            input.Payment = Utilities.ToBool(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.Payment]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.Description]);
            input.EquipmentId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.EquipmentId]);
            input.Equipment = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipmentDetail.ColumnNames.Equipment]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillBookingRoomEquipmentDetailList(List<BookingRoomEquipmentDetail> list, System.Data.IDataReader reader)
        {
            list.Clear();
            BookingRoomEquipmentDetail item;
            while (true)
            {
                item = Factory.BookingRoomEquipmentDetail(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region BookingRoomService

        public static BookingRoomService BookingRoomService(System.Data.IDataReader reader)
        {
            BookingRoomService result = null;

            if (null != reader && reader.Read())
            {
                result = new BookingRoomService();
                PopulateBookingRoomService(result, reader);
            }

            return result;
        }

        public static void PopulateBookingRoomService(BookingRoomService input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.BookingRoomServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomService.ColumnNames.BookingRoomServiceId]);
            input.BookingId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomService.ColumnNames.BookingId]);
            input.ServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomService.ColumnNames.ServiceId]);
            input.Service = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomService.ColumnNames.Service]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomService.ColumnNames.Price]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomService.ColumnNames.Description]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomEquipment.ColumnNames.Unit]);
            input.CanDelete = Utilities.ToBool(reader["CanDelete"]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillBookingRoomServiceList(List<BookingRoomService> list, System.Data.IDataReader reader)
        {
            list.Clear();
            BookingRoomService item;
            while (true)
            {
                item = Factory.BookingRoomService(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

        #region BookingRoomServiceDetail

        public static BookingRoomServiceDetail BookingRoomServiceDetail(System.Data.IDataReader reader)
        {
            BookingRoomServiceDetail result = null;

            if (null != reader && reader.Read())
            {
                result = new BookingRoomServiceDetail();
                PopulateBookingRoomServiceDetail(result, reader);
            }

            return result;
        }

        public static void PopulateBookingRoomServiceDetail(BookingRoomServiceDetail input, System.Data.IDataReader reader)
        {
            PopulateRecord(input, reader);
            input.RecordId = input.BookingRoomServiceDetailId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.BookingRoomServiceDetailId]);
            input.BookingRoomServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.BookingRoomServiceId]);
            input.Quantity = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.Quantity]);
            input.DateStart = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.DateStart]);
            input.DateEnd = Utilities.ToNDateTime(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.DateEnd]);
            input.Unit = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.Unit]);
            input.Price = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.Price]);
            input.TotalPrice = Utilities.ToNDecimal(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.TotalPrice]);
            input.Payment = Utilities.ToBool(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.Payment]);
            input.Description = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.Description]);
            input.ServiceId = Utilities.ToInt(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.ServiceId]);
            input.Service = Utilities.ToString(reader[Mimosa.Apartment.Common.BookingRoomServiceDetail.ColumnNames.Service]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void FillBookingRoomServiceDetailList(List<BookingRoomServiceDetail> list, System.Data.IDataReader reader)
        {
            list.Clear();
            BookingRoomServiceDetail item;
            while (true)
            {
                item = Factory.BookingRoomServiceDetail(reader);
                if (null == item) break;
                list.Add(item);
            }
        }
        #endregion

    }
}
