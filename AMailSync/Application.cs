using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Account = Android.Accounts.Account;
using AManager = Android.Accounts.AccountManager;
//using Mail.API;

namespace AMailSync {
    /// <summary>
    /// Maintain global application state
    /// https://developer.xamarin.com/api/type/Android.App.Application/
    /// </summary>
    class Application : Android.App.Application {
        #region Singleton
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ff650316.aspx
        /// http://csharpindepth.com/Articles/General/Singleton.aspx
        /// </summary>
        private static readonly Application instance = new Application();
        private Application() {
            initializeAccounts();
        }
        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        /// </summary>
        static Application() {
        }
        public static Application Instance {
            get {
                return instance;
            }
        }
        #endregion

        #region Accounts
        public Account[] accounts;
        void initializeAccounts() {
            AManager accountManager = AManager.Get(Instance.ApplicationContext);
            accounts = accountManager.GetAccounts();

        }
        #endregion
    }
}