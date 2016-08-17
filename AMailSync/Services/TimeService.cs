using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace AMailSync.Services
{
    [IntentFilter(new String[] {"io.mail.TimeService"})]
    public class TimeService : Service
    {
        private TimeServiceBinder binder;

        public override IBinder OnBind(Intent intent)
        {
            binder = new TimeServiceBinder(this);
            return binder;
        }

        public string GetTime()
        {
            String now = DateTime.Now.ToShortTimeString();
            return "Time from Sole service: " + now;
        }
    }
}