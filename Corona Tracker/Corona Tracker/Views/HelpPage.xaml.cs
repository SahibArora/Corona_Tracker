using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Corona_Tracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        public void GetLocationPage(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new Locations());
        }

        public void GetMapsPage(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new Maps());
        }
    }
}