using System;
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
        public GlobalVariable GetGlobalVariable()
        {
            GlobalVariable result = new GlobalVariable();

            result.AppSettings = GetAppSettings();
            result.UserLogin = GetUserLogin();
            result.ApplicationCurrentDateTime = DateTime.Now;

            return result;
        }

        [OperationContract]
        public AppSettings GetAppSettings()
        {
            AppSettings appSettings = new AppSettings();

            return appSettings;
        }

        [OperationContract]
        public UserLogin GetUserLogin()
        {
            MembershipUser loginUser = Membership.GetUser();
            UserLogin settings = new UserLogin();
            settings.UserName = loginUser.UserName;
            settings.UserUserId = Utilities.ToGuid(loginUser.ProviderUserKey);
            
            //settings.ActiveModules = Role.ListActiveModules();
            settings.AspUser = GetAspUser(settings.UserUserId);
            settings.UserCustomerId = 123; //TODO :

            return settings;
        }

        [OperationContract]
        public AspUser GetAspUser(Guid userId)
        {
            List<AspUser> result = ApartmentMethods.ListAspUser(null, userId, null);
            if (result != null && result.Count > 0)
                return result[0];
            return null;
        }

        [OperationContract]
        public List<AspUser> ListAspUser(int? orgId, Guid? userId, bool? isLegacy)
        {            
            return ApartmentMethods.ListAspUser(orgId, userId, isLegacy);
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
        public List<Country> ListCountry(int? countryId)
        {
            return ApartmentMethods.ListCountry(countryId);
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

        #region Customer
        [OperationContract]
        public List<Customer> ListCustomer(int? customerId, string name, bool includeLegacy)
        {
            return ApartmentMethods.ListCustomer(customerId, name, includeLegacy);
        }

        [OperationContract]
        public void SaveCustomer(List<Customer> saveList)
        {
            ApartmentMethods.SaveCustomer(saveList);
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
        public int? UserCustomerId { set; get; }

        [DataMember]
        public string UserName { set; get; }

        [DataMember]
        public bool IsUserCustomer { set; get; }
        
        [DataMember]
        public bool IsUserAdministrator { set; get; }

        [DataMember]
        public Guid UserUserId { get; set; }

        [DataMember]
        public string UserOrganisationWeekday { set; get; }

        [DataMember]
        public AspUser AspUser { get; set; }
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
    }

    [DataContract]
    public class GlobalVariable
    {
        [DataMember]
        public AppSettings AppSettings { set; get; }

        [DataMember]
        public UserLogin UserLogin { set; get; }

        [DataMember]
        public DateTime? ApplicationCurrentDateTime { set; get; }
    }
}
