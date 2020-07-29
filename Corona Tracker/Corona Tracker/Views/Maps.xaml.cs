using Corona_Tracker.Database;
using Corona_Tracker.Models;
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
    public partial class Maps : ContentPage
    {
        List<CurrentLocation> locas;
        FileDB file;
        public Maps()
        {
            InitializeComponent();

            locas = new List<CurrentLocation>();
            file = new FileDB();

            locas = file.getData();

            MapLocation.Clicked += (sender, e) =>
            {
                GetUserLocation();
            };
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);

            Position p = new Position(location.Latitude, location.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(0.600));
            map.MoveToRegion(mapSpan);
        }
        public void GetUserLocation()
        {
            try
            {
                if (locas != null)
                {
                    foreach (var obj in locas)
                    {
                        Position position = new Position(obj.latitude, obj.longitude);
                        Pin pin = new Pin
                        {
                            Label = "Coronavirus Alert",
                            Address = "Patient was here!",
                            Type = PinType.Place,
                            Position = position
                        };
                        map.Pins.Add(pin);
                    }
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}