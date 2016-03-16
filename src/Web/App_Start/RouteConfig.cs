using System;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Extensions;

namespace Web
{
    public class RouteConfig
    {
        private static bool estaConfigurado;

        public static void RegisterRoutes(RouteCollection routes)
        {
            if (estaConfigurado == false)
            {
                MapIgnoredRoutes(routes);
                routes.DiscoverMvcControllerRoutes();
                MapDefaultRoute(routes);

                estaConfigurado = true;
            }
        }

        private static void MapDefaultRoute(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        private static void MapIgnoredRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
            routes.IgnoreRoute("{resource}.gif/{*pathInfo}");
            routes.IgnoreRoute("{resource}.png/{*pathInfo}");
            routes.IgnoreRoute("{resource}.jpg/{*pathInfo}");
        }
    }
}
