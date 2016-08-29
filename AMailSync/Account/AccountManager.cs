using AManager = Android.Accounts.AccountManager;

namespace AMailSync {
    class IntAccountManager {
        IntAccountManager() {
            AManager accountManager = AManager.Get(Application.Instance.ApplicationContext);
            var accounts = accountManager.GetAccounts();
        }
    }
}