using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ScrumPoker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "AdminApi",
                routeTemplate: "api/admin/{roomId}/{action}",
                defaults: new { controller = "Admin" }
            );

            config.Routes.MapHttpRoute(
                name: "ParticipantApi",
                routeTemplate: "api/participant/{participantId}/{action}",
                defaults: new { controller = "Participant" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
