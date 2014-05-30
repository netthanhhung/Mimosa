using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using Mimosa.Apartment.Common;
using System.IO;
using common = Mimosa.Apartment.Common;
using Mimosa.Apartment.Silverlight.UI.ApartmentService;

namespace Mimosa.Apartment.Silverlight.UI
{
	public static class DataServiceHelper
	{
		/*  ======================================================================            
		 *      EVENTS AND DELEGATES
		 *  ====================================================================== */
		internal delegate void EmptyCallback();
		internal delegate void DataServiceCallback<T>(T returnValue);
		internal delegate void DataServiceIEnumerableCallback<T>(IEnumerable<T> itemsSource);
		internal delegate void DataServiceListCallback<T>(List<T> itemsSource);


		/*  ======================================================================            
		 *      PAGE MEMBERS
		 *  ====================================================================== */
		private static Dictionary<Guid, Delegate> _callbacks = new Dictionary<Guid, Delegate>();
		private static Dictionary<Guid, Object[]> _params = new Dictionary<Guid, Object[]>();

		/*  ======================================================================            
		 *      EVENT HANDLERS
		 *  ====================================================================== */
        private static ApartmentServiceClient GetProxy()
		{
			return GetProxy(Guid.Empty, null);
		}

        private static ApartmentServiceClient GetProxy(Guid callerKey, Delegate callback)
		{
            ApartmentServiceClient result = new ApartmentServiceClient();
			// Rely on the endpoint addresses being setup within ServiceReferences.ClientConfig instead.
			// result.Endpoint.Address = new EndpointAddress(Globals.EndPointAddresses.DataService);            
			if (callback != null)
			{
				_callbacks.Add(callerKey, callback);
			}

			return result;
		}

		
		/*  ======================================================================            
		 *      EVENT HANDLERS
		 *  ====================================================================== */
		static void proxy_VoidMethodCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			Guid callerKey = (Guid)e.UserState;
			if (_callbacks.ContainsKey(callerKey))
			{
				((EmptyCallback)_callbacks[callerKey]).Invoke();

				_callbacks.Remove(callerKey);
			}
		}

        /*  ======================================================================            
		 *      Methods
		 *  ====================================================================== */
        internal static void SelectSessionId()
        {
            ApartmentServiceClient proxy = GetProxy();
            proxy.SelectSessionIdAsync();
        }

        //GetGlobalVariableAsync
        internal delegate void GetGlobalVariableCallBack(GlobalVariable itemSource);
        internal static void GetGlobalVariableAsync(GetGlobalVariableCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetGlobalVariableCompleted += new EventHandler<GetGlobalVariableCompletedEventArgs>(proxy_GetGlobalVariableCompleted);
            proxy.GetGlobalVariableAsync(callerKey);
        }

        static void proxy_GetGlobalVariableCompleted(object sender, GetGlobalVariableCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                GlobalVariable itemSource = e.Result;
                ((GetGlobalVariableCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ChangePasswordAsync
        internal delegate void ChangePasswordCallback(int status);
        internal static void ChangePasswordAsync(string oldPassword, string newPassword, ChangePasswordCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ChangePasswordCompleted += new EventHandler<ChangePasswordCompletedEventArgs>(proxy_ChangePasswordCompleted);
            proxy.ChangePasswordAsync(oldPassword, newPassword, callerKey);
        }

        static void proxy_ChangePasswordCompleted(object sender, ChangePasswordCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                int status = e.Result;

                ((ChangePasswordCallback)_callbacks[callerKey]).Invoke(status);

                _callbacks.Remove(callerKey);
            }
        }

        // ListAspUser
        internal delegate void ListAspUserCallback(List<AspUser> itemSource);
        internal static void ListAspUserAsync(int? orgId, Guid? userId, bool? isLegacy, ListAspUserCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListAspUserCompleted += new EventHandler<ListAspUserCompletedEventArgs>(proxy_ListAspUserCompleted);
            proxy.ListAspUserAsync(orgId, userId, isLegacy, callerKey);
        }

        static void proxy_ListAspUserCompleted(object sender, ListAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<AspUser> itemSource = e.Result;
                ((ListAspUserCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // UnlockAspUser
        internal delegate void UnlockAspUserCallBack(AspUser oldUser);
        internal static void UnlockAspUserAsync(AspUser oldUser, UnlockAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.UnlockAspUserCompleted += new EventHandler<UnlockAspUserCompletedEventArgs>(proxy_UnlockAspUserCompleted);
            proxy.UnlockAspUserAsync(oldUser, callerKey);
        }

        static void proxy_UnlockAspUserCompleted(object sender, UnlockAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                AspUser itemSource = e.Result;
                ((UnlockAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //Save AspUser
        internal delegate void SaveAspUserCallBack(AspUser result);
        internal static void SaveAspUserAsync(AspUser saveUser, SaveAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveAspUserCompleted += new EventHandler<SaveAspUserCompletedEventArgs>(proxy_SaveAspUserCompleted);
            proxy.SaveAspUserAsync(saveUser, callerKey);
        }

        static void proxy_SaveAspUserCompleted(object sender, SaveAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                AspUser result = e.Result;
                ((SaveAspUserCallBack)_callbacks[callerKey]).Invoke(result);

                _callbacks.Remove(callerKey);
            }
        }

        // ListCountry
        internal delegate void ListCountryCallback(List<Country> itemSource);
        internal static void ListCountryAsync(int? countryId, ListCountryCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListCountryCompleted += new EventHandler<ListCountryCompletedEventArgs>(proxy_ListCountryCompleted);
            proxy.ListCountryAsync(countryId, callerKey);
        }

        static void proxy_ListCountryCompleted(object sender, ListCountryCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Country> itemSource = e.Result;
                ((ListCountryCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        internal delegate void ListOrganisationCallback(List<Organisation> itemSource);
        internal static void ListOrganisationAsync(Guid? roleId, ListOrganisationCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListOrganisationCompleted += new EventHandler<ListOrganisationCompletedEventArgs>(proxy_ListOrganisationCompleted);
            proxy.ListOrganisationAsync(roleId, callerKey);
        }

        static void proxy_ListOrganisationCompleted(object sender, ListOrganisationCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Organisation> itemSource = new List<Organisation>();
                if (e.Result != null)
                    itemSource = e.Result;
                ((ListOrganisationCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //GetOrganisation
        internal delegate void GetOrganisationCallBack(Organisation itemSource);
        internal static void GetOrganisationAsync(int organisationId, GetOrganisationCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.GetOrganisationCompleted += new EventHandler<GetOrganisationCompletedEventArgs>(proxy_GetOrganisationCompleted);
            proxy.GetOrganisationAsync(organisationId, callerKey);
        }

        static void proxy_GetOrganisationCompleted(object sender, GetOrganisationCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                Organisation itemSource = e.Result;
                ((GetOrganisationCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveOrganisation
        internal static void SaveOrganisationAsync(List<Organisation> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveOrganisationsCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveOrganisationsAsync(saveList, callerKey);
        }

        internal delegate void ListSiteCallback(List<Site> itemSource);
        internal static void ListSiteAsync(int? orgId, int? siteId, bool showLegacy, bool loadContact, ListSiteCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListSiteCompleted += new EventHandler<ListSiteCompletedEventArgs>(proxy_ListSiteCompleted);
            proxy.ListSiteAsync(orgId, siteId, showLegacy, loadContact, callerKey);
        }

        static void proxy_ListSiteCompleted(object sender, ListSiteCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Site> itemSource = new List<Site>();
                if (e.Result != null)
                    itemSource = e.Result;
                ((ListSiteCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveSite
        internal static void SaveSiteAsync(List<Site> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveSiteCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveSiteAsync(saveList, callerKey);
        }

        // ListContactInformation
        internal delegate void ListContactInformationCallback(List<ContactInformation> itemSource);
        internal static void ListContactInformationAsync(int? contactInformationId, ListContactInformationCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListContactInformationCompleted += new EventHandler<ListContactInformationCompletedEventArgs>(proxy_ListContactInformationCompleted);
            proxy.ListContactInformationAsync(contactInformationId, callerKey);
        }

        static void proxy_ListContactInformationCompleted(object sender, ListContactInformationCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<ContactInformation> itemSource = e.Result;
                ((ListContactInformationCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveContactInformation
        internal static void SaveContactInformationAsync(List<ContactInformation> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveContactInformationCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveContactInformationAsync(saveList, callerKey);
        }

        // ListCustomer
        internal delegate void ListCustomerCallback(List<Customer> itemSource);
        internal static void ListCustomerAsync(int? orgId, int? customerId, string firstName, string lastName,
            int? siteId, bool hasContracts, DateTime? contractDateStart, DateTime? contractDateEnd, bool includeLegacy, ListCustomerCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListCustomerCompleted += new EventHandler<ListCustomerCompletedEventArgs>(proxy_ListCustomerCompleted);
            proxy.ListCustomerAsync(orgId, customerId, firstName, lastName, siteId, hasContracts, contractDateStart, contractDateEnd, includeLegacy, callerKey);
        }

        static void proxy_ListCustomerCompleted(object sender, ListCustomerCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Customer> itemSource = e.Result;
                ((ListCustomerCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveCustomer        
        internal static void SaveCustomerAsync(List<Customer> saveList, ListCustomerCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveCustomerCompleted += new EventHandler<SaveCustomerCompletedEventArgs>(proxy_SaveCustomerCompleted);
            proxy.SaveCustomerAsync(saveList, callerKey);
        }

        static void proxy_SaveCustomerCompleted(object sender, SaveCustomerCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Customer> itemSource = e.Result;
                ((ListCustomerCallback)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        #region Security
        // ListComponent
        internal delegate void ListComponentCallBack(List<Component> itemSource);
        internal static void ListComponentAsync(int? componentId, ListComponentCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListComponentCompleted += new EventHandler<ListComponentCompletedEventArgs>(proxy_ListComponentCompleted);
            proxy.ListComponentAsync(componentId, callerKey);
        }

        static void proxy_ListComponentCompleted(object sender, ListComponentCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Component> itemSource = e.Result;
                ((ListComponentCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListRoleComponentPermission
        internal delegate void ListRoleComponentPermissionCallBack(List<RoleComponentPermission> itemSource);
        internal static void ListRoleComponentPermissionAsync(Guid? roleId, int? componentId, ListRoleComponentPermissionCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListRoleComponentPermissionCompleted += new EventHandler<ListRoleComponentPermissionCompletedEventArgs>(proxy_ListRoleComponentPermissionCompleted);
            proxy.ListRoleComponentPermissionAsync(roleId, componentId, callerKey);
        }

        static void proxy_ListRoleComponentPermissionCompleted(object sender, ListRoleComponentPermissionCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<RoleComponentPermission> itemSource = e.Result;
                ((ListRoleComponentPermissionCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        internal static void SaveRoleComponentPermissionAsync(List<RoleComponentPermission> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveRoleComponentPermissionCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveRoleComponentPermissionAsync(saveList, callerKey);
        }

        // ListAspRole
        internal delegate void ListAspRoleCallBack(List<AspRole> itemSource);
        internal static void ListAspRoleAsync(Guid? roleId, ListAspRoleCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListAspRoleCompleted += new EventHandler<ListAspRoleCompletedEventArgs>(proxy_ListAspRoleCompleted);
            proxy.ListAspRoleAsync(roleId, callerKey);
        }

        static void proxy_ListAspRoleCompleted(object sender, ListAspRoleCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<AspRole> itemSource = e.Result;
                ((ListAspRoleCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        internal static void SaveAspRoleAsync(List<AspRole> saveList, string currentUser, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveAspRoleCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveAspRoleAsync(saveList, currentUser, callerKey);
        }

        // ListUserRoleAuth
        internal delegate void ListUserRoleAuthCallBack(List<UserRoleAuth> itemSource);
        internal static void ListUserRoleAuthAsync(int? orgId, Guid? userId, Guid? roleId, ListUserRoleAuthCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListUserRoleAuthCompleted += new EventHandler<ListUserRoleAuthCompletedEventArgs>(proxy_ListUserRoleAuthCompleted);
            proxy.ListUserRoleAuthAsync(orgId, userId, roleId, callerKey);
        }

        static void proxy_ListUserRoleAuthCompleted(object sender, ListUserRoleAuthCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<UserRoleAuth> itemSource = e.Result;
                ((ListUserRoleAuthCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        internal static void SaveUserRoleAuthAsync(List<UserRoleAuth> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveUserRoleAuthCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveUserRoleAuthAsync(saveList, callerKey);
        }

        // ListOrgAdminAspUser
        internal delegate void ListOrgAdminAspUserCallBack(List<AspUser> itemSource);
        internal static void ListOrgAdminAspUserAsync(int orgId, Guid roleid, ListOrgAdminAspUserCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListOrgAdminAspUserCompleted += new EventHandler<ListOrgAdminAspUserCompletedEventArgs>(proxy_ListOrgAdminAspUserCompleted);
            proxy.ListOrgAdminAspUserAsync(orgId, roleid, callerKey);
        }

        static void proxy_ListOrgAdminAspUserCompleted(object sender, ListOrgAdminAspUserCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<AspUser> itemSource = e.Result;
                ((ListOrgAdminAspUserCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        #endregion

        // ListEquipment
        internal delegate void ListEquipmentCallBack(List<Equipment> itemSource);
        internal static void ListEquipmentAsync(int? organisationId, int? equipmentId, bool showLegacy, ListEquipmentCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListEquipmentCompleted += new EventHandler<ListEquipmentCompletedEventArgs>(proxy_ListEquipmentCompleted);
            proxy.ListEquipmentAsync(organisationId, equipmentId, showLegacy, callerKey);
        }

        static void proxy_ListEquipmentCompleted(object sender, ListEquipmentCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Equipment> itemSource = e.Result;
                ((ListEquipmentCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveEquipment
        internal static void SaveEquipmentAsync(List<Equipment> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveEquipmentCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveEquipmentAsync(saveList, callerKey);
        }

        // ListService
        internal delegate void ListServiceCallBack(List<Service> itemSource);
        internal static void ListServiceAsync(int? organisationId, int? serviceId, bool showLegacy, ListServiceCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListServiceCompleted += new EventHandler<ListServiceCompletedEventArgs>(proxy_ListServiceCompleted);
            proxy.ListServiceAsync(organisationId, serviceId, showLegacy, callerKey);
        }

        static void proxy_ListServiceCompleted(object sender, ListServiceCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Service> itemSource = e.Result;
                ((ListServiceCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveService
        internal static void SaveServiceAsync(List<Service> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveServiceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveServiceAsync(saveList, callerKey);
        }

        // ListRoomType
        internal delegate void ListRoomTypeCallBack(List<RoomType> itemSource);
        internal static void ListRoomTypeAsync(int? organisationId, int? siteId, bool showLegacy, ListRoomTypeCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListRoomTypeCompleted += new EventHandler<ListRoomTypeCompletedEventArgs>(proxy_ListRoomTypeCompleted);
            proxy.ListRoomTypeAsync(organisationId, siteId, showLegacy, callerKey);
        }

        static void proxy_ListRoomTypeCompleted(object sender, ListRoomTypeCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<RoomType> itemSource = e.Result;
                ((ListRoomTypeCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveRoomType
        internal static void SaveRoomTypeAsync(List<RoomType> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveRoomTypeCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveRoomTypeAsync(saveList, callerKey);
        }

        // ListRoom
        internal delegate void ListRoomCallBack(List<Room> itemSource);
        internal static void ListRoomAsync(int orgId, int? siteId, int? roomId, string roomName, string roomStatusIds, string roomTypeIds, int? floor, bool showLegacy, ListRoomCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListRoomCompleted += new EventHandler<ListRoomCompletedEventArgs>(proxy_ListRoomCompleted);
            proxy.ListRoomAsync(orgId, siteId, roomId, roomName, roomStatusIds, roomTypeIds, floor, showLegacy, callerKey);
        }

        static void proxy_ListRoomCompleted(object sender, ListRoomCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Room> itemSource = e.Result;
                ((ListRoomCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveRoom
        internal static void SaveRoomAsync(List<Room> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveRoomCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveRoomAsync(saveList, callerKey);
        }

        // ListRoomEquipment
        internal delegate void ListRoomEquipmentCallBack(List<RoomEquipment> itemSource);
        internal static void ListRoomEquipmentAsync(int? roomEquipmentId, int? roomId, ListRoomEquipmentCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListRoomEquipmentCompleted += new EventHandler<ListRoomEquipmentCompletedEventArgs>(proxy_ListRoomEquipmentCompleted);
            proxy.ListRoomEquipmentAsync(roomEquipmentId, roomId, callerKey);
        }

        static void proxy_ListRoomEquipmentCompleted(object sender, ListRoomEquipmentCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<RoomEquipment> itemSource = e.Result;
                ((ListRoomEquipmentCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveRoomEquipment
        internal static void SaveRoomEquipmentAsync(List<RoomEquipment> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveRoomEquipmentCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveRoomEquipmentAsync(saveList, callerKey);
        }

        // ListRoomService
        internal delegate void ListRoomServiceCallBack(List<RoomService> itemSource);
        internal static void ListRoomServiceAsync(int? roomServiceId, int? roomId, ListRoomServiceCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListRoomServiceCompleted += new EventHandler<ListRoomServiceCompletedEventArgs>(proxy_ListRoomServiceCompleted);
            proxy.ListRoomServiceAsync(roomServiceId, roomId, callerKey);
        }

        static void proxy_ListRoomServiceCompleted(object sender, ListRoomServiceCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<RoomService> itemSource = e.Result;
                ((ListRoomServiceCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveRoomService
        internal static void SaveRoomServiceAsync(List<RoomService> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveRoomServiceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveRoomServiceAsync(saveList, callerKey);
        }

        // ListImage
        internal delegate void ListImageCallBack(List<common.Image> itemSource);
        internal static void ListImageAsync(int? imageId, int? itemId, int? imageTypeId, int loadType, ListImageCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListImageCompleted += new EventHandler<ListImageCompletedEventArgs>(proxy_ListImageCompleted);
            proxy.ListImageAsync(imageId, itemId, imageTypeId, loadType, callerKey);
        }

        static void proxy_ListImageCompleted(object sender, ListImageCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<common.Image> itemSource = e.Result;
                ((ListImageCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveImage
        internal static void SaveImageAsync(List<common.Image> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveImageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveImageAsync(saveList, callerKey);
        }

        // ListBooking
        internal delegate void ListBookingCallBack(List<Booking> itemSource);
        internal static void ListBookingAsync(int orgId, int? siteId, int? roomId, string roomName, int? bookingId, string bookingStatusIds,
            int? customerId, string customerName, DateTime? bookDateStart, DateTime? bookDateEnd, ListBookingCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListBookingCompleted += new EventHandler<ListBookingCompletedEventArgs>(proxy_ListBookingCompleted);
            proxy.ListBookingAsync(orgId, siteId, roomId, roomName, bookingId, bookingStatusIds,
                customerId, customerName, bookDateStart, bookDateEnd, callerKey);
        }

        static void proxy_ListBookingCompleted(object sender, ListBookingCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Booking> itemSource = e.Result;
                ((ListBookingCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveBooking
        internal static void SaveBookingAsync(List<Booking> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveBookingCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveBookingAsync(saveList, callerKey);
        }

        // ListBookingRoomEquipment
        internal delegate void ListBookingRoomEquipmentCallBack(List<BookingRoomEquipment> itemSource);
        internal static void ListBookingRoomEquipmentAsync(int? bookingRoomEquipmentId, int? bookingId, int? roomEquipmentId, ListBookingRoomEquipmentCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListBookingRoomEquipmentCompleted += new EventHandler<ListBookingRoomEquipmentCompletedEventArgs>(proxy_ListBookingRoomEquipmentCompleted);
            proxy.ListBookingRoomEquipmentAsync(bookingRoomEquipmentId, bookingId, roomEquipmentId, callerKey);
        }

        static void proxy_ListBookingRoomEquipmentCompleted(object sender, ListBookingRoomEquipmentCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<BookingRoomEquipment> itemSource = e.Result;
                ((ListBookingRoomEquipmentCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveBookingRoomEquipment
        internal static void SaveBookingRoomEquipmentAsync(List<BookingRoomEquipment> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveBookingRoomEquipmentCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveBookingRoomEquipmentAsync(saveList, callerKey);
        }


        // ListBookingRoomEquipmentDetail
        internal delegate void ListBookingRoomEquipmentDetailCallBack(List<BookingRoomEquipmentDetail> itemSource);
        internal static void ListBookingRoomEquipmentDetailAsync(int? bookingRoomEquipmentDetailId, int? bookingRoomEquipmentId, ListBookingRoomEquipmentDetailCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListBookingRoomEquipmentDetailCompleted += new EventHandler<ListBookingRoomEquipmentDetailCompletedEventArgs>(proxy_ListBookingRoomEquipmentDetailCompleted);
            proxy.ListBookingRoomEquipmentDetailAsync(bookingRoomEquipmentDetailId, bookingRoomEquipmentId, callerKey);
        }

        static void proxy_ListBookingRoomEquipmentDetailCompleted(object sender, ListBookingRoomEquipmentDetailCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<BookingRoomEquipmentDetail> itemSource = e.Result;
                ((ListBookingRoomEquipmentDetailCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveBookingRoomEquipmentDetail
        internal static void SaveBookingRoomEquipmentDetailAsync(List<BookingRoomEquipmentDetail> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveBookingRoomEquipmentDetailCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveBookingRoomEquipmentDetailAsync(saveList, callerKey);
        }


        // ListBookingRoomService
        internal delegate void ListBookingRoomServiceCallBack(List<BookingRoomService> itemSource);
        internal static void ListBookingRoomServiceAsync(int? bookingRoomServiceId, int? bookingId, int? roomServiceId, ListBookingRoomServiceCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListBookingRoomServiceCompleted += new EventHandler<ListBookingRoomServiceCompletedEventArgs>(proxy_ListBookingRoomServiceCompleted);
            proxy.ListBookingRoomServiceAsync(bookingRoomServiceId, bookingId, roomServiceId, callerKey);
        }

        static void proxy_ListBookingRoomServiceCompleted(object sender, ListBookingRoomServiceCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<BookingRoomService> itemSource = e.Result;
                ((ListBookingRoomServiceCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveBookingRoomService
        internal static void SaveBookingRoomServiceAsync(List<BookingRoomService> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveBookingRoomServiceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveBookingRoomServiceAsync(saveList, callerKey);
        }

        // ListBookingRoomServiceDetail
        internal delegate void ListBookingRoomServiceDetailCallBack(List<BookingRoomServiceDetail> itemSource);
        internal static void ListBookingRoomServiceDetailAsync(int? bookingRoomServiceDetailId, int? bookingRoomServiceId, ListBookingRoomServiceDetailCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListBookingRoomServiceDetailCompleted += new EventHandler<ListBookingRoomServiceDetailCompletedEventArgs>(proxy_ListBookingRoomServiceDetailCompleted);
            proxy.ListBookingRoomServiceDetailAsync(bookingRoomServiceDetailId, bookingRoomServiceId, callerKey);
        }

        static void proxy_ListBookingRoomServiceDetailCompleted(object sender, ListBookingRoomServiceDetailCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<BookingRoomServiceDetail> itemSource = e.Result;
                ((ListBookingRoomServiceDetailCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        //SaveBookingRoomServiceDetail
        internal static void SaveBookingRoomServiceDetailAsync(List<BookingRoomServiceDetail> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveBookingRoomServiceDetailCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveBookingRoomServiceDetailAsync(saveList, callerKey);
        }


        // ListSiteBySiteGroup
        internal delegate void ListSiteBySiteGroupCallBack(List<Site> itemSource);
        internal static void ListSiteBySiteGroupAsync(int? siteGroupId, bool? showLegacy, ListSiteBySiteGroupCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListSiteBySiteGroupCompleted += new EventHandler<ListSiteBySiteGroupCompletedEventArgs>(proxy_ListSiteBySiteGroupCompleted);
            proxy.ListSiteBySiteGroupAsync(siteGroupId, showLegacy, callerKey);
        }

        static void proxy_ListSiteBySiteGroupCompleted(object sender, ListSiteBySiteGroupCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<Site> itemSource = e.Result;
                ((ListSiteBySiteGroupCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        // ListSiteGroup
        internal delegate void ListSiteGroupCallBack(List<SiteGroup> itemSource);
        internal static void ListSiteGroupAsync(int? orgId, int? siteGroupId, ListSiteGroupCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListSiteGroupCompleted += new EventHandler<ListSiteGroupCompletedEventArgs>(proxy_ListSiteGroupCompleted);
            proxy.ListSiteGroupAsync(orgId, siteGroupId, callerKey);
        }

        static void proxy_ListSiteGroupCompleted(object sender, ListSiteGroupCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<SiteGroup> itemSource = e.Result;
                ((ListSiteGroupCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        internal static void SaveSiteGroupsAsync(List<SiteGroup> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveSiteGroupsCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveSiteGroupsAsync(saveList, callerKey);
        }

        // ListSiteGroupSite
        internal delegate void ListSiteGroupSiteCallBack(List<SiteGroupSite> itemSource);
        internal static void ListSiteGroupSiteAsync(int? siteGroupId, int? siteId, ListSiteGroupSiteCallBack callback)
        {
            Guid callerKey = Guid.NewGuid();

            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListSiteGroupSiteCompleted += new EventHandler<ListSiteGroupSiteCompletedEventArgs>(proxy_ListSiteGroupSiteCompleted);
            proxy.ListSiteGroupSiteAsync(siteGroupId, siteId, callerKey);
        }

        static void proxy_ListSiteGroupSiteCompleted(object sender, ListSiteGroupSiteCompletedEventArgs e)
        {
            Guid callerKey = (Guid)e.UserState;
            if (_callbacks.ContainsKey(callerKey))
            {
                List<SiteGroupSite> itemSource = e.Result;
                ((ListSiteGroupSiteCallBack)_callbacks[callerKey]).Invoke(itemSource);

                _callbacks.Remove(callerKey);
            }
        }

        internal static void SaveSiteGroupSitesAsync(List<SiteGroupSite> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveSiteGroupSitesCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveSiteGroupSitesAsync(saveList, callerKey);
        }

	}
}
