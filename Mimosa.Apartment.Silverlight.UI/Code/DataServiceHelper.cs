﻿using System;
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
        internal static void ListCustomerAsync(int? customerId, string name, bool includeLegacy, ListCustomerCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.ListCustomerCompleted += new EventHandler<ListCustomerCompletedEventArgs>(proxy_ListCustomerCompleted);
            proxy.ListCustomerAsync(customerId, name, includeLegacy, callerKey);
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
        internal static void SaveCustomerAsync(List<Customer> saveList, EmptyCallback callback)
        {
            Guid callerKey = Guid.NewGuid();
            ApartmentServiceClient proxy = GetProxy(callerKey, callback);
            proxy.SaveCustomerCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(proxy_VoidMethodCompleted);
            proxy.SaveCustomerAsync(saveList, callerKey);
        }

	}
}