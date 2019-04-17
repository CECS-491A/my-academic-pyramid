using System;
using System.Threading.Tasks;
using System.Web.Services.Description;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(KFC.SIT.WebAPI.App_Start.Startup))]

namespace KFC.SIT.WebAPI.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

        }

    }
}
