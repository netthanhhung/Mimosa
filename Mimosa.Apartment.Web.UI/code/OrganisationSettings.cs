using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mimosa.Apartment.Common;
using Mimosa.Apartment.Business;

namespace Mimosa.Apartment.Web.UI.code
{
    public class OrganisationSettings
    {
        #region Member Variables

        private const string _cookieName = "ezyOrgCookie";
        private const string _cookieIdField = "orgid";
        private const string _cookieDisplayField = "orgdisplay";
        private const string _portalAdminDisplay = "Portal Admin";
        private static readonly string _portalAdminOrgCode = MimosaSettings.PortalAdminOrganisationCode();

        private HttpCookie _cookie = GetCookie(_cookieName);
        private string _organisationId;
        private string _organisationName;
        private bool _isPortalAdmin; //= false

        #endregion

        #region Public Properties

        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        public bool IsValid
        {
            get { return (null != _cookie); }
        }

        public string OrganisationId
        {
            get { return _organisationId; }
            set { _organisationId = value; }
        }

        public bool IsPortalAdmin
        {
            get { return _isPortalAdmin; }
            set { _isPortalAdmin = value; }
        }

        #endregion

        #region Constructors

        public OrganisationSettings()
        {
            this._isPortalAdmin = IsValid && (_cookie[_cookieIdField] == _portalAdminDisplay);
            this._organisationId = IsValid ? _cookie[_cookieIdField] : string.Empty;
            this._organisationName = IsValid ? _cookie[_cookieDisplayField] : string.Empty;
        }

        public OrganisationSettings(bool isPortalAdmin, string organisationId, string organisationName)
        {
            this._isPortalAdmin = isPortalAdmin;
            this._organisationId = organisationId;
            this._organisationName = organisationName;
        }

        #endregion

        #region Private Methods

        protected void Save()
        {
            // Create the cookie, set internal values, expiry and then save.
            HttpCookie cookie = new HttpCookie(_cookieName);
            cookie[_cookieIdField] = this._organisationId;
            cookie[_cookieDisplayField] = this._organisationName;
            cookie.Expires = DateTime.MaxValue;

            _cookie = cookie;
            SetCookie(cookie);
        }

        #region Cookie Get and Set
        // If any other area needs to use cookie functionality, these will be moved to a CookieHelper.cs

        private static HttpCookie GetCookie(string cookieName)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            return cookies[cookieName];
        }

        private static void SetCookie(HttpCookie cookie)
        {
            HttpCookieCollection cookies = HttpContext.Current.Response.Cookies;
            cookies.Remove(cookie.Name); // Doesn't delete from the client, but replacing it so it is unimportant.
            cookies.Add(cookie);
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates if the authorisation code as passed is the code for any organisation in the db
        /// or is the special non-organisation high level portal admin password as defined in web.config
        /// </summary>
        /// <param name="authorisationCode">User entered authorisation code.</param>
        /// <returns>An OrganisationSettings with IsValid=true where authcode/password valid.
        /// Else an OrganisationSettings with IsValid=false.
        /// </returns>
        public static OrganisationSettings Validate(string authorisationCode)
        {
            OrganisationSettings result = null;

            // Portal Admins gain access to the login controls by entering config code in org authorisation code.
            // All other users need to have an organisation cookie set before logging in.
            if (authorisationCode == _portalAdminOrgCode)
            {
                result = new OrganisationSettings(true, _portalAdminDisplay, _portalAdminDisplay);
                result.Save();
            }
            else
            {
                Organisation org = ApartmentMethods.GetOrganisation(authorisationCode);
                if (null != org)
                {
                    result = new OrganisationSettings(false, org.NullableRecordId.ToString(), org.Name);
                    result.Save();
                }
            }

            return result;
        }

        #endregion
    }
}
