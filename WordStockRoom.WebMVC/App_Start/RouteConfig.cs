using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WordStockRoom.WebMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Word",
                url: "Word/{languageId}/{action}/{id}",
                defaults: new { controller = "Word", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Sentence",
                url: "Sentence/{languageId}/{wordId}/{action}/{id}",
                defaults: new { controller = "Sentence", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Video",
                url: "Video/{languageId}/{wordId}/{action}/{id}",
                defaults: new { controller = "Video", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
