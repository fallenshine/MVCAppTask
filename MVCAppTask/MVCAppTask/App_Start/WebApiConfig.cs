using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MVCAppTask
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Re-Define the Default Routing (uses action name routing to methods in the controller)
            config.Routes.MapHttpRoute(
                    name: "API Default",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional });
        }
    }
}
