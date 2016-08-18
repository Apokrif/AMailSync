using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AMailSync.Services {
    class ImapService : Service {
        #region Bind
        private ImapServiceBinder binder;
        public override IBinder OnBind(Intent intent) {
            return new ImapServiceBinder(this);
        }
        #endregion
        public void Connect() {
            //Mail.Client.
        }
    }
    public static class Mail {
        static Mail() {
            //Client = new ImapClient();
        }

        //public static ImapClient Client { get; set; }

    }
}