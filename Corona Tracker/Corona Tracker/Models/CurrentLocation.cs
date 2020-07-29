using System;
using System.Collections.Generic;
using System.Text;

namespace Corona_Tracker.Models
{
    public class CurrentLocation
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string address { get; set; }
        public DateTime dateTime { get; set; }

        public CurrentLocation() { 
            
        }
        public CurrentLocation(double latitude, double longitude, string address, DateTime dateTime) {
            this.latitude = latitude;
            this.longitude = longitude;
            this.address = address;
            this.dateTime = dateTime;
        }
    }
}
