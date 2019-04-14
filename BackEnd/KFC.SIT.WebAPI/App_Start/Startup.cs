using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KFC.SIT.WebAPI.App_Start.Startup))]

namespace KFC.SIT.WebAPI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Map("/signalr", map =>
            {

                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };

                map.RunSignalR(hubConfiguration);
            });

        }
    }
}
