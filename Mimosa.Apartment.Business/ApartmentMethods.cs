using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mimosa.Apartment.Data;
using Mimosa.Apartment.Common;
using System.Security.Cryptography;

namespace Mimosa.Apartment.Business
{
    public static class ApartmentMethods
    {
        #region Common
        /// <summary>
        /// Calls the data layer to save the record object.
        /// The record's RecordId is used to determine if the save operation is an insert or an update.
        /// </summary>
        /// <param name="record">The record object containing the data to be saved.</param>
        public static void SaveRecord(Record record)
        {
            new DataLayer().SaveRecord(record, (record.NullableRecordId == null) ? record.CreatedBy : record.UpdatedBy);
        }       

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(Record record)
        {
            DataLayer dl = new DataLayer();
            String type = record.GetType().ToString();
            dl.DeleteRecord(Utilities.ToLong(record.RecordId), type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static void DeleteRecord(long id, Type recordType)
        {
            DataLayer temp = new DataLayer();
            String type = recordType.ToString();
            temp.DeleteRecord(id, type.Substring(type.LastIndexOf(".") + 1, (type.Length - (type.LastIndexOf(".") + 1))));
        }
       
        #endregion

        #region Organisation
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"),
        System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<Organisation> ListOrganisation(Guid? roleId)
        {
            DataLayer dataLayer = new DataLayer();
            List<Organisation> result = dataLayer.ListOrganisation();
            if (roleId.HasValue)
            {
                foreach (Organisation org in result)
                {
                    org.ContactInformation = dataLayer.ListContactInformation(org.ContactInformationId).FirstOrDefault();
                    org.OrgAdminUsers = dataLayer.ListUserRoleAuth(org.OrganisationId, null, roleId.Value);
                }
            }
            return result;
        }

        public static List<AspUser> ListOrgAdminAspUser(int orgId, Guid roleId)
        {
            DataLayer dataLayer = new DataLayer();
            List<AspUser> result = new List<AspUser>();
            List<UserRoleAuth> uraList = dataLayer.ListUserRoleAuth(orgId, null, roleId);
            if (uraList != null)
            {
                foreach (UserRoleAuth ura in uraList)
                {
                    result.AddRange(dataLayer.ListAspUser(orgId, ura.UserId, null));
                }
                result = result.OrderByDescending(i => i.LastActivityDate).ToList();
            }
            return result;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static void SaveOrganisations(List<Organisation> saveList)
        {
            if (saveList != null)
            {
                foreach (Organisation item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        if (item.ContactInformation != null)
                        {
                            SaveRecord(item.ContactInformation);
                            if (item.ContactInformation.NullableRecordId.HasValue
                                && item.ContactInformationId != Convert.ToInt32(item.ContactInformation.RecordId.Value))
                            {
                                item.ContactInformationId = Convert.ToInt32(item.ContactInformation.RecordId.Value);
                            }
                        }
                        SaveRecord(item);
                    }
                }
            }
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static Organisation GetOrganisation(int organisationId)
        {
            return new DataLayer().GetOrganisation(organisationId);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static Organisation GetOrganisation(string authorisationCode)
        {
            return new DataLayer().GetOrganisation(authorisationCode);
        }

        public static int? GetOrganisationIdForEmployee(Guid userId)
        {
            return new DataLayer().GetOrganisationIdForEmployee(userId);
        }
        #endregion

        #region UserAccount
        public static List<AspUser> ListAspUser(int? orgId, Guid? userId, bool? isLegacy)
        {
            return new DataLayer().ListAspUser(orgId, userId, isLegacy);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static List<string> ListUserName(string applicationName, int? orgId, string startsWith)
        {
            return new DataLayer().ListUserName(applicationName, orgId, startsWith);
        }

        public static int? UpdateMembershipUserName(string applicationName, string userName, string newUserName)
        {
            return new DataLayer().UpdateMemberhipUserName(applicationName, userName, newUserName);
        }

        #endregion

        #region Contact Information
        public static List<Country> ListCountry(int? countryId)
        {
            return new DataLayer().ListCountry(countryId);
        }

        public static List<ContactInformation> ListContactInformation(int? contactInfoId)
        {            
            return new DataLayer().ListContactInformation(contactInfoId);
        }

        public static void SaveContactInformation(List<ContactInformation> saveList)
        {
            if (saveList != null)
            {
                foreach (ContactInformation item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Customer
        public static List<Customer> ListCustomer(int? customerId, string name, bool includeLegacy)
        {
            DataLayer dl = new DataLayer();
            List<Customer> result = dl.ListCustomer(customerId, name, includeLegacy);
            if (result != null)
            {
                foreach (Customer cus in result)
                {
                    cus.ContactInformation = dl.ListContactInformation(cus.ContactInformationId).FirstOrDefault();
                    
                }
            }
            return result;
        }

        public static void SaveCustomer(List<Customer> saveList)
        {
            if (saveList != null)
            {
                foreach (Customer item in saveList)
                {
                    if (item.IsDeleted && item.NullableRecordId != null)
                    {
                        if (item.ContactInformation != null)
                        {
                            DeleteRecord((Record)item.ContactInformation);
                        }
                        DeleteRecord((Record)item);
                    }
                    else if (item.IsChanged || (item.ContactInformation != null && item.ContactInformation.IsChanged))
                    {
                        if (item.ContactInformation != null && item.ContactInformation.IsChanged)
                        {
                            SaveRecord((Record)item.ContactInformation);
                            item.ContactInformationId = item.ContactInformation.ContactInformationId;
                        }
                        SaveRecord((Record)item);
                    }
                }
            }
        }
        #endregion

        #region Encrypt/Decrypt        
        /// <summary>
        /// Encrypt the given string using the specified key.
        /// </summary>
        /// <param name="strToEncrypt">The string to be encrypted.</param>
        /// <param name="strKey">The encryption key.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string strToEncrypt, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        /// <summary>
        /// Decrypt the given string using the specified key.
        /// </summary>
        /// <param name="strEncrypted">The string to be decrypted.</param>
        /// <param name="strKey">The decryption key.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string strEncrypted, string strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                string strTempKey = strKey;
                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(strEncrypted);
                string strDecrypted = ASCIIEncoding.ASCII.GetString(
                    objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        #endregion
    }
}
