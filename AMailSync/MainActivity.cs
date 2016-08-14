﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AMailSync
{
    [Activity(Label = "AMailSync", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;
        //private static readonly string 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click +=
                delegate { button.Text = string.Format("{0} Timer clicks!", DateTime.Now.ToLongTimeString()); };

            Button buttonMonitor = FindViewById<Button>(Resource.Id.FollowToMonitorButton);

            buttonMonitor.Click +=
                delegate { StartActivity(new Intent("AMailSync.ServiceMonitorActivity")); };
        }

    }
}