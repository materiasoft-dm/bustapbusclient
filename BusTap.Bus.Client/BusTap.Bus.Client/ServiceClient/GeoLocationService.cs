using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content.Res;
using BusTap.Bus.Client.Models;
using Plugin.Geolocator;

namespace BusTap.Bus.Client.ServiceClient
{
    public class GeoLocationService
    {
        public async Task<LatLong> GetGeolocation()
        {
            var latlong = new LatLong();
            if (CrossGeolocator.Current.IsGeolocationEnabled == false)
            {
                throw new Exception("Geolocation is Unavailable ");
            }
            else
            {
                var locator = CrossGeolocator.Current;
                //locator.DesiredAccuracy = 50;               
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 50000);

                latlong.Latitude = position.Latitude;
               latlong.Longitude = position.Longitude;

                
            }

            return latlong;
        }
    }
}
