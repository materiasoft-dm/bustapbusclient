﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(BusTap.Server.Startup))]

namespace BusTap.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
