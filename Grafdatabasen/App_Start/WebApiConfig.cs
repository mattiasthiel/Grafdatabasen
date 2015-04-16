using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Neo4jClient;


namespace Grafdatabasen
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var url = "http://localhost:7474/db/data";
            var client = new GraphClient(new Uri(url));
            client.Connect();

            GraphClient = client;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static IGraphClient GraphClient { get; private set; }
    }
}
