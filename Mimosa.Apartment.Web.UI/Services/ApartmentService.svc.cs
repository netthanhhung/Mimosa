﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Security;
using System.Web;
using Mimosa.Apartment.Common;
using Mimosa.Apartment.Business;
using Mimosa.Apartment.Web.UI.code;
using System.Net.Mail;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Xml;

namespace Mimosa.Apartment.Web.UI
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApartmentService
    {
        #region Common

        [OperationContract]
        public string SelectSessionId()
        {
            MembershipUser user = Membership.GetUser();
            string result = (user == null ? string.Empty : HttpContext.Current.Session.SessionID);

            return result;
        }


        [OperationContract]
        public UserLogin GetUserLogin()
        {
            MembershipUser loginUser = Membership.GetUser();
            UserLogin settings = new UserLogin();
            if (loginUser != null)
            {
                settings.UserName = loginUser.UserName;
                settings.UserUserId = Utilities.ToGuid(loginUser.ProviderUserKey);
                //settings.UserSiteId = SiteMethods.GetSiteIdForEmployee(settings.UserUserId);
                //settings.UserLicenseKeys = General.ListLicenseKeyByUserId(settings.UserUserId);

                OrganisationSettings orgSetting = new OrganisationSettings();
                settings.UserOrganisationId = Convert.ToInt32(orgSetting.OrganisationId);

                settings.UserOrganisation = ApartmentMethods.GetOrganisation(settings.UserOrganisationId);
                //settings.UserSite = settings.UserSiteId.HasValue ? SiteMethods.GetSite(settings.UserSiteId.Value) : null;


                //settings.ActiveModules = Role.ListActiveModules();
                settings.AspUser = GetAspUser(settings.UserUserId);
                //settings.UserEmployeeId = EmployeeMethods.GetEmployeeId(settings.UserUserId);

                settings.RoleComponentPermissions = ApartmentMethods.ListRoleComponentPermissionByUser(settings.UserUserId);
                if (!settings.AspUser.OrganisationId.HasValue)
                {
                    settings.UserRoleAuths = ApartmentMethods.ListUserRoleAuth(null, settings.UserUserId, null);
                }
                else
                {
                    settings.UserRoleAuths = ApartmentMethods.ListUserRoleAuth(settings.UserOrganisationId, settings.UserUserId, null);
                }

                if (settings.UserSite == null && settings.UserRoleAuths != null && settings.UserRoleAuths.Count > 0
                    && settings.UserRoleAuths.Count(i => i.SiteId.HasValue) > 0)
                {
                    settings.UserSiteId = settings.UserRoleAuths.FirstOrDefault(i => i.SiteId.HasValue).SiteId.Value;
                    List<Site> sites = ApartmentMethods.ListSite(null, settings.UserSiteId.Value, true, false);
                    if (sites.Count > 0)
                    {
                        settings.UserSite = sites[0];
                    }
                }
            }
            return settings;
        }

        private bool HasRole(List<UserRoleAuth> userRoleAuths, Guid roleId)
        {
            return userRoleAuths != null && userRoleAuths.Count(i => i.RoleId.Equals(roleId)) > 0;
        }

        [OperationContract]
        public AppSettings GetAppSettings()
        {
            AppSettings appSettings = new AppSettings();
            appSettings.GloblaCulture = MimosaSettings.GloblaCulture();
            appSettings.NumberFormatCulture = MimosaSettings.NumberFormatCulture();
            appSettings.DateTimeFormatCulture = MimosaSettings.DateTimeFormatCulture();
            appSettings.MainCurrency = MimosaSettings.MainCurrency();
            appSettings.SecondCurrency = MimosaSettings.SecondCurrency();
            appSettings.VirtualDirectory = MimosaSettings.VirtualDirectory();
            decimal rate = ConvertCurrency(appSettings.SecondCurrency, appSettings.MainCurrency);
            appSettings.ExchangeRate = rate;
            return appSettings;
        }

        [OperationContract]
        public GlobalVariable GetGlobalVariable()
        {
            GlobalVariable result = new GlobalVariable();

            result.AppSettings = GetAppSettings();
            result.UserLogin = GetUserLogin();
            result.TagVersion = ApartmentMethods.GetLatestTagVersion();
            //result.Licenses = General.ListLicense(null);
            //result.UserLogin.UserModuleTypes = new List<ModuleTypes>();           


            return result;
        }

        [OperationContract]
        public int ChangePassword(string oldPassword, string newPassword)
        {
            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);

            try
            {
                if (user.ChangePassword(oldPassword, newPassword))
                    return 0; // Password changed
                else
                    return 1; // Password change failed. Please re-enter your values and try again
            }
            catch
            {
                return 3; // An exception occurred. Please re-enter your values and try again
            }
        }


        [OperationContract]
        public AspUser GetAspUser(Guid userId)
        {
            AspUser result = null;
            List<AspUser> list = ListAspUser(null, userId, null);
            if (list != null && list.Count > 0)
                result = list[0];
            return result;

        }

        [OperationContract]
        public bool DeleteAspUser(AspUser aspUser, long? employeeId)
        {
            bool userDeleted = Membership.DeleteUser(aspUser.UserName);
            return userDeleted;
        }


        [OperationContract]
        public AspUser UnlockAspUser(AspUser oldUser)
        {
            if (oldUser != null)
            {
                MembershipUser memberShipUser = Membership.GetUser(oldUser.UserId);
                memberShipUser.UnlockUser();
                oldUser = GetAspUser(oldUser.UserId);
            }
            return oldUser;
        }

        [OperationContract]
        public AspUser SaveAspUser(AspUser saveUser)
        {
            if (saveUser != null)
            {
                MembershipProvider simpleProvider = Membership.Providers["SimpleProvider"];

                if (saveUser.UserId == Guid.Empty) //means this is new user : create user
                {
                    // Insert New Membership Account
                    MembershipCreateStatus status;
                    MembershipUser newUser = Membership.CreateUser(saveUser.UserName, saveUser.Password, saveUser.Email,
                            saveUser.PasswordQuestion, saveUser.PasswordAnswer, saveUser.IsApproved, out status);

                    if (status == MembershipCreateStatus.Success)
                    {
                        Guid newUserId = Utilities.ToGuid(newUser.ProviderUserKey);
                        ApartmentMethods.UpdateAspUserOrganisationId(newUserId, saveUser.OrganisationId);
                        saveUser = GetAspUser(newUserId);
                    }
                    else
                    {
                        switch (status)
                        {
                            case MembershipCreateStatus.DuplicateEmail:
                                saveUser.ErrorMessage = "The e-mail address already exists in the database for the application."; break;
                            case MembershipCreateStatus.DuplicateProviderUserKey:
                                saveUser.ErrorMessage = "The provider user key already exists in the database for the application."; break;
                            case MembershipCreateStatus.DuplicateUserName:
                                saveUser.ErrorMessage = "The user name already exists in the database for the application."; break;
                            case MembershipCreateStatus.InvalidAnswer:
                                saveUser.ErrorMessage = "The password answer is not formatted correctly."; break;
                            case MembershipCreateStatus.InvalidEmail:
                                saveUser.ErrorMessage = "The e-mail address is not formatted correctly."; break;
                            case MembershipCreateStatus.InvalidProviderUserKey:
                                saveUser.ErrorMessage = "The provider user key is of an invalid type or format."; break;
                            case MembershipCreateStatus.InvalidQuestion:
                                saveUser.ErrorMessage = "The password question is not formatted correctly."; break;
                            case MembershipCreateStatus.InvalidUserName:
                                saveUser.ErrorMessage = "The user name was not found in the database."; break;
                            case MembershipCreateStatus.InvalidPassword:
                                saveUser.ErrorMessage = "The password is not formatted correctly."; break;
                            default:
                                saveUser.ErrorMessage = "Fail to create new user";
                                break;

                        }

                    }
                }
                else
                {
                    MembershipUser memberShipUser = Membership.GetUser(saveUser.UserId);
                    int? updateCode = null;
                    if (memberShipUser.UserName != saveUser.UserName)
                    {
                        updateCode = ApartmentMethods.UpdateMembershipUserName(Membership.ApplicationName, memberShipUser.UserName, saveUser.UserName);
                        memberShipUser = Membership.GetUser(saveUser.UserId);
                    }

                    string newGenPassword = string.Empty;
                    bool saveQAerror = false;
                    if (updateCode == null || updateCode == 0)
                    {
                        memberShipUser.Email = saveUser.Email;
                        memberShipUser.IsApproved = saveUser.IsApproved;
                        Membership.UpdateUser(memberShipUser);

                        if (!string.IsNullOrEmpty(saveUser.PasswordQuestion) && !string.IsNullOrEmpty(saveUser.PasswordAnswer))
                        {
                            saveQAerror = !memberShipUser.ChangePasswordQuestionAndAnswer(saveUser.InputPassword, saveUser.PasswordQuestion, saveUser.PasswordAnswer);
                        }

                        if (saveUser.IsResetPassword)
                        {
                            if (simpleProvider != null)
                            {
                                MembershipUser simpleUser = simpleProvider.GetUser(saveUser.UserId, false);
                                if (simpleUser != null)
                                {
                                    if (saveUser.IsResetPassword)
                                    {
                                        newGenPassword = simpleUser.ResetPassword();
                                    }
                                }
                            }
                        }
                    }
                    saveUser = GetAspUser(saveUser.UserId);
                    saveUser.NewGenPassword = newGenPassword;
                    saveUser.IsSavedQAError = saveQAerror;
                }
            }

            return saveUser;
        }

        private AspUser ConvertUser(MembershipUser membership)
        {
            AspUser user = new AspUser();
            if (membership == null)
                return null;
            user.UserId = Utilities.ToGuid(membership.ProviderUserKey);
            user.UserName = membership.UserName;
            user.IsApproved = membership.IsApproved;
            user.IsLockedOut = membership.IsLockedOut;
            user.IsOnline = membership.IsOnline;
            user.Comment = membership.Comment;
            user.CreationDate = membership.CreationDate;
            user.Email = membership.Email;
            user.LastActivityDate = membership.LastActivityDate;
            user.LastLockoutDate = membership.LastLockoutDate;
            user.LastLoginDate = membership.LastLoginDate;
            user.LastPasswordChangedDate = membership.LastPasswordChangedDate;
            user.PasswordQuestion = membership.PasswordQuestion;
            user.ProviderName = membership.ProviderName;
            user.ProviderUserKey = membership.ProviderUserKey;
            user.Password = "nothing";
            return user;
        }

        [OperationContract]
        public List<Country> ListCountry(int? countryId)
        {
            return ApartmentMethods.ListCountry(countryId);
        }

        [OperationContract]
        public List<City> ListCity(int? countryid, int? cityId)
        {
            return ApartmentMethods.ListCity(countryid, cityId);
        }

        [OperationContract]
        public List<District> ListDistrict(int? cityId, int? districtId)
        {
            return ApartmentMethods.ListDistrict(cityId, districtId);
        }
        #endregion

        #region Contact Information
        [OperationContract]
        public List<ContactInformation> ListContactInformation(int? contactInfoId)
        {
            return ApartmentMethods.ListContactInformation(contactInfoId);
        }

        [OperationContract]
        public void SaveContactInformation(List<ContactInformation> saveList)
        {
            ApartmentMethods.SaveContactInformation(saveList);
        }
        #endregion

        #region Organisation
        [OperationContract]
        public List<Organisation> ListOrganisation(Guid? roleId)
        {
            return ApartmentMethods.ListOrganisation(roleId);
        }

        [OperationContract]
        public List<AspUser> ListOrgAdminAspUser(int orgId, Guid roleId)
        {
            return ApartmentMethods.ListOrgAdminAspUser(orgId, roleId);
        }

        [OperationContract]
        public void SaveOrganisations(List<Organisation> saveList)
        {
            ApartmentMethods.SaveOrganisations(saveList);
        }

        [OperationContract]
        public Organisation GetOrganisation(int organisationId)
        {
            return ApartmentMethods.GetOrganisation(organisationId);
        }

        [OperationContract]
        public Organisation GetOrganisationByCode(string authorisationCode)
        {
            return ApartmentMethods.GetOrganisation(authorisationCode);
        }

        #endregion

        #region Sites
        [OperationContract]
        public List<Site> ListSite(int? orgId, int? siteId, bool showLegacy, bool loadContact)
        {
            return ApartmentMethods.ListSite(orgId, siteId, showLegacy, loadContact);
        }

        [OperationContract]
        public void SaveSite(List<Site> saveList)
        {
            ApartmentMethods.SaveSite(saveList);
        }
        #endregion

        #region Customer
        [OperationContract]
        public List<Customer> ListCustomer(int? orgId, int? customerId,  string firstName, string lastName, 
            int? siteId, bool hasContracts, DateTime? contractDateStart, DateTime? contractDateEnd, bool includeLegacy)
        {
            return ApartmentMethods.ListCustomer(orgId, customerId, firstName, lastName, siteId, hasContracts, contractDateStart, contractDateEnd, includeLegacy);
        }

        [OperationContract]
        public List<Customer> SaveCustomer(List<Customer> saveList)
        {
            return ApartmentMethods.SaveCustomer(saveList);
        }
        #endregion

        #region UserAccount
        [OperationContract]
        public List<AspUser> ListAspUser(int? orgId, Guid? userId, bool? isLegacy)
        {
            return ApartmentMethods.ListAspUser(orgId, userId, isLegacy);
        }

        [OperationContract]
        public List<string> ListUserName(string applicationName, int? orgId, string startsWith)
        {
            return ApartmentMethods.ListUserName(applicationName, orgId, startsWith);
        }

        [OperationContract]
        public int? UpdateMembershipUserName(string applicationName, string userName, string newUserName)
        {
            return ApartmentMethods.UpdateMembershipUserName(applicationName, userName, newUserName);
        }

        #endregion

        #region Security
        [OperationContract]
        public List<Component> ListComponent(int? componentId)
        {
            return ApartmentMethods.ListComponent(componentId);
        }
        [OperationContract]
        public List<RoleComponentPermission> ListRoleComponentPermission(Guid? roleId, int? componentId)
        {
            return ApartmentMethods.ListRoleComponentPermission(roleId, componentId);
        }

        [OperationContract]
        public List<RoleComponentPermission> ListRoleComponentPermissionByUser(Guid? userId)
        {
            return ApartmentMethods.ListRoleComponentPermissionByUser(userId);
        }

        [OperationContract]
        public void SaveRoleComponentPermission(List<RoleComponentPermission> saveList)
        {
            ApartmentMethods.SaveRoleComponentPermission(saveList);
        }
        [OperationContract]
        public List<AspRole> ListAspRole(Guid? roleId)
        {
            return ApartmentMethods.ListAspRole(roleId);
        }
        [OperationContract]
        public void SaveAspRole(List<AspRole> saveList, string currentUser)
        {
            ApartmentMethods.SaveAspRole(Membership.ApplicationName, saveList, currentUser);
        }
        [OperationContract]
        public List<UserRoleAuth> ListUserRoleAuth(int? orgId, Guid? userId, Guid? roleId)
        {
            return ApartmentMethods.ListUserRoleAuth(orgId, userId, roleId);
        }
        [OperationContract]
        public void SaveUserRoleAuth(List<UserRoleAuth> saveList)
        {
            ApartmentMethods.SaveUserRoleAuth(saveList);
        }
        #endregion

        #region Equipment
        [OperationContract]
        public List<Equipment> ListEquipment(int? organisationId, int? equipmentId, bool showLegacy)
        {
            return ApartmentMethods.ListEquipment(organisationId, equipmentId, showLegacy);
        }

        [OperationContract]
        public void SaveEquipment(List<Equipment> saveList)
        {
            ApartmentMethods.SaveEquipment(saveList);
        }
        #endregion

        #region Service
        [OperationContract]
        public List<Service> ListService(int? organisationId, int? serviceId, bool showLegacy)
        {
            return ApartmentMethods.ListService(organisationId, serviceId, showLegacy);
        }

        [OperationContract]
        public void SaveService(List<Service> saveList)
        {
            ApartmentMethods.SaveService(saveList);
        }
        #endregion

        #region RoomType
        [OperationContract]
        public List<RoomType> ListRoomType(int? organisationId, int? siteId, bool showLegacy)
        {
            return ApartmentMethods.ListRoomType(organisationId, siteId, showLegacy);
        }

        [OperationContract]
        public void SaveRoomType(List<RoomType> saveList)
        {
            ApartmentMethods.SaveRoomType(saveList);
        }
        #endregion

        #region Room
        [OperationContract]
        public List<Room> ListRoom(int orgId, int? siteId, int? roomId, string roomName, string roomStatusIds, string roomTypeIds, int? floor, bool showLegacy)
        {
            return ApartmentMethods.ListRoom(orgId, siteId, roomId, roomName, roomStatusIds, roomTypeIds, floor, showLegacy);
        }

        [OperationContract]
        public void SaveRoom(List<Room> saveList)
        {
            ApartmentMethods.SaveRoom(saveList);
        }
        #endregion

        #region RoomEquipment
        [OperationContract]
        public List<RoomEquipment> ListRoomEquipment(int? roomEquipmentId, int? roomId)
        {
            return ApartmentMethods.ListRoomEquipment(roomEquipmentId, roomId);
        }

        [OperationContract]
        public void SaveRoomEquipment(List<RoomEquipment> saveList)
        {
            ApartmentMethods.SaveRoomEquipment(saveList);
        }
        #endregion

        #region RoomService
        [OperationContract]
        public List<RoomService> ListRoomService(int? roomServiceId, int? roomId)
        {
            return ApartmentMethods.ListRoomService(roomServiceId, roomId);
        }

        [OperationContract]
        public void SaveRoomService(List<RoomService> saveList)
        {
            ApartmentMethods.SaveRoomService(saveList);
        }
        #endregion

        #region Image
        [OperationContract]
        public List<Image> ListImage(int? imageId, int? itemId, int? imageTypeId, int loadType)
        {
            return ApartmentMethods.ListImage(imageId, itemId, imageTypeId, loadType);
        }

        [OperationContract]
        public void SaveImage(List<Image> saveList)
        {
            ApartmentMethods.SaveImage(saveList);
        }
        #endregion

        #region Booking
        [OperationContract]
        public List<Booking> ListBooking(int orgId, int? siteId, int? roomId, string roomName, int? bookingId, string bookingStatusIds,
            int? customerId, string customerName, DateTime? bookDateStart, DateTime? bookDateEnd)
        {
            return ApartmentMethods.ListBooking(orgId, siteId, roomId, roomName, bookingId, bookingStatusIds,
                customerId, customerName, bookDateStart, bookDateEnd);
        }

        [OperationContract]
        public void SaveBooking(List<Booking> saveList)
        {
            ApartmentMethods.SaveBooking(saveList);
        }

        [OperationContract]
        public bool CheckExistBooking(int roomId, DateTime dateStart, DateTime dateEnd)
        {
            return ApartmentMethods.CheckExistBooking(roomId, dateStart, dateEnd);
        }
        #endregion

        #region BookingRoomEquipment
        [OperationContract]
        public List<BookingRoomEquipment> ListBookingRoomEquipment(int? bookingRoomEquipmentId, int? bookingId, int? roomEquipmentId)
        {
            return ApartmentMethods.ListBookingRoomEquipment(bookingRoomEquipmentId, bookingId, roomEquipmentId);
        }

        [OperationContract]
        public void SaveBookingRoomEquipment(List<BookingRoomEquipment> saveList)
        {
            ApartmentMethods.SaveBookingRoomEquipment(saveList);
        }
        #endregion

        #region BookingPayment
        [OperationContract]
        public List<BookingPayment> ListBookingPayment(int? orgId, int? siteId, int? roomId, int? bookingId, int? bookingPaymentId,
            DateTime dateStart, DateTime dateEnd, int payment)
        {
            return ApartmentMethods.ListBookingPayment(orgId, siteId, roomId, bookingId, bookingPaymentId, dateStart, dateEnd, payment);
        }

        [OperationContract]
        public List<BookingPayment> ListHistoryPayment(int? orgId, int? siteId, int? roomId, int? customerId,
            DateTime dateStart, DateTime dateEnd, int payment)
        {
            return ApartmentMethods.ListHistoryPayment(orgId, siteId, roomId, customerId, dateStart, dateEnd, payment);
        }

        [OperationContract]
        public void SaveBookingPayment(List<BookingPayment> saveList)
        {
            ApartmentMethods.SaveBookingPayment(saveList);
        }

        [OperationContract]
        public void SendBookingPaymentMail(List<BookingPayment> paymentList)
        {
            if (paymentList != null && paymentList.Count > 0)
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                foreach (BookingPayment paymentItem in paymentList)
                {
                    SendOnePaymentMail(smtpClient, paymentItem);
                }
            }
        }

        private void SendOnePaymentMail(SmtpClient smtpClient, BookingPayment paymentItem)
        {
            MailMessage message = new MailMessage();
            List<Customer> customers = ApartmentMethods.ListCustomer(null, paymentItem.CustomerId, null, null, null, false, null, null, true);
            string email = string.Empty;
            if (customers.Count > 0 && customers[0].ContactInformation != null && !string.IsNullOrEmpty(customers[0].ContactInformation.Email))
            {
                email = customers[0].ContactInformation.Email;
            }

            if (!string.IsNullOrEmpty(email))
            {
                string dateFormat = "dd-MMM-yyyy";
                string moneyFormat = "C0";
                message.To.Add(new MailAddress(email));
                string subject = string.Format("Payment Details from {0} to {1}", paymentItem.DateStart.ToString(dateFormat), paymentItem.DateEnd.ToString(dateFormat));
                message.Subject = subject;
                message.IsBodyHtml = true;

                using (FileStream stream = File.OpenRead(HttpContext.Current.Server.MapPath("/EmailTemplates/MonthlyPayment.htm")))
                {
                    StringBuilder strb = new StringBuilder();
                    byte[] b = new byte[stream.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (stream.Read(b, 0, b.Length) > 0)
                    {
                        strb.Append(temp.GetString(b));
                    }
                    string content = strb.ToString();

                    CultureInfo customCultureInfo = new CultureInfo(MimosaSettings.GloblaCulture());
                    customCultureInfo.NumberFormat = (new CultureInfo(MimosaSettings.NumberFormatCulture())).NumberFormat;
                    customCultureInfo.DateTimeFormat = (new CultureInfo(MimosaSettings.DateTimeFormatCulture())).DateTimeFormat; 

                    Thread.CurrentThread.CurrentCulture = customCultureInfo;

                    content = string.Format(content, paymentItem.CustomerName, subject, paymentItem.SiteName, paymentItem.RoomName, 
                        paymentItem.DateStart.ToString(dateFormat), paymentItem.DateEnd.ToString(dateFormat),
                        paymentItem.RoomPrice.ToString(moneyFormat),
                        paymentItem.EquipmentPrice.ToString(moneyFormat),
                        paymentItem.ServicePrice.ToString(moneyFormat),
                        paymentItem.TotalPrice.ToString(moneyFormat),
                        paymentItem.CustomerPaid.ToString(moneyFormat), 
                        paymentItem.MoneyLeft.ToString(moneyFormat));
                    message.Body = content;

                    smtpClient.Send(message);
                }                
            }

        }

        private string BuildBookingPaymentContent(BookingPayment paymentItem)
        {
            StringBuilder strBuilder = new StringBuilder();


            return strBuilder.ToString();
        }
        #endregion

        #region BookingRoomEquipmentDetail
        [OperationContract]
        public List<BookingRoomEquipmentDetail> ListBookingRoomEquipmentDetail(int? bookingRoomEquipmentDetailId, int? bookingRoomEquipmentId,
                int? bookingId, DateTime? dateStart, DateTime? dateEnd)
        {
            return ApartmentMethods.ListBookingRoomEquipmentDetail(bookingRoomEquipmentDetailId, bookingRoomEquipmentId, bookingId, dateStart, dateEnd);
        }

        [OperationContract]
        public void SaveBookingRoomEquipmentDetail(List<BookingRoomEquipmentDetail> saveList)
        {
            ApartmentMethods.SaveBookingRoomEquipmentDetail(saveList);
        }
        #endregion

        #region BookingRoomService
        [OperationContract]
        public List<BookingRoomService> ListBookingRoomService(int? bookingRoomServiceId, int? bookingId, int? roomServiceId)
        {
            return ApartmentMethods.ListBookingRoomService(bookingRoomServiceId, bookingId, roomServiceId);
        }

        [OperationContract]
        public void SaveBookingRoomService(List<BookingRoomService> saveList)
        {
            ApartmentMethods.SaveBookingRoomService(saveList);
        }
        #endregion

        #region BookingRoomServiceDetail
        [OperationContract]
        public List<BookingRoomServiceDetail> ListBookingRoomServiceDetail(int? bookingRoomServiceDetailId, int? bookingRoomServiceId,
                int? bookingId, DateTime? dateStart, DateTime? dateEnd)
        {
            return ApartmentMethods.ListBookingRoomServiceDetail(bookingRoomServiceDetailId, bookingRoomServiceId, bookingId, dateStart, dateEnd);
        }

        [OperationContract]
        public void SaveBookingRoomServiceDetail(List<BookingRoomServiceDetail> saveList)
        {
            ApartmentMethods.SaveBookingRoomServiceDetail(saveList);
        }
        #endregion

        #region SiteGroup
        [OperationContract]
        public List<SiteGroup> ListSiteGroup(int? orgId, int? siteGroupId)
        {
            return ApartmentMethods.ListSiteGroup(orgId, siteGroupId);
        }

        [OperationContract]
        public void SaveSiteGroups(List<SiteGroup> saveList)
        {
            ApartmentMethods.SaveSiteGroups(saveList);
        }

        [OperationContract]
        public List<Site> ListSiteBySiteGroup(int? siteGroupId, bool? showLegacy, bool loadContact)
        {
            return ApartmentMethods.ListSiteBySiteGroup(siteGroupId, showLegacy, loadContact);
        }

        [OperationContract]
        public List<SiteGroupSite> ListSiteGroupSite(int? siteGroupId, int? siteId)
        {
            return ApartmentMethods.ListSiteGroupSite(siteGroupId, siteId);
        }

        [OperationContract]
        public void SaveSiteGroupSites(List<SiteGroupSite> saveList)
        {
            ApartmentMethods.SaveSiteGroupSites(saveList);
        }
        #endregion

        #region ExchangeRate
        public decimal ConvertCurrency(string fromCurrency, string toCurrency)
        {
            string xmlResult = null;
            string url = string.Format("http://www.webservicex.net/CurrencyConvertor.asmx/ConversionRate?FromCurrency={0}&ToCurrency={1}", fromCurrency.ToUpper(), toCurrency.ToUpper());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader resStream = new StreamReader(response.GetResponseStream());
            XmlDocument doc = new XmlDocument();
            xmlResult = resStream.ReadToEnd();
            doc.LoadXml(xmlResult);
            string rateStr = doc.GetElementsByTagName("double").Item(0).InnerText;
            return Convert.ToDecimal(rateStr);
        }
        #endregion
    }

    /// <summary>
    /// Entity object class for storing the setting of TimeClock Application. It will consume by 
    /// the Silverlight pages
    /// </summary>
    [DataContract]
    public class UserLogin
    {
        [DataMember]
        public int UserOrganisationId { set; get; }

        [DataMember]
        public int? UserSiteId { set; get; }

        [DataMember]
        public int? UserEmployeeId { set; get; }

        [DataMember]
        public string UserName { set; get; }

        [DataMember]
        public bool IsUserOrganisationAdministrator { set; get; }
        [DataMember]
        public bool IsUserSiteAdministrator { set; get; }
        [DataMember]
        public bool IsUserPortalAdministrator { set; get; }
        [DataMember]
        public bool IsUserSecurityAdministrator { set; get; }


        [DataMember]
        public Guid UserUserId { get; set; }

        [DataMember]
        public Organisation UserOrganisation { set; get; }

        [DataMember]
        public Site UserSite { set; get; }

        [DataMember]
        public AspUser AspUser { get; set; }

        [DataMember]
        public List<RoleComponentPermission> RoleComponentPermissions { get; set; }

        [DataMember]
        public List<UserRoleAuth> UserRoleAuths { get; set; }

        [DataMember]
        public List<Guid> UserLicenseKeys { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AppSettings
    {
        [DataMember]
        public string ReportServerUrl { set; get; }

        [DataMember]
        public string ReportLocalization { set; get; }

        [DataMember]
        public string GloblaCulture { set; get; }

        [DataMember]
        public string NumberFormatCulture { set; get; }

        [DataMember]
        public string DateTimeFormatCulture { set; get; }

        [DataMember]
        public string MainCurrency { set; get; }

        [DataMember]
        public string SecondCurrency { set; get; }

        [DataMember]
        public string VirtualDirectory { set; get; }

        [DataMember]
        public decimal ExchangeRate { set; get; }
    }

    [DataContract]
    public class GlobalVariable
    {
        [DataMember]
        public AppSettings AppSettings { set; get; }

        [DataMember]
        public UserLogin UserLogin { set; get; }

        [DataMember]
        public TagVersion TagVersion { set; get; }
    }
}
