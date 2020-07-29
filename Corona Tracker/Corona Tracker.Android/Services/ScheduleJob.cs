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
using Corona_Tracker.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(Corona_Tracker.Droid.Services.ScheduleJob))]

namespace Corona_Tracker.Droid.Services
{
    public class ScheduleJob : IScheduleJob
    {
        public bool Schedule()
        {
            return MainActivity.Instance.scheduleJob();
        }

        public bool Cancel()
        {
            return MainActivity.Instance.cancelJob();
        }
    }
}