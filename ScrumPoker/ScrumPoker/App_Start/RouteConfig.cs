using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ScrumPoker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Client",
                url: "Client/{id}/{action}",
                defaults: new { controller = "Client", action = "Index" }
                );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/{roomAdminId}/{action}",
                defaults: new { controller = "Admin", action = "Index" }
                );

            routes.MapRoute(
                name: "Dashboard",
                url: "Dashboard/{id}/{action}",
                defaults: new { controller = "Dashboard", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}