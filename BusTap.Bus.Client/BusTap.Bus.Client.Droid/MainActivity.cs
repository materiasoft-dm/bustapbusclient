using System;
using System.Collections.Generic;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BusTap.Bus.Client.Models;
using BusTap.Bus.Client.ServiceClient;
using Microsoft.AspNet.SignalR.Client;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace BusTap.Bus.Client.Droid
{
    [Activity(Label = "BusTap.Bus.Client.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        GoogleMap googleMap;
        MapView mapView;


        private HubConnection conn = null;
        private IHubProxy locationHubProxy = null;
        private string uniqueUserId;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            AccountManager manager = AccountManager.Get(this);
            uniqueUserId = manager.GetAccountsByType("com.google")[0].Name;


            mapView = FindViewById<MapView>(Resource.Id.map);
            mapView.OnCreate(bundle);
            mapView.GetMapAsync(this);
            
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            this.googleMap = googleMap;
            this.googleMap.UiSettings.ZoomControlsEnabled = true;
            this.googleMap.UiSettings.SetAllGesturesEnabled(true);
            //Setup and customize your Google Map
            
            MapsInitializer.Initialize(this);

            var position = await CrossGeolocator.Current.GetPositionAsync(10000);
            var me = new LatLng(position.Latitude, position.Longitude);
            this.googleMap.MoveCamera(CameraUpdateFactory.NewLatLng(me));

            conn = new HubConnection("http://darkmaster-001-site1.btempurl.com/");
            //conn = new HubConnection("http://localhost:56977/");
            locationHubProxy = conn.CreateHubProxy("LocationHub");
            locationHubProxy.On<string, double, double>("broadcastLocation", (name, longitude, latitude) =>
            {

                this.RunOnUiThread(() =>
                {
                    //put on map
               
                    var marker = new MarkerOptions();
                    marker.SetTitle(uniqueUserId);
                    marker.SetPosition(new LatLng(latitude, longitude));
                    this.googleMap.AddMarker(marker);
                });
            });

            await conn.Start();
            
            await locationHubProxy.Invoke("send", $"user-{uniqueUserId}", position.Longitude, position.Latitude);

        }
    }
}


