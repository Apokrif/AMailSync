using System.Collections.Generic;
using AAccount = Android.Accounts;
using Android.Widget;

namespace Mail.API {
    public class Account {
        public Account(string name) {
            Name = name;
        }
        public Account(string name, string password="a3141592653") {
            Name = (name);
            Password = password;
        }

        public string Name { get; private set; }
        public string Password { get; private set; }

        public static List<Account> GetAll(Filter filter)
        {
            return new List<Account>();
        }
}
}