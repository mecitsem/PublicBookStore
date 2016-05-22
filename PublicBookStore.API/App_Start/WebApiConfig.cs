using Microsoft.Practices.Unity;
using PublicBookStore.API.Initializer;
using PublicBookStore.API.Interfaces;
using PublicBookStore.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PublicBookStore.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Initialization of Unity container
            Bootstrapper.Initialise(config);

            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
