using NUnit.Framework;
using Android.Accounts;
using Mail.API;

namespace AMailSync.UnitTest {
    public class AccountManagerTest {
        [Test]
        public void GetList() {

        }
        public void AccountsList() {
            //AccountManager accountManager = 
            AccountManager.Get(null);   
        }

        public void AddAccount()
        {
            Mail.API.Account account = new Mail.API.Account(name: "testnct1@gmail.com");
        }
    }
}