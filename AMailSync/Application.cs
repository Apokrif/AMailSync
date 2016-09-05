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
using Android.Content.PM;
using Android.App;
//using Mail.API;

namespace AMailSync {
    /// <summary>
    /// Maintain global application state
    /// https://developer.xamarin.com/api/type/Android.App.Application/
    /// </summary>
    [Application]
    class SyncApplication : Android.App.Application {
        #region Singleton
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ff650316.aspx
        /// http://csharpindepth.com/Articles/General/Singleton.aspx
        /// </summary>
        //private static readonly Application instance = new Application();
        public SyncApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer) {
            CheckPermissions();
            InitializeAccounts();
        }
        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        /// http://csharpindepth.com/Articles/General/Beforefieldinit.aspx
        /// </summary>
        static SyncApplication() {
        }
        //public static Application Instance {
        //    get {
        //        return instance;
        //    }
        //}
        #endregion

        #region Application

        public void Initialize()
        {
            
        }
        private void CheckPermissions() {
            var applicationInfo = ApplicationInfo;
            var dir = CacheDir;
            var permission = CheckCallingOrSelfPermission("android.permission.INTERNET");
            if (permission == Permission.Denied) {
                throw new Exception("Insufficient permissions");
            }
            //Assert();
        }

        #endregion

        #region Accounts
        public Account[] accounts;
        public void InitializeAccounts() {
            AManager accountManager = AManager.Get(ApplicationContext);
            accounts = accountManager.GetAccounts();

        }
        #endregion
    }
}