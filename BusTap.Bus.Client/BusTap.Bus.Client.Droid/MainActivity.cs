using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BusTap.Bus.Client.ServiceClient;
using Microsoft.AspNet.SignalR.Client;

namespace BusTap.Bus.Client.Droid
{
    [Activity(Label = "BusTap.Bus.Client.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        private HubConnection conn = null;
        private IHubProxy locationHubProxy = null;
        private Guid uniqueUserId;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            uniqueUserId =Guid.NewGuid();

            conn = new HubConnection("http://darkmaster-001-site1.btempurl.com/");
            //conn = new HubConnection("http://localhost:56977/");
            locationHubProxy = conn.CreateHubProxy("LocationHub");
            locationHubProxy.On<string, decimal, decimal>("broadcastLocation", (name, longitude, latitude) =>
            {
              
                this.RunOnUiThread(() =>
                {
                    //put on map
                    //FindViewById<LinearLayout>(Resource.Id.llChatMessages).AddView(txtview);
                });
            });

            await conn.Start();

            var buttonSendLocation = FindViewById<Button>(Resource.Id.SendLocation);
            buttonSendLocation.Click += Button_Click;
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            var geoLocationService = new GeoLocationService();
            var position = await geoLocationService.GetGeolocation();
            await locationHubProxy.Invoke("send", $"user-{uniqueUserId}", position.Longitude, position.Latitude);
        }
    }
}


