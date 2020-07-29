using Corona_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Corona_Tracker.Database
{
    public class FileDB
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Locations.txt");

        public FileDB() { 
            
        }
        public List<CurrentLocation> getData() {
            List<CurrentLocation> locas = new List<CurrentLocation>();
            string[] obj = null;

            if (File.Exists(_fileName))
            {
                foreach (string line in File.ReadLines(_fileName)) {
                    obj = line.Split('|');

                    DateTimeOffset dtOffset;
                    DateTimeOffset.TryParse(obj[3], null, DateTimeStyles.None, out dtOffset);

                    locas.Add(new CurrentLocation(Convert.ToDouble(obj[0]),Convert.ToDouble(obj[1]),obj[2], dtOffset.DateTime));
                }
                return locas;
            }
            else {
                return null;
            }
        }

        public bool saveData(CurrentLocation currentLocation) {
            try
            {
                string text = currentLocation.latitude + "|" + currentLocation.longitude + "|" + currentLocation.address + "|" + currentLocation.dateTime.ToString() + '\n';
                File.AppendAllText(_fileName, text);
                return true;
            } catch (Exception e) {
                return false;
            }
            
        }
    }
}
