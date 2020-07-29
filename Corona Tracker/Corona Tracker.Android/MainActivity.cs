using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Android.App.Job;
using Android.Support.V4.App;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Util;
using System.Diagnostics;
using Corona_Tracker.Droid.Services;

namespace Corona_Tracker.Droid
{
    [Activity(Label = "Corona_Tracker", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestLocationId = 0;

        internal static MainActivity Instance { get; set; }

        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnStart()
        {   
            base.OnStart();
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
                else
                {
                    // Permissions already granted - display a message.
                }
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    // Permissions granted - display a message.
                }
                else
                {
                    // Permissions denied - display a message.
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        public bool scheduleJob()
        {
            try
            {
                var jobBuilder = this.CreateJobBuilderUsingJobId<ServiceGPS>(111111);
                
                var jobInfo = jobBuilder
                    .SetPeriodic(20 * 60 * 1000, 5 * 60 * 1000)
                    .Build();

                var jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
                var scheduleResult = jobScheduler.Schedule(jobInfo);

                if (JobScheduler.ResultSuccess == scheduleResult)
                {
                    Toast.MakeText(Android.App.Application.Context, "Scheduled!", ToastLength.Long).Show();
                    return true;
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, "Scheduling Failed!", ToastLength.Long).Show();
                    return false;
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(Android.App.Application.Context, "Error!", ToastLength.Long).Show();
                return false;
            }
        }

        public bool cancelJob()
        {
            try
            {
                var jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
                jobScheduler.Cancel(111111);
                Toast.MakeText(Android.App.Application.Context, "Canceled!", ToastLength.Long).Show();
                return true;
            }
            catch (Exception e)
            {
                Toast.MakeText(Android.App.Application.Context, "Error!", ToastLength.Long).Show();
                return false;
            }
        }
    }
}