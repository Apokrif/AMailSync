using System;
using AMailSync.Services;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace AMailSync.View {
    [Activity(Label = "Monitor")]
    public class MonitorActivity : Activity {
        #region Activity

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MonitorLayout);
            InitializeContent();
        }

        protected override void OnStart(){
            base.OnStart();

            var result = BindService();
        }


        protected override void OnDestroy() {
            base.OnDestroy();

            if (!isConfigurationChange) {
                if (isBound) {
                    UnbindService(connection);
                    isBound = false;
                }
            }
        }

        #endregion

        /// <summary>
        /// Set up widgets reaction on user events
        /// </summary>
        void InitializeContent() {
            // This temporary variable is for the start button
            // ReSharper disable once JoinDeclarationAndInitializer
            Button theButton;
            theButton = FindViewById<Button>(Resource.Id.buttonStartService);
            theButton.Click += (sender, e) => { StartService(); };

            // Use the same vriable for another button
            theButton = FindViewById<Button>(Resource.Id.buttonStopService);
            theButton.Click += (sender, e) => { StopService(); };

            // Third case do not use explicit auxilary variable
            FindViewById<Button>(Resource.Id.buttonRefresh)
                .Click += (sender, e) => { CallService(); };
        }
        #region Monitor

        private string SomeText {
            get {
                return
                    FindViewById<TextView>(Resource.Id.textViewServiceResponce).Text;
            }
            set { FindViewById<TextView>(Resource.Id.textViewServiceResponce).Text = value; }
        }

        #endregion
        
        #region Service Communication

        private void StartService() {
            StartService(new Intent(this, typeof(TimeService)));
            SomeText = typeof (TimeService).ToString();
        }

        private void StopService() {
            var result = StopService(new Intent(this, typeof(TimeService)));
            SomeText = "Stop service, result: " + result.ToString();
        }
        /// <summary>
        /// Bind to Time service by explicit Intent 
        /// </summary>
        /// <returns>true if success</returns>
        private bool BindService() {
            (Application as SyncApplication).Initialize();
            //Since Android 5.0 (Lollipop) bindService() must always be called with an explicit intent. 
            //This was previously a recommendation, but since Lollipop it is enforced: by
            //java.lang.IllegalArgumentException
            connection = new ServiceConnection(this);
            Intent intent = new Intent(this, typeof(TimeService));
            var result = BindService(intent, connection, Bind.AutoCreate);
            return result;
        }

        private void CallService() {
            RunOnUiThread(() => {
                string text = binder.GetService().Time();
                Console.WriteLine("{0} returned from Sole Service", text);
                FindViewById<TextView>(Resource.Id.textViewServiceResponce)
                    .Text = text;
            });
        }

        #endregion

        #region Service Connection

        private TimeServiceBinder binder;
        private ServiceConnection connection;
        private bool isConfigurationChange = false;
        private bool isBound = false;

        class ServiceConnection : Java.Lang.Object, IServiceConnection {
            private MonitorActivity activity;

            public ServiceConnection(MonitorActivity activity) {
                this.activity = activity;
            }

            void IServiceConnection.OnServiceConnected(ComponentName name, IBinder service) {
                var serviceBinder = service as TimeServiceBinder;
                if (serviceBinder == null)
                    return;
                activity.binder = serviceBinder;
            }

            void IServiceConnection.OnServiceDisconnected(ComponentName name) {
                activity.binder = null;
            }
        }

        // return the service connection if there is a configuration change
        [Obsolete("deprecated")]
        public override Java.Lang.Object OnRetainNonConfigurationInstance() {
            base.OnRetainNonConfigurationInstance();

            isConfigurationChange = true;

            return connection;
        }

        #endregion
    }
}