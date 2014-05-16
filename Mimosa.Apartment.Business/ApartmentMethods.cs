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

        #region Sites
        public static List<Site> ListSite(int? orgId, int? siteId, bool showLegacy, bool loadContact)
        {
            DataLayer dataLayer = new DataLayer();
            List<Site> result = dataLayer.ListSite(orgId, siteId, showLegacy);
            if (loadContact)
            {
                foreach (Site site in result)
                {
                    if (site.ContactInformationID > 0)
                    {
                        List<ContactInformation> contacts = dataLayer.ListContactInformation(site.ContactInformationID);
                        if (contacts.Count > 0)
                        {
                            site.ContactInformation = contacts[0];
                        }
                    }
                }
            }
            return result;
        }

        public static void SaveSite(List<Site> saveList)
        {
            if (saveList != null)
            {
                foreach (Site item in saveList)
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
                                && item.ContactInformationID != Convert.ToInt32(item.ContactInformation.RecordId.Value))
                            {
                                item.ContactInformationID = Convert.ToInt32(item.ContactInformation.RecordId.Value);
                            }
                        }
                        SaveRecord(item);
                    }
                }
            }
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

        #region Security
        public static List<Component> ListComponent(int? componentId)
        {
            return new DataLayer().ListComponent(componentId);
        }

        public static List<RoleComponentPermission> ListRoleComponentPermission(Guid? roleId, int? componentId)
        {
            return new DataLayer().ListRoleComponentPermission(roleId, componentId);
        }

        public static List<RoleComponentPermission> ListRoleComponentPermissionByUser(Guid? userId)
        {
            return new DataLayer().ListRoleComponentPermissionByUser(userId);
        }

        public static void SaveRoleComponentPermission(List<RoleComponentPermission> saveList)
        {
            if (saveList != null)
            {
                foreach (RoleComponentPermission item in saveList)
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


        public static List<AspRole> ListAspRole(Guid? roleId)
        {
            return new DataLayer().ListAspRole(roleId);
        }

        public static void SaveAspRole(string applicationName, List<AspRole> saveList, string currentUser)
        {
            if (saveList != null)
            {
                DataLayer dataLayer = new DataLayer();
                foreach (AspRole item in saveList)
                {
                    if (item.IsDeleted && item.RoleId != Guid.Empty && item.CanDelete)
                    {
                        dataLayer.DeleteAspRole(item);
                    }
                    else if (item.IsChanged)
                    {
                        if (!string.IsNullOrEmpty(item.RoleName))
                            item.LoweredRoleName = item.RoleName.ToLower();
                        else
                            item.LoweredRoleName = item.RoleName = string.Empty;
                        dataLayer.SaveAspRole(applicationName, item, currentUser);
                    }
                }
            }
        }

        public static List<UserRoleAuth> ListUserRoleAuth(int? orgId, Guid? userId, Guid? roleId)
        {
            return new DataLayer().ListUserRoleAuth(orgId, userId, roleId);
        }

        public static void SaveUserRoleAuth(List<UserRoleAuth> saveList)
        {
            if (saveList != null)
            {
                foreach (UserRoleAuth item in saveList)
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
        public static List<Customer> ListCustomer(int? ordId, int? customerId, string firstName, string lastName, bool includeLegacy)
        {
            DataLayer dl = new DataLayer();
            List<Customer> result = dl.ListCustomer(ordId, customerId, firstName, lastName, includeLegacy);
            if (result != null)
            {
                foreach (Customer cus in result)
                {
                    cus.ContactInformation = dl.ListContactInformation(cus.ContactInformationId).FirstOrDefault();
                    
                }
            }
            return result;
        }

        public static List<Customer> SaveCustomer(List<Customer> saveList)
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
            return saveList;
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

        #region Tag Version
        public static TagVersion GetLatestTagVersion()
        {
            return new DataLayer().GetLatestTagVersion();
        }
        #endregion

        #region Equipment
        public static List<Equipment> ListEquipment(int? organisationId, int? equipmentId, bool showLegacy)
        {
            return new DataLayer().ListEquipment(organisationId, equipmentId, showLegacy);
        }

        public static void SaveEquipment(List<Equipment> saveList)
        {
            if (saveList != null)
            {
                foreach (Equipment item in saveList)
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

        #region Service
        public static List<Service> ListService(int? organisationId, int? serviceId, bool showLegacy)
        {
            return new DataLayer().ListService(organisationId, serviceId, showLegacy);
        }

        public static void SaveService(List<Service> saveList)
        {
            if (saveList != null)
            {
                foreach (Service item in saveList)
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

        #region RoomType
        public static List<RoomType> ListRoomType(int? organisationId, int? siteId, bool showLegacy)
        {
            return new DataLayer().ListRoomType(organisationId, siteId, showLegacy);
        }

        public static void SaveRoomType(List<RoomType> saveList)
        {
            if (saveList != null)
            {
                foreach (RoomType item in saveList)
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

        #region Room
        public static List<Room> ListRoom(int orgId, int? siteId, int? roomId, string roomName, string roomStatusIds, string roomTypeIds, int? floor , bool showLegacy)
        {
            return new DataLayer().ListRoom(orgId, siteId, roomId, roomName, roomStatusIds, roomTypeIds, floor, showLegacy);
        }

        public static void SaveRoom(List<Room> saveList)
        {
            if (saveList != null)
            {
                foreach (Room item in saveList)
                {
                    if (item.IsChanged)
                    {
                        SaveRecord((Record)item);
                    }
                    //if (item.RoomEquipments != null && item.RoomEquipments.Count > 0)
                    //{
                    //    foreach (RoomEquipment equipment in item.RoomEquipments)
                    //    {
                    //        equipment.RoomId = roomId;
                    //        if (equipment.IsChanged)
                    //        {
                    //            DeleteRecord((Record)equipment);
                    //        }
                    //        else if (equipment.IsChanged)
                    //        {
                    //            SaveRecord((Record)equipment);
                    //        }
                    //    }
                    //}

                    //if (item.RoomServices != null && item.RoomServices.Count > 0)
                    //{
                    //    foreach (RoomService service in item.RoomServices)
                    //    {
                    //        service.RoomId = roomId;
                    //        if (service.IsChanged)
                    //        {
                    //            DeleteRecord((Record)service);
                    //        }
                    //        else if (service.IsChanged)
                    //        {
                    //            SaveRecord((Record)service);
                    //        }
                    //    }
                    //}
                }
            }
        }
        #endregion

        #region RoomEquipment
        public static List<RoomEquipment> ListRoomEquipment(int? roomEquipmentId, int? roomId)
        {
            return new DataLayer().ListRoomEquipment(roomEquipmentId, roomId);
        }

        public static void SaveRoomEquipment(List<RoomEquipment> saveList)
        {
            if (saveList != null)
            {
                foreach (RoomEquipment item in saveList)
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

        #region RoomService
        public static List<RoomService> ListRoomService(int? roomServiceId, int? roomId)
        {
            return new DataLayer().ListRoomService(roomServiceId, roomId);
        }

        public static void SaveRoomService(List<RoomService> saveList)
        {
            if (saveList != null)
            {
                foreach (RoomService item in saveList)
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

        #region Image
        public static List<Image> ListImage(int? imageId, int? itemId, int? imageTypeId, int loadType)
        {
            return new DataLayer().ListImage(imageId, itemId, imageTypeId, loadType);
        }

        public static void SaveImage(List<Image> saveList)
        {
            if (saveList != null)
            {
                foreach (Image item in saveList)
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

        #region Booking
        public static List<Booking> ListBooking(int orgId, int? siteId, int? roomId, string roomName, int? bookingId, string bookingStatusIds,
            string customerName, DateTime? bookDateStart, DateTime? bookDateEnd)
        {
            return new DataLayer().ListBooking(orgId, siteId, roomId, roomName, bookingId, bookingStatusIds,
                customerName, bookDateStart, bookDateEnd);
        }

        public static void SaveBooking(List<Booking> saveList)
        {
            if (saveList != null)
            {
                foreach (Booking item in saveList)
                {
                    if (item.IsChanged)
                    {                        
                        if (item.CustomerItem != null)
                        {
                            if (item.CustomerItem.ContactInformation != null)
                            {
                                SaveRecord((Record)item.CustomerItem.ContactInformation);
                                item.CustomerItem.ContactInformationId = item.CustomerItem.ContactInformation.ContactInformationId;
                                SaveRecord((Record)item.CustomerItem);
                                item.CustomerId = item.CustomerItem.CustomerId;
                            }
                        }
                        SaveRecord((Record)item);

                    }
                }
            }
        }
        #endregion

        #region BookingRoomEquipment
        public static List<BookingRoomEquipment> ListBookingRoomEquipment(int? bookingRoomEquipmentId, int? bookingId, int? roomEquipmentId)
        {
            return new DataLayer().ListBookingRoomEquipment(bookingRoomEquipmentId, bookingId, roomEquipmentId);
        }

        public static void SaveBookingRoomEquipment(List<BookingRoomEquipment> saveList)
        {
            if (saveList != null)
            {
                foreach (BookingRoomEquipment item in saveList)
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


        #region BookingRoomService
        public static List<BookingRoomService> ListBookingRoomService(int? bookingRoomServiceId, int? bookingId, int? roomServiceId)
        {
            return new DataLayer().ListBookingRoomService(bookingRoomServiceId, bookingId, roomServiceId);
        }

        public static void SaveBookingRoomService(List<BookingRoomService> saveList)
        {
            if (saveList != null)
            {
                foreach (BookingRoomService item in saveList)
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
    }
}

