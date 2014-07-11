using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using System.Security.Cryptography;
using nsTooltips = Silverlight.Controls.ToolTips;
using Mimosa.Apartment.Common;

namespace Mimosa.Apartment.Silverlight.UI
{
    public partial class UserAccount : UserControl
    {        
        /*  ======================================================================            
         *      EVENTS AND DELEGATES
         *  ====================================================================== */
        internal event EventHandler SaveUserAccountComplete;

        #region Properties
        //private const string DisplayPassword = "display";
        private const string OnlineImage = "/Mimosa.Apartment.Silverlight.UI;component/Assets/Images/online.png";
        private const string OfflineImage = "/Mimosa.Apartment.Silverlight.UI;component/Assets/Images/offline.png";
        private const string TransparentImage = "/Mimosa.Apartment.Silverlight.UI;component/Assets/Images/Transparent.png";

        private int _currentOrgId;
        private List<SiteGroup> _siteGroups = new List<SiteGroup>();
        public List<SiteGroup> SiteGroups
        {
            get { return _siteGroups; }
            set { _siteGroups = value; }
        }

        private List<AspUser> _aspUsers = new List<AspUser>();        

        public Brush BackgroundColour {
            get { return panelUserAccount.Background; }
            set { panelUserAccount.Background = value; } 
        }
        public bool IsEditable { get; set; }
        public AspUser SavedAspUser { get; set; }
        
        #endregion
        
        #region Constructors
        public UserAccount()
        {
            InitializeComponent();
            FillLanguage();

            uiUsers.IsFilteringEnabled = true;
            uiUsers.KeyUp += new KeyEventHandler(uiUsers_KeyUp);
            uiUsers.OpenDropDownOnFocus = true;
            uiUsers.TextSearchMode = TextSearchMode.Contains;
            uiUsers.LostFocus += new RoutedEventHandler(uiUsers_LostFocus);

            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnUnlock.Click += new RoutedEventHandler(btnUnlock_Click);

            chkChangePasswordQuestionAnswer.Checked += new RoutedEventHandler(chkChangePasswordQuestionAnswer_Checked);
            chkChangePasswordQuestionAnswer.Unchecked += new RoutedEventHandler(chkChangePasswordQuestionAnswer_Checked);
        }

        void FillLanguage()
        {
            btnUnlock.Content = ResourceHelper.GetReourceValue("UserAccount_btnUnlock");
            btnSave.Content = ResourceHelper.GetReourceValue("Common_btnSave");
            chkAccountApproved.Content = ResourceHelper.GetReourceValue("UserAccount_chkAccountApproved");
            chkChangePasswordQuestionAnswer.Content = ResourceHelper.GetReourceValue("UserAccount_chkChangePasswordQuestionAnswer");
            chkResetPassword.Content = ResourceHelper.GetReourceValue("UserAccount_chkResetPassword");
            lblEmail.Text = ResourceHelper.GetReourceValue("UserAccount_lblEmail");
            lblPassword.Text = ResourceHelper.GetReourceValue("UserAccount_lblPassword");
            lblUsername.Text = ResourceHelper.GetReourceValue("UserAccount_lblUsername");
            lblInputPassword.Text = ResourceHelper.GetReourceValue("UserAccount_lblInputPassword");
            txtPasswordAnswerLabel.Text = ResourceHelper.GetReourceValue("UserAccount_txtPasswordAnswerLabel");
            txtPasswordQuestionLabel.Text = ResourceHelper.GetReourceValue("UserAccount_txtPasswordQuestionLabel");
        }

        void chkChangePasswordQuestionAnswer_Checked(object sender, RoutedEventArgs e)
        {
            txtPasswordQuestion.Visibility = txtPasswordQuestionLabel.Visibility
                = txtPasswordAnswer.Visibility = txtPasswordAnswerLabel.Visibility                
                = chkChangePasswordQuestionAnswer.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            lblInputPassword.Visibility = txtInputPassword.Visibility = System.Windows.Visibility.Collapsed;
            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    lblInputPassword.Visibility = txtInputPassword.Visibility
                        = chkChangePasswordQuestionAnswer.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public void ResetControlStatus()
        {
            uiImageOnline.Visibility = chkResetPassword.Visibility = txtResetPasswordInfo.Visibility = System.Windows.Visibility.Collapsed;
            btnSave.IsEnabled = this.IsEditable;
            btnUnlock.IsEnabled = false;
            txtPassword.Password = string.Empty;
            txtPassword.IsEnabled = true;
            chkChangePasswordQuestionAnswer.IsChecked = false;
            //chkChangePasswordQuestionAnswer.Visibility = System.Windows.Visibility.Collapsed;
            txtPasswordQuestion.Text = ResourceHelper.GetReourceValue("UserAccount_DefaultPasswordQuestion");
            txtPasswordAnswer.Text = ResourceHelper.GetReourceValue("UserAccount_DefaultPasswordAnswer");
            txtInputPassword.Password = string.Empty;
            txtEmail.Text = string.Empty;
            chkChangePasswordQuestionAnswer_Checked(null, null);
            txtResetPasswordInfo.Text = string.Empty;
            ucInformation.InfoMessage = ResourceHelper.GetReourceValue("UserAccount_NewRecord");            
        }

        public void RebindData(int orgId)
        {
            Globals.IsBusy = true;
            _currentOrgId = orgId;
            ResetControlStatus();
            ucInformation.InfoMessage = ResourceHelper.GetReourceValue("UserAccount_NewRecord");
            DataServiceHelper.ListOrgAdminAspUserAsync(orgId, SecurityHelper.OrganisationAdministratorRoleId, ListAllAspUserCompleted);
        }

        public void RebindData()
        {
            Globals.IsBusy = true;
            _currentOrgId = Globals.UserLogin.UserOrganisationId;
            ResetControlStatus();
            ucInformation.InfoMessage = ResourceHelper.GetReourceValue("UserAccount_NewRecord");
            DataServiceHelper.ListSiteGroupAsync(Globals.UserLogin.UserOrganisationId, null, ListSiteGroupCompleted);
        }

        void ListSiteGroupCompleted(List<SiteGroup> siteGroupList)
        {
            _siteGroups = siteGroupList;
            if (Globals.UserLogin.AspUser.OrganisationId.HasValue)
            {
                DataServiceHelper.ListAspUserAsync(Globals.UserLogin.AspUser.OrganisationId.Value, null, false, ListAllAspUserCompleted);
            }
            else
            {
                DataServiceHelper.ListAspUserAsync(null, null, false, ListAllAspUserCompleted);
            }
            //MembershipServiceHelper.ListUserRoleAuthAsync(null, SecurityHelper.SalesPersonRoleId, ListSalePersonsCompleted);
        }

        void ListAllAspUserCompleted(List<AspUser> userList)
        {
            _aspUsers = userList;
            FillUserNameCombobox();
            Globals.IsBusy = false;
        }

        //void FillUserNameCombobox()
        //{
        //    Dictionary<Guid, string> userItemSource = new Dictionary<Guid, string>();
        //    foreach (AspUser user in _aspUsers)
        //    {
        //        if (!userItemSource.ContainsKey(user.UserId))
        //        {
        //            userItemSource.Add(user.UserId, user.UserName);
        //        }
        //    }
        //    uiUsers.ItemsSource = userItemSource;
        //    if(userItemSource.Count > 0)
        //        uiUsers.SelectedIndex = 0;
        //    RebindUserAccountData();
        //}

        void FillUserNameCombobox()
        {
            //if this user is Security Admin : user can create new, edit the user accounts.
            Dictionary<Guid, string> userItemSource = new Dictionary<Guid, string>();
            if (Globals.UserLogin.IsUserPortalAdministrator)
            {
                foreach (AspUser user in _aspUsers)
                {
                    if (!userItemSource.ContainsKey(user.UserId))
                    {
                        userItemSource.Add(user.UserId, user.UserName);
                    }
                }
                uiUsers.ItemsSource = userItemSource;
                uiUsers.SelectedIndex = 0;
                RebindUserAccountData();
            }
            else if (Globals.UserLogin.IsUserSecurityAdministrator)
            {
                List<AspUser> filterUsers = SecurityHelper.FilterUserList(_aspUsers, _siteGroups);
                foreach (AspUser user in filterUsers)
                {
                    if (!userItemSource.ContainsKey(user.UserId))
                    {
                        userItemSource.Add(user.UserId, user.UserName);
                    }
                }
                
                userItemSource.OrderBy(i => i.Value);
                uiUsers.ItemsSource = userItemSource;

            }
            else
            {
                userItemSource.Add(Globals.UserLogin.UserUserId, Globals.UserLogin.UserName);
                uiUsers.ItemsSource = userItemSource;
                uiUsers.SelectedIndex = 0;
                
                RebindUserAccountData();
                uiUsers.IsEnabled = false;                
                chkAccountApproved.IsEnabled = false;
                btnUnlock.IsEnabled = false;
            }

        }


        void uiUsers_KeyUp(object sender, KeyEventArgs e)
        {
            Key keyCode = e.Key;
            if (keyCode == Key.Tab || keyCode == Key.Enter)
            {
                if (keyCode == Key.Enter)
                {
                    btnSave.Focus();
                }
                //RebindUserRoleAuthList();

            }
        }

        void uiUsers_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(uiUsers.Text))
                RebindUserAccountData();
        }

        public void RebindUserAccountData()
        {
            bool exist = false;
            txtInputPassword.Password = string.Empty;
            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    exist = true;
                    uiImageOnline.Visibility = chkResetPassword.Visibility = System.Windows.Visibility.Visible;
                    txtPassword.Password = aspUser.Password;
                    txtPassword.IsEnabled = false;
                    txtEmail.Text = string.Empty;

                    if (!string.IsNullOrEmpty(aspUser.Email))
                    {
                        txtEmail.Text = aspUser.Email;
                    }
                    //uiEmployees.IsEnabled = false;
                    chkAccountApproved.IsChecked = aspUser.IsApproved;
                    chkResetPassword.IsChecked = false;
                    txtResetPasswordInfo.Text = string.Empty;
                    if (aspUser.IsLockedOut)
                    {
                        chkResetPassword.IsEnabled = false;
                        chkAccountApproved.IsEnabled = false;
                        btnUnlock.IsEnabled = this.IsEditable;
                        ucInformation.InfoMessage = string.Format(ResourceHelper.GetReourceValue("UserAccount_LockedOutMessage"), aspUser.LastLockoutDate);
                    }
                    else
                    {
                        btnUnlock.IsEnabled = false;
                        chkAccountApproved.IsEnabled = true;
                        chkResetPassword.IsEnabled = true;
                        ucInformation.InfoMessage = string.Format(ResourceHelper.GetReourceValue("UserAccount_CreatedInfo"), aspUser.CreationDate.ToString(), aspUser.LastActivityDate.ToString());
                    }
                    if(!string.IsNullOrEmpty(aspUser.PasswordQuestion))
                        txtPasswordQuestion.Text = aspUser.PasswordQuestion;
                    if (!string.IsNullOrEmpty(aspUser.PasswordAnswer))
                    txtPasswordAnswer.Text = aspUser.PasswordAnswer;

                    chkChangePasswordQuestionAnswer.IsChecked = false;

                    if (aspUser.IsOnline)  /*This displays after adding new Admins incorrectly, as the UserID reset triggers this method, ie after saving a new siteAdmin as Org admin.*/
                    {                   /*Dont know where else it is used though, so shall leave for now*/
                        uiImageOnline.Source = new BitmapImage(new Uri(OnlineImage, UriKind.Relative));
                        nsTooltips.ToolTip tooltip = new nsTooltips.ToolTip()
                        {
                            DisplayTime = new Duration(TimeSpan.FromSeconds(10)),
                            InitialDelay = new Duration(TimeSpan.FromMilliseconds(0)),
                            Content = ResourceHelper.GetReourceValue("UserAccount_OnlineTooltip")
                        };
                        nsTooltips.ToolTipService.SetToolTip(uiImageOnline, tooltip);
                    }
                    else
                    {
                        uiImageOnline.Source = new BitmapImage(new Uri(OfflineImage, UriKind.Relative));
                        nsTooltips.ToolTip tooltip = new nsTooltips.ToolTip()
                        {
                            DisplayTime = new Duration(TimeSpan.FromSeconds(10)),
                            InitialDelay = new Duration(TimeSpan.FromMilliseconds(0)),
                            Content = ResourceHelper.GetReourceValue("UserAccount_OfflineTooltip")
                        };
                        nsTooltips.ToolTipService.SetToolTip(uiImageOnline, tooltip);
                    }
                }
            }

            if (!exist)
            {
                ResetControlStatus();                
            }
        }

        void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    Globals.IsBusy = true;
                    DataServiceHelper.UnlockAspUserAsync(aspUser, UnlockAspUserCompleted);
                }
            }
        }

        void UnlockAspUserCompleted(AspUser aspUser)
        {
            for (int i = 0; i < _aspUsers.Count; i++)
            {
                if (_aspUsers[i].UserId == aspUser.UserId)
                {
                    _aspUsers[i] = aspUser;
                    RebindUserAccountData();
                    MessageBox.Show(ResourceHelper.GetReourceValue("UserAccount_UserUnlocked"));
                    break;
                }
            }
            Globals.IsBusy = true;
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkChangePasswordQuestionAnswer.IsChecked == true
                 && (string.IsNullOrEmpty(txtPasswordQuestion.Text) || string.IsNullOrEmpty(txtPasswordAnswer.Text)))
            {
                MessageBox.Show(ResourceHelper.GetReourceValue("UserAccount_QuestionPasswordEmpty"), ResourceHelper.GetReourceValue("Common_ValidationError"), MessageBoxButton.OK);
                return;
            }

            if (uiUsers.SelectedValue != null && (Guid)uiUsers.SelectedValue != Guid.Empty)//Means update user
            {
                Guid userId = (Guid)uiUsers.SelectedValue;
                AspUser aspUser = _aspUsers.FirstOrDefault(i => i.UserId == userId);
                if (aspUser != null)
                {
                    Globals.IsBusy = true;
                    aspUser = GetSaveAspUser(aspUser);
                    DataServiceHelper.SaveAspUserAsync(aspUser, SaveAspUserCompleted);
                }
                if (chkChangePasswordQuestionAnswer.IsChecked == true
                    && string.IsNullOrEmpty(txtInputPassword.Password))
                {
                    MessageBox.Show(ResourceHelper.GetReourceValue("UserAccount_.InputPasswordEmpty"), ResourceHelper.GetReourceValue("Common_ValidationError"), MessageBoxButton.OK);
                    return;
                }
            }
            else//means create new user
            {
                if (string.IsNullOrEmpty(uiUsers.Text) || string.IsNullOrEmpty(txtPassword.Password))
                {
                    MessageBox.Show(ResourceHelper.GetReourceValue("UserAccount_.UserPasswordEmpty"), ResourceHelper.GetReourceValue("Common_ValidationError"), MessageBoxButton.OK);
                    return;
                }
                AspUser newUser = new AspUser();
                newUser.OrganisationId = Globals.UserLogin.UserOrganisationId;
                newUser.UserName = uiUsers.Text;
                newUser.Password = txtPassword.Password;
                newUser = GetSaveAspUser(newUser);
                if (_currentOrgId > 0)
                {
                    newUser.OrganisationId = _currentOrgId;
                }
                if (string.IsNullOrEmpty(newUser.PasswordQuestion))
                    newUser.PasswordQuestion = ResourceHelper.GetReourceValue("UserAccount_DefaultPasswordQuestion");
                if (string.IsNullOrEmpty(newUser.PasswordAnswer))
                    newUser.PasswordAnswer = ResourceHelper.GetReourceValue("UserAccount_DefaultPasswordAnswer");
                Globals.IsBusy = true;
                DataServiceHelper.SaveAspUserAsync(newUser, CreateAspUserCompleted);
            }
        }

        private AspUser GetSaveAspUser(AspUser aspUser)
        {
            aspUser.IsResetPassword = chkResetPassword.IsChecked == true;
            aspUser.IsApproved = chkAccountApproved.IsChecked == true;
            aspUser.Email = txtEmail.Text;
            if (chkChangePasswordQuestionAnswer.IsChecked == true)
            {
                aspUser.PasswordQuestion = txtPasswordQuestion.Text;
                aspUser.PasswordAnswer = txtPasswordAnswer.Text;
                if (string.IsNullOrEmpty(aspUser.PasswordQuestion))
                    aspUser.PasswordQuestion = ResourceHelper.GetReourceValue("UserAccount_DefaultPasswordQuestion");
                if (string.IsNullOrEmpty(aspUser.PasswordAnswer))
                    aspUser.PasswordAnswer = ResourceHelper.GetReourceValue("UserAccount_DefaultPasswordAnswer");
                aspUser.InputPassword = txtInputPassword.Password;
            }
            else
            {
                aspUser.PasswordQuestion = string.Empty;
                aspUser.PasswordAnswer = string.Empty;
            }
            
            aspUser.ErrorMessage = string.Empty;
            return aspUser;
        }

        void SaveAspUserCompleted(AspUser aspUser)
        {
            for (int i = 0; i < _aspUsers.Count; i++)
            {
                if (_aspUsers[i].UserId == aspUser.UserId)
                {
                    if (aspUser.IsSavedQAError)
                    {
                        MessageBox.Show(ResourceHelper.GetReourceValue("UserAccount_InputPasswordIncorrect"));
                    }
                    else if (!string.IsNullOrEmpty(aspUser.ErrorMessage))
                    {
                        MessageBox.Show(aspUser.ErrorMessage);
                    }
                    else
                    {
                        _aspUsers[i] = aspUser;
                        SavedAspUser = aspUser;
                        if (SaveUserAccountComplete != null)
                        {
                            SaveUserAccountComplete(this, null);
                        }
                        RebindUserAccountData();
                        if (!string.IsNullOrEmpty(aspUser.NewGenPassword))
                        {
                            txtResetPasswordInfo.Text = string.Format(ResourceHelper.GetReourceValue("UserAccount_NewGenPassword"), aspUser.NewGenPassword);
                            txtResetPasswordInfo.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            txtResetPasswordInfo.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        
                        MessageBox.Show(Globals.UserMessages.RecordsSaved);
                    }

                    break;
                }
            }
            Globals.IsBusy = false;
        }

        void CreateAspUserCompleted(AspUser aspUser)
        {
            //if (!string.IsNullOrEmpty(aspUser.ErrorMessage))
            //{
            //    Globals.IsBusy = false;
            //    MessageBox.Show(aspUser.ErrorMessage);
            //    return;
            //}
            //_aspUsers.Add(aspUser);
            //SavedAspUser = aspUser;
            //if (SaveUserAccountComplete != null)
            //{
            //    SaveUserAccountComplete(this, null);
            //}
            //Dictionary<Guid, string> userItemSource = uiUsers.ItemsSource as Dictionary<Guid, string>;
            //userItemSource.Add(aspUser.UserId, aspUser.UserName);
            //userItemSource.OrderBy(i => i.Value);
            //uiUsers.ItemsSource = null;
            //uiUsers.ItemsSource = userItemSource;
            //uiUsers.SelectedValue = aspUser.UserId;
            //RebindUserAccountData();
            //Globals.IsBusy = false;
            //MessageBox.Show(Globals.UserMessages.RecordsSaved);

            if (!string.IsNullOrEmpty(aspUser.ErrorMessage))
            {
                Globals.IsBusy = false;
                MessageBox.Show(aspUser.ErrorMessage);
                return;
            }
            _aspUsers.Add(aspUser);
            SavedAspUser = aspUser;
            if (SaveUserAccountComplete != null)
            {
                SaveUserAccountComplete(this, null);
            }
            Dictionary<Guid, string> userItemSource = uiUsers.ItemsSource as Dictionary<Guid, string>;
            userItemSource.Add(aspUser.UserId, aspUser.UserName);
            userItemSource.OrderBy(i => i.Value);
            uiUsers.ItemsSource = null;
            uiUsers.ItemsSource = userItemSource;
            uiUsers.SelectedValue = aspUser.UserId;
            RebindUserAccountData();
            if (Globals.UserLogin.IsUserPortalAdministrator)
            {
                UserRoleAuth uraOrgAdmin = new UserRoleAuth();
                uraOrgAdmin.RoleId = SecurityHelper.OrganisationAdministratorRoleId;
                uraOrgAdmin.WholeOrg = true;
                uraOrgAdmin.UserId = aspUser.UserId;
                uraOrgAdmin.IsChanged = true;
                uraOrgAdmin.CreatedBy = uraOrgAdmin.UpdatedBy = Globals.UserLogin.UserName;

                UserRoleAuth uraSecurityAdmin = new UserRoleAuth();
                uraSecurityAdmin.RoleId = SecurityHelper.SecurityAdminRoleId;
                uraSecurityAdmin.WholeOrg = true;
                uraSecurityAdmin.UserId = aspUser.UserId;
                uraSecurityAdmin.IsChanged = true;
                uraSecurityAdmin.CreatedBy = uraSecurityAdmin.UpdatedBy = Globals.UserLogin.UserName;

                List<UserRoleAuth> saveList = new List<UserRoleAuth>();
                saveList.Add(uraOrgAdmin);
                saveList.Add(uraSecurityAdmin);
                DataServiceHelper.SaveUserRoleAuthAsync(saveList, SaveUserRoleAuthCompleted);
            }
            else
            {
                Globals.IsBusy = false;
                MessageBox.Show(Globals.UserMessages.RecordsSaved);
            }

        }

        void SaveUserRoleAuthCompleted()
        {
            Globals.IsBusy = false;
            MessageBox.Show(Globals.UserMessages.RecordsSaved);
        }
        #endregion

    }
}
