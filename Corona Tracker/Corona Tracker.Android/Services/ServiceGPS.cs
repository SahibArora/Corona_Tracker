using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Corona_Tracker.Droid.Services
{
    [Service(Name = "Corona_Tracker.Droid.Services.ServiceGPS",
         Permission = "android.permission.BIND_JOB_SERVICE")]
    class ServiceGPS : JobService
    {
        Locations l;
        public override bool OnStartJob(JobParameters @params)
        {
            Task.Run(async() =>
            {
                l = new Locations();
                await l.GetUserLocation();

                JobFinished(@params, false);
            });

            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }
    }
}