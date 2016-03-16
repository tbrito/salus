using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Web.Extensions
{
    public static class RouteExtensions
    {
        private const string RouteNameDataTokenKey = "__routeName";

        public static void SetRouteName(this Route route, string routeName)
        {
            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }

            route.DataTokens[RouteNameDataTokenKey] = routeName;
        }

        public static string GetRouteName(this Route route)
        {
            return route.DataTokens != null ? route.DataTokens[RouteNameDataTokenKey] as string : null;
        }

        public static string GetRouteName(this RouteData routeData)
        {
            return routeData.DataTokens != null ? routeData.DataTokens[RouteNameDataTokenKey] as string : null;
        }

        public static string[] GetNamespaces(this Route route)
        {
            return route.DataTokens != null ? route.DataTokens["namespaces"] as string[] : null;
        }

        public static string GetControllerName(this Route route)
        {
            return route.Defaults != null ? route.Defaults["controller"] as string : null;
        }

        public static string GetControllerName(this RouteData routeData)
        {
            return routeData.Values != null ? routeData.Values["controller"] as string : null;
        }

        public static string GetActionName(this Route route)
        {
            return route.Defaults != null ? route.Defaults["action"] as string : null;
        }

        public static string GetActionName(this RouteData routeData)
        {
            return routeData.Values != null ? routeData.Values["action"] as string : null;
        }
    }
}
