using System;
using AMailSync.Services;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace AMailSync.View
{
    [Activity(Label = "Monitor")]
    public class MonitorActivity : Activity
    {
        #region Activity

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MonitorLayout);
            InitializeContent();
        }

        protected override void OnStart()
        {
            base.OnStart();

            //Since Android 5.0 (Lollipop) bindService() must always be called with an explicit intent. 
            //This was previously a recommendation, but since Lollipop it is enforced: by
            //java.lang.IllegalArgumentException
            connection = new ServiceConnection(this);
            Intent intent = new Intent(this, typeof(TimeService));
            var result = BindService(intent, connection, Bind.AutoCreate);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!isConfigurationChange)
            {
                if (isBound)
                {
                    UnbindService(connection);
                    isBound = false;
                }
            }
        }

        #endregion

        void InitializeContent()
        {
            Button theButton;

            theButton = FindViewById<Button>(Resource.Id.buttonStartService);
            theButton.Click += (sender, e) => { StartService(); };
            theButton = FindViewById<Button>(Resource.Id.buttonStopService);
            theButton.Click += (sender, e) => { StopService(); };

            FindViewById<Button>(Resource.Id.buttonRefresh)
                .Click += (sender, e) => { CallService(); };
        }

        #region ServiceCommunication

        private void StartService()
        {
            StartService(new Intent(this, typeof(TimeService)));
        }

        private void StopService()
        {
            StopService(new Intent(this, typeof(TimeService)));
        }

        private void CallService()
        {
            RunOnUiThread(() =>
            {
                string text = binder.GetService().Time();
                Console.WriteLine("{0} returned from Sole Service", text);
            });
        }

        #endregion

        #region Service Connection

        private TimeServiceBinder binder;
        private ServiceConnection connection;
        private bool isConfigurationChange = false;
        private bool isBound = false;

        class ServiceConnection : Java.Lang.Object, IServiceConnection
        {
            private MonitorActivity activity;

            public ServiceConnection(MonitorActivity activity)
            {
                this.activity = activity;
            }

            void IServiceConnection.OnServiceConnected(ComponentName name, IBinder service)
            {
                var serviceBinder = service as TimeServiceBinder;
                if (serviceBinder == null)
                    return;
                activity.binder = serviceBinder;
            }

            void IServiceConnection.OnServiceDisconnected(ComponentName name)
            {
                activity.binder = null;
            }
        }

        // return the service connection if there is a configuration change
        public override Java.Lang.Object OnRetainNonConfigurationInstance()
        {
            base.OnRetainNonConfigurationInstance();

            isConfigurationChange = true;

            return connection;
        }

        #endregion
    }
}