using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BusTap.Bus.Client.ServiceClient;

namespace BusTap.Bus.Client.Droid
{
	[Activity (Label = "BusTap.Bus.Client.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

            button.Click += Button_Click;
		}

        private async void Button_Click(object sender, EventArgs e)
        {
            var geoLocationService = new GeoLocationService();
            var position = await geoLocationService.GetGeolocation();
            string message = $"Lat = {position.Latitude} ||| Lon = {position.Longitude}";
            Toast.MakeText(ApplicationContext, message, ToastLength.Long).Show();
        }
    }
}


