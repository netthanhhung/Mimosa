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
            _db.ExecuteNonQuery("procDelete", recordId, recordType);
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
            using (IDataReader reader = this._db.ExecuteReader(CommandType.StoredProcedure, "procListOrganisation"))
            {
                Factory.PopulateOrganisationList(result, reader);
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
        public List<Customer> ListCustomer(int? customerId, string name, bool includeLegacy)
        {
            List<Customer> result = new List<Customer>();

            using (IDataReader reader = _db.ExecuteReader("procListCustomer", customerId, name, includeLegacy))
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
    }
}
