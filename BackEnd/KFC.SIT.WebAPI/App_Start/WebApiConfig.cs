using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KFC.SIT.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            IAppBuilder app = new AppBuilder();

            app.Map("/signalr", map =>
            {
                
                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };

                map.RunSignalR(hubConfiguration);
            });

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
          



            //config.routes.maphttproute(
            //    name: "defaulthub",
            //    routetemplate: "signalr/{hub}/{id",
            //    defaults: new { id = routeparameter.optional }
            //);





        }


    }
}
