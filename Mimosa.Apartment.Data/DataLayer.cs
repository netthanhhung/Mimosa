using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using Mimosa.Apartment.Common;
using System.Data;
using System.Data.Common;

namespace Mimosa.Apartment.Data
{
    public sealed partial class DataLayer
    {
        #region Private Attributes
        private Database _db;
        private const int _extendedTimeout = 5400;

        #endregion

        #region Public Constructors
        public DataLayer()
        {
            //_db = DatabaseFactory.CreateDatabase();
            _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("Mimosa");
        }
        #endregion

        #region Common
        private void AddParameters(System.Data.Common.DbCommand command, SqlParameter[] sqlParams)
        {
            foreach (SqlParameter param in sqlParams)
            {
                _db.AddParameter(command, param.ParameterName, param.DbType, param.Size, param.Direction, param.IsNullable, param.Precision, param.Scale, param.SourceColumn, param.SourceVersion, param.Value);
            }
        }

        public void SaveRecord(Record record)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSave" + record.TypeName);
            AddParameters(cmd, sqlParams);
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecord(Record record, string currentUser)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("procSave" + record.TypeName);
            AddParameters(cmd, sqlParams);
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecord(Record record, string currentUser, string procName)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand(procName);
            AddParameters(cmd, sqlParams);
            AddParameters(cmd, new SqlParameter[] { new SqlParameter("CurrentUser", currentUser) });
            record.Concurrency = Utilities.ToByteArray(_db.ExecuteScalar(cmd));
            record.RecordId = (long)_db.GetParameterValue(cmd, record.TypeName + "ID");
        }

        public void SaveRecordExtended(Record record, string procName)
        {
            SqlParameter[] sqlParams = record.SqlParameters();
            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand(procName);
            AddParameters(cmd, sqlParams);
            _db.ExecuteScalar(cmd);
        }
        
        private DbCommand GetDbCommandWithExtendedTimeout(string storedProcedureName, params object[] parameterValues)
        {
            DbCommand result = this._db.GetStoredProcCommand(storedProcedureName, parameterValues);
            result.CommandTimeout = _extendedTimeout;

            return result;
        }

        private int ExecuteNonQueryWithExtendedTimeout(string storedProcedureName, params object[] parameterValues)
        {
            int result = 0;

            using (DbCommand cmd = this._db.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                cmd.CommandTimeout = _extendedTimeout;
                result = _db.ExecuteNonQuery(cmd);
            }

            return result;
        }

        public void DeleteRecord(long recordId, string recordType)
        {
            string store = "procDelete" + recordType;
            _db.ExecuteNonQuery(store, recordId);
        }
        #endregion

        #region UserAccount
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<string> ListUserName(string applicationName, int? orgId, string startsWith)
        {
            List<string> result = new List<string>();

            using (IDataReader reader = _db.ExecuteReader("procListUserName", applicationName, orgId, startsWith))
            {
                result.Clear();

                while (true)
                {
                    string userName = null;

                    if (null != reader && reader.Read())
                    {
                        userName = Utilities.ToString(reader["UserName"]);
                    }

                    if (string.IsNullOrEmpty(userName))
                    {
                        break;
                    }

                    result.Add(userName);
                }
            }

            return result;
        }

        public int? UpdateMemberhipUserName(string applicationName, string userName, string newUserName)
        {
            int? result = null;

            SqlParameter[] sqlParams = new SqlParameter[]
			{ 
				Utilities.MakeInputParameter("ApplicationName", applicationName),
				Utilities.MakeInputParameter("UserName", userName),
				Utilities.MakeInputParameter("NewUserName", newUserName)
			};

            System.Data.Common.DbCommand cmd = _db.GetStoredProcCommand("aspnet_Membership_UpdateUserName_Custom");
            AddParameters(cmd, sqlParams);

            result = Utilities.ToNInt(_db.ExecuteScalar(cmd));

            return result;
        }


        #endregion

        #region Security

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<UserRoleAuth> ListUserRoleAuth(int? orgId, Guid? userId, Guid? roleId)
        {
            List<UserRoleAuth> result = new List<UserRoleAuth>();

            using (IDataReader reader = _db.ExecuteReader("[procListUserRoleAuth]", orgId, userId, roleId))
            {
                Factory.FillUserRoleAuthList(result, reader);
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Component> ListComponent(int? componentId)
        {
            List<Component> result = new List<Component>();

            using (IDataReader reader = _db.ExecuteReader("procListComponent", componentId))
            {
                Factory.FillComponentList(result, reader);
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<RoleComponentPermission> ListRoleComponentPermission(Guid? roleId, int? componentId)
        {
            List<RoleComponentPermission> result = new List<RoleComponentPermission>();

            using (IDataReader reader = _db.ExecuteReader("procListRoleComponentPermission", roleId, componentId))
            {
                Factory.FillRoleComponentPermissionList(result, reader);
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<RoleComponentPermission> ListRoleComponentPermissionByUser(Guid? userId)
        {
            List<RoleComponentPermission> result = new List<RoleComponentPermission>();

            using (IDataReader reader = _db.ExecuteReader("procListRoleComponentPermissionByUser", userId))
            {
                Factory.FillRoleComponentPermissionList(result, reader);
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<AspRole> ListAspRole(Guid? roleId)
        {
            List<AspRole> result = new List<AspRole>();

            using (IDataReader reader = _db.ExecuteReader("procListAspRole", roleId))
            {
                Factory.FillAspRoleList(result, reader);
            }
            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void SaveAspRole(string applicationName, AspRole saveItem, string currentUser)
        {
            _db.ExecuteNonQuery("procSaveAspRole", applicationName, saveItem.RoleId, saveItem.RoleName,
                saveItem.LoweredRoleName, saveItem.Description, currentUser);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeleteAspRole(AspRole deleteItem)
        {
            _db.ExecuteNonQuery("procDeleteAspRole", deleteItem.RoleId);
        }


        #endregion

        #region Organisation

        /// <summary>
        /// Gets the specified Organisation record.
        /// </summary>
        /// <param name="recordId">The OrganisationId.</param>
        /// <returns>The Organisation object.</returns>
        public Organisation GetOrganisation(int recordId)
        {
            Organisation result = null;
            using (DbCommand cmd = this._db.GetStoredProcCommand("procGetOrganisation"))
            {
                AddParameters(cmd, new SqlParameter[] { new SqlParameter("OrganisationID", recordId) });
                using (IDataReader reader = this._db.ExecuteReader(cmd))
                {
                    result = Factory.PopulateOrganisation(reader);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorisationCode"></param>
        /// <returns></returns>
        public Organisation GetOrganisation(string authorisationCode)
        {
            Organisation result = null;

            using (IDataReader reader = _db.ExecuteReader("procGetOrganisationByAuthorisationCode", authorisationCode))
            {
                result = Factory.PopulateOrganisation(reader);
            }

            return result;
        }

        public int? GetOrganisationIdForEmployee(Guid userId)
        {
            int? result = null;

            result = _db.ExecuteScalar("procGetOrganisationIdForEmployee", userId) as int?;

            return result;
        }

        /// <summary>
        /// Lists the Organisation.
        /// </summary>
        /// <returns>The generic Organisation list.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Organisation> ListOrganisation()
        {
            List<Organisation> result = new List<Organisation>();
            using (IDataReader reader = this._db.ExecuteReader("procListOrganisation"))
            {
                Factory.PopulateOrganisationList(result, reader);
            }
            return result;
        }
        #endregion

        #region Sites
        /// <summary>
        /// Lists the Site.
        /// </summary>
        /// <returns>The generic Site list.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Site> ListSite(int? orgId, int? siteId, bool showLegacy)
        {
            List<Site> result = new List<Site>();
            using (IDataReader reader = this._db.ExecuteReader("procListSite", orgId, siteId, showLegacy))
            {
                Factory.PopulateSiteList(result, reader);
            }
            return result;
        }
        #endregion

        #region AspUser

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<AspUser> ListAspUser(int? orgId, Guid? userId, bool? isLegacy)
        {
            List<AspUser> result = new List<AspUser>();
            using (IDataReader reader = _db.ExecuteReader("procListAspUser", orgId, userId, isLegacy))
            {
                Factory.FillAspUserList(result, reader);
            }
            return result;
        }        
        #endregion        

        #region Contact Information
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Country> ListCountry(int? countryId)
        {
            List<Country> result = new List<Country>();

            using (IDataReader reader = _db.ExecuteReader("procListCountry", countryId))
            {
                Factory.FillCountryList(result, reader);
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<ContactInformation> ListContactInformation(int? contactInfoId)
        {
            List<ContactInformation> result = new List<ContactInformation>();

            using (IDataReader reader = _db.ExecuteReader("procListContactInformation", contactInfoId))
            {
                Factory.FillContactInformationList(result, reader);
            }

            return result;
        }
        #endregion

        #region Customer
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Customer> ListCustomer(int? customerId, string firstName, string lastName, bool includeLegacy)
        {
            List<Customer> result = new List<Customer>();

            using (IDataReader reader = _db.ExecuteReader("procListCustomer", customerId, firstName, lastName, includeLegacy))
            {
                Factory.FillCustomerList(result, reader);
            }

            return result;
        }
        #endregion

        #region TagVersion
        public TagVersion GetLatestTagVersion()
        {
            List<TagVersion> result = new List<TagVersion>();

            using (IDataReader reader = _db.ExecuteReader("procGetLatestTagVersion"))
            {
                Factory.PopulateTagVersionsList(result, reader);
            }

            if (result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Equipment
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Equipment> ListEquipment(int? organisationId, int? equipmentId, bool showLegacy)
        {
            List<Equipment> result = new List<Equipment>();

            using (IDataReader reader = _db.ExecuteReader("procListEquipment", organisationId, equipmentId, showLegacy))
            {
                Factory.FillEquipmentList(result, reader);
            }
            return result;
        }
        #endregion

        #region Service
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Service> ListService(int? organisationId, int? serviceId, bool showLegacy)
        {
            List<Service> result = new List<Service>();

            using (IDataReader reader = _db.ExecuteReader("procListService", organisationId, serviceId, showLegacy))
            {
                Factory.FillServiceList(result, reader);
            }
            return result;
        }
        #endregion

        #region RoomType
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<RoomType> ListRoomType(int? organisationId, int? siteId, bool showLegacy)
        {
            List<RoomType> result = new List<RoomType>();

            using (IDataReader reader = _db.ExecuteReader("procListRoomType", organisationId, siteId, showLegacy))
            {
                Factory.FillRoomTypeList(result, reader);
            }
            return result;
        }
        #endregion

        #region Room
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Room> ListRoom(int orgId, int? siteId, int? roomId, string roomName, string roomStatusIds, string roomTypeIds, int? floor, bool showLegacy)
        {
            List<Room> result = new List<Room>();

            using (IDataReader reader = _db.ExecuteReader("procListRoom", orgId, siteId, roomId, roomName, roomStatusIds, roomTypeIds, floor, showLegacy))
            {
                Factory.FillRoomList(result, reader);
            }
            return result;
        }
        #endregion

        #region RoomEquipment
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<RoomEquipment> ListRoomEquipment(int? roomEquipmentId, int? roomId)
        {
            List<RoomEquipment> result = new List<RoomEquipment>();

            using (IDataReader reader = _db.ExecuteReader("procListRoomEquipment", roomEquipmentId, roomId))
            {
                Factory.FillRoomEquipmentList(result, reader);
            }
            return result;
        }
        #endregion

        #region RoomService
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<RoomService> ListRoomService(int? roomServiceId, int? roomId)
        {
            List<RoomService> result = new List<RoomService>();

            using (IDataReader reader = _db.ExecuteReader("procListRoomService", roomServiceId, roomId))
            {
                Factory.FillRoomServiceList(result, reader);
            }
            return result;
        }
        #endregion

        #region Image
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Image> ListImage(int? imageId, int? itemId, int? imageTypeId, int loadType)
        {
            List<Image> result = new List<Image>();

            using (IDataReader reader = _db.ExecuteReader("procListImage", imageId, itemId, imageTypeId, loadType))
            {
                Factory.FillImageList(result, reader);
            }
            return result;
        }
        #endregion

        #region Booking
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<Booking> ListBooking(int orgId, int? siteId, int? roomId, string roomName, int? bookingId, string bookingStatusIds, 
            string customerName, DateTime? bookDateStart, DateTime? bookDateEnd)
        {
            List<Booking> result = new List<Booking>();

            using (IDataReader reader = _db.ExecuteReader("procListBooking", orgId, siteId, roomId, roomName, bookingId, bookingStatusIds, 
                customerName, bookDateStart, bookDateEnd))
            {
                Factory.FillBookingList(result, reader);
            }
            return result;
        }

        #endregion

        #region BookingRoomEquipment
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<BookingRoomEquipment> ListBookingRoomEquipment(int? bookingRoomEquipmentId, int? bookingId, int? roomEquipmentId)
        {
            List<BookingRoomEquipment> result = new List<BookingRoomEquipment>();

            using (IDataReader reader = _db.ExecuteReader("procListBookingRoomEquipment", bookingRoomEquipmentId, bookingId, roomEquipmentId))
            {
                Factory.FillBookingRoomEquipmentList(result, reader);
            }
            return result;
        }
        #endregion

        #region BookingRoomService
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<BookingRoomService> ListBookingRoomService(int? bookingRoomServiceId, int? bookingId, int? roomServiceId)
        {
            List<BookingRoomService> result = new List<BookingRoomService>();

            using (IDataReader reader = _db.ExecuteReader("procListBookingRoomService", bookingRoomServiceId, bookingId, roomServiceId))
            {
                Factory.FillBookingRoomServiceList(result, reader);
            }
            return result;
        }
        #endregion
    }
}
