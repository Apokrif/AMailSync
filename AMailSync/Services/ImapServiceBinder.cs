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
    class ImapServiceBinder : Android.OS.Binder {
        private ImapService service;
        ///
        /// <param name="service">change to correct service typó</param>
        public ImapServiceBinder(ImapService service) {
            this.service = service;
        }

        public ImapService GetService() {
            return service;
        }

    }
}