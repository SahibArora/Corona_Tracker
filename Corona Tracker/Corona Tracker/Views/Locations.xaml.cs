using Android.Content;
using Corona_Tracker.Database;
using Corona_Tracker.Interface;
using Corona_Tracker.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Corona_Tracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Locations : ContentPage
    {
        CurrentLocation loca;
        List<CurrentLocation> locas;
        FileDB file;

        public Locations()
        {
            InitializeComponent();

            locas = new List<CurrentLocation>();
            file = new FileDB();

            startService.Clicked += (sender, e) =>
            {
                DependencyService.Get<IScheduleJob>().Schedule();
                startService.IsVisible = false;
                stopService.IsVisible = true;
            };

            stopService.Clicked += (sender, e) =>
            {
                DependencyService.Get<IScheduleJob>().Cancel();
                stopService.IsVisible = false;
                startService.IsVisible = true;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            locas = file.getData();
            LocationList.ItemsSource = locas;
        }
        public async Task GetUserLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    // getting string address

                    Geocoder geoCoder = new Geocoder();
                    Position p = new Position(location.Latitude, location.Longitude);
                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(p);

                    loca = new CurrentLocation(location.Latitude, location.Longitude,possibleAddresses.FirstOrDefault(), DateTime.Now);

                    file = new FileDB();
                    file.saveData(loca);

                    locas = file.getData();
                    LocationList.ItemsSource = locas;
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}