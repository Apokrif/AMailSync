using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace AMailSync.Services
{
    [Service]
    [IntentFilter(new string[] {"io.mail.TimeService"})]
    public class TimeService : Service
    {
        private TimeServiceBinder binder;

        public override IBinder OnBind(Intent intent)
        {
            binder = new TimeServiceBinder(this);
            return binder;
        }

        public string Time()
        {
            string now = DateTime.Now.ToShortTimeString();
            string nowAtKathmandu = now + "2.45";
            return "Time from SoleService: " + now;
        }
    }
}