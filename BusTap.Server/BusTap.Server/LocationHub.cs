using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BusTap.Server
{
    public class LocationHub : Hub
    {
        public void send(string name, decimal longitude, decimal latitude)
        {
            Clients.All.broadcastLocation(name, longitude, latitude);
        }
    }
}