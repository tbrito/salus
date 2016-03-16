using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Extensions
{
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Discover and add routes for controller methods in the calling assembly
        /// which declare the UrlRoute attribute.
        /// </summary>
        /// <param name="routes">Route collection to add the discovered routes to.</param>
        public static void DiscoverMvcControllerRoutes(this RouteCollection routes)
        {
            routes.DiscoverMvcControllerRoutes(Assembly.GetCallingAssembly(), null);
        }

        /// <summary>
        /// Discover and add routes for controller methods in the specified assembly
        /// which declare the UrlRoute attribute.
        /// </summary>
        /// <param name="routes">Route collection to add the discovered routes to.</param>
        /// <param name="assembly">Assembly to search routes for.</param>
        /// <param name="namespacePrefix">If specified, will only add controllers belonging to a namespace which begins with the prefix.</param>
        public static void DiscoverMvcControllerRoutes(this RouteCollection routes, Assembly assembly, string namespacePrefix)
        {
            // Enumerate assembly for UrlRoute attributes.
            List<MapRouteParams> routeParams = GetRouteParamsFromAttributes(assembly, namespacePrefix, null);

            // Sort the routes based on the Order attribute.
            routeParams.Sort((a, b) => a.Order.CompareTo(b.Order));

            // Add the routes to the routes collection.
            foreach (MapRouteParams rd in routeParams)
            {
                ////Trace.TraceInformation(
                ////    "Adding route {{ Priority = {0}, Name = {1}, Path = {2}, Controller = {3}, Action = {4} }}",
                ////    rd.Order, rd.RouteName ?? "<null>", rd.Path, rd.ControllerName, rd.ActionName);

                // Controller and action is always the class/method that
                // the UrlRoute attribute was declared on (if you set
                // these as route parameters it will be overridden here).
                rd.Defaults["controller"] = rd.ControllerName;
                rd.Defaults["action"] = rd.ActionName;

                RouteCollectionExtensions.MapRoute(routes,
                    rd.RouteName,
                    rd.Path,
                    rd.Defaults,
                    rd.Constraints,
                    new string[] { rd.ControllerNamespace });
            }
        }

        /// <summary>
        /// Discover all UrlRoute attributes declared on controller classes
        /// in the area and add them to the routing table.
        /// </summary>
        /// <param name="ctx"></param>
        public static void DiscoverMvcAreaRoutes(this AreaRegistrationContext ctx)
        {
            ctx.DiscoverMvcAreaRoutes(Assembly.GetCallingAssembly(), null);
        }

        /// <summary>
        /// Discover all UrlRoute attributes declared on controller classes
        /// in the area and add them to the routing table.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="basePath"></param>
        public static void DiscoverMvcAreaRoutes(this AreaRegistrationContext ctx, string basePath)
        {
            ctx.DiscoverMvcAreaRoutes(Assembly.GetCallingAssembly(), basePath);
        }

        /// <summary>
        /// Discover all UrlRoute attributes declared on controller classes
        /// in the area and add them to the routing table.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="assembly"></param>
        public static void DiscoverMvcAreaRoutes(this AreaRegistrationContext ctx, Assembly assembly)
        {
            DiscoverMvcAreaRoutes(ctx, assembly, null);
        }

        /// <summary>
        /// Discover all UrlRoute attributes declared on controller classes
        /// in the area and add them to the routing table.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="assembly"></param>
        /// <param name="basePath">Optional base path which will be prefixed to all discovered UrlRoute paths.</param>
        public static void DiscoverMvcAreaRoutes(this AreaRegistrationContext ctx, Assembly assembly, string basePath)
        {
            string namespacePrefix = null;

            foreach (string ns in ctx.Namespaces)
            {
                int wildcardPos = ns.LastIndexOf('*');
                if (wildcardPos != -1)
                {
                    namespacePrefix = ns.Substring(0, wildcardPos);
                }

                break; // TODO: handle multiple namespaces, for now break out after one
            }

            if (String.IsNullOrEmpty(namespacePrefix))
            {
                throw new ApplicationException("MVC area does not define any namespaces");
            }

            // Enumerate assembly for UrlRoute attributes.
            List<MapRouteParams> routeParams = GetRouteParamsFromAttributes(assembly, namespacePrefix, basePath);

            // Sort the routes based on the Order attribute.
            routeParams.Sort((a, b) => a.Order.CompareTo(b.Order));

            // Add the routes to the routes collection.
            foreach (MapRouteParams rd in routeParams)
            {
                ////Trace.TraceInformation(
                ////    "Adding route {{ Priority = {0}, Name = {1}, Path = {2}, Controller = {3}, Action = {4} }}",
                ////    rd.Order, rd.RouteName ?? "<null>", rd.Path, rd.ControllerName, rd.ActionName);

                // Controller and action is always the class/method that
                // the UrlRoute attribute was declared on (if you set
                // these as route parameters it will be overridden here).
                rd.Defaults["controller"] = rd.ControllerName;
                rd.Defaults["action"] = rd.ActionName;

                Route r = ctx.MapRoute(
                    rd.RouteName,
                    rd.Path,
                    new { controller = rd.ControllerName, action = rd.ActionName }, // rd.Defaults,
                    null, // rd.Constraints,
                    new string[] { rd.ControllerNamespace });

                if (!String.IsNullOrEmpty(rd.RouteName))
                {
                    r.SetRouteName(rd.RouteName);
                }
            }

        }

        /// <summary>
        /// Discover and add routes for .aspx web form pages in the specified
        /// virtual folder which declare the UrlPageRoute in the code behind
        /// class (derived from System.Web.UI.Page).
        /// </summary>
        /// <param name="routes">Route collection to add the discovered routes to.</param>
        /// <param name="virtualFolderPath">Virtual folder to search.</param>
        public static void DiscoverWebFormPageRoutes(this RouteCollection routes, string virtualFolderPath)
        {
            string rootPath = System.Web.HttpContext.Current.Server.MapPath(virtualFolderPath);

            foreach (string pageFile in Directory.EnumerateFiles(rootPath, "*.aspx"))
            {
                string vpath = PhysicalToVirtualPath(pageFile);
                Type pageType = BuildManager.GetCompiledType(vpath);

                if (pageType.IsSubclassOf(typeof(System.Web.UI.Page)))
                {
                    UrlPageRouteAttribute[] attribs = (UrlPageRouteAttribute[])pageType.GetCustomAttributes(typeof(UrlPageRouteAttribute), true);

                    foreach (UrlPageRouteAttribute attr in attribs)
                    {
                        string routeName;

                        if (!String.IsNullOrEmpty(attr.Name))
                        {
                            routeName = attr.Name;
                        }
                        else
                        {
                            routeName = vpath;
                        }

                        Route r = routes.MapPageRoute(routeName, attr.Path, vpath);

                        if (!String.IsNullOrEmpty(attr.Name))
                        {
                            r.SetRouteName(attr.Name);
                        }
                    }
                }
            }
        }

        private static string PhysicalToVirtualPath(string physicalPath)
        {
            string rootpath = System.Web.HttpContext.Current.Server.MapPath("~/");
            physicalPath = physicalPath.Replace(rootpath, "");
            physicalPath = physicalPath.Replace("\\", "/");
            return "~/" + physicalPath;
        }

        private static bool IsValidUrlPath(string path)
        {
            // TODO: switch to whitelist, there are probably many other chars which are invalid

            if (path.StartsWith("/") || path.EndsWith("/") || path.Contains("?"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Match this up with what is checked in IsValidUrlPath
        /// </summary>
        private const string InvalidUrlMessage = " must not start or end with '/' or contain '?'";

        private static string ConcatPaths(string path1, string path2)
        {
            if (String.IsNullOrEmpty(path1))
            {
                return path2;
            }
            else if (!String.IsNullOrEmpty(path2))
            {
                return path1 + "/" + path2;
            }
            else
            {
                return path1;
            }
        }

        /// <summary>
        /// Uses reflection to enumerate all Controller classes in the
        /// assembly and registers a route for each method declaring a
        /// UrlRoute attribute.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="namespacePrefix"></param>
        private static List<MapRouteParams> GetRouteParamsFromAttributes(Assembly a, string namespacePrefix, string basePath)
        {
            if (!String.IsNullOrEmpty(basePath) && !IsValidUrlPath(basePath))
            {
                throw new ArgumentException(String.Format("basePath argument {0}: path" + InvalidUrlMessage, basePath));
            }

            List<MapRouteParams> routeParams = new List<MapRouteParams>();

            var controllerClasses = a.GetTypes().Where(
                    t => t.IsClass
                    && t.Name.EndsWith("Controller")
                    && !t.IsAbstract
                    && t.IsSubclassOf(typeof(System.Web.Mvc.Controller))
                    && (String.IsNullOrEmpty(namespacePrefix) || t.Namespace.StartsWith(namespacePrefix)));

            foreach (Type controllerClass in controllerClasses)
            {
                string controllerBasePath = basePath;

                // Strip out the "Controller" suffix.
                string controllerName = controllerClass.Name.Substring(0, controllerClass.Name.Length - "Controller".Length);

                // Get the basepath defined on the controller if there is one.
                UrlRouteBaseAttribute controllerRouteBaseAttrib = controllerClass
                    .GetCustomAttributes(typeof(UrlRouteBaseAttribute), true)
                    .SingleOrDefault() as UrlRouteBaseAttribute;

                if (controllerRouteBaseAttrib != null)
                {
                    if (!IsValidUrlPath(controllerRouteBaseAttrib.BasePath))
                    {
                        throw new ApplicationException(String.Format(
                            "UrlRouteBase attribute \"{0}\" on controller class {1} (or its superclass): BasePath" + InvalidUrlMessage,
                            controllerRouteBaseAttrib.BasePath, controllerClass.FullName));
                    }

                    controllerBasePath = ConcatPaths(controllerBasePath, controllerRouteBaseAttrib.BasePath);
                }

                // Enumerate public methods on the controller class.
                foreach (MethodInfo methodInfo in controllerClass.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    // Enumerate UrlRoute attributes on the method.
                    foreach (UrlRouteAttribute routeAttrib in methodInfo.GetCustomAttributes(typeof(UrlRouteAttribute), false))
                    {
                        if (!IsValidUrlPath(routeAttrib.Path))
                        {
                            throw new ApplicationException(String.Format(
                                "UrlRoute attribute \"{0}\" on method {1} of controller class {2}: Path" + InvalidUrlMessage,
                                routeAttrib.Path, methodInfo.Name, controllerClass.FullName));
                        }

                        routeAttrib.Path = ConcatPaths(controllerBasePath, routeAttrib.Path);

                        // Add to list of routes.
                        routeParams.Add(new MapRouteParams()
                        {
                            RouteName = String.IsNullOrEmpty(routeAttrib.Name) ? null : routeAttrib.Name,
                            Path = routeAttrib.Path,
                            ControllerName = controllerName,
                            ActionName = methodInfo.Name,
                            Order = routeAttrib.Order,
                            Constraints = GetConstraints(methodInfo),
                            Defaults = GetDefaults(methodInfo),
                            ControllerNamespace = controllerClass.Namespace,
                        });
                    }
                }
            }

            return routeParams;
        }

        /// <summary>
        /// This was copied from System.Web.Mvc.RouteCollectionExtensions and
        /// modified slightly.  The original function declares the defaults
        /// and constraints parameters as object type, which causes the wrong
        /// overload of RouteValueDictionary to be invoked, causing values in
        /// the dictionaries not to be set properly.  The modified version
        /// declares these parameters as Dictionary&lt;string, object&gt;,
        /// fixing the problem.
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        private static Route MapRoute(RouteCollection routes, string name, string url, Dictionary<string, object> defaults, Dictionary<string, object> constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (constraints == null)
            {
                throw new ArgumentNullException("constraints");
            }

            Route route = new Route(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            route.DataTokens = new RouteValueDictionary();

            if (!String.IsNullOrEmpty(name))
            {
                route.SetRouteName(name);
            }

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }

        private static Dictionary<string, object> GetConstraints(MethodInfo mi)
        {
            Dictionary<string, object> constraints = new Dictionary<string, object>();

            foreach (UrlRouteParameterConstraintAttribute attrib in mi.GetCustomAttributes(typeof(UrlRouteParameterConstraintAttribute), true))
            {
                if (String.IsNullOrEmpty(attrib.Name))
                {
                    throw new ApplicationException(String.Format("UrlRouteParameterContraint attribute on {0}.{1} is missing the Name property.",
                        mi.DeclaringType.Name, mi.Name));
                }

                if (String.IsNullOrEmpty(attrib.Regex))
                {
                    throw new ApplicationException(String.Format("UrlRouteParameterContraint attribute on {0}.{1} is missing the RegEx property.",
                        mi.DeclaringType.Name, mi.Name));
                }

                constraints.Add(attrib.Name, attrib.Regex);
            }

            return constraints;
        }

        private static Dictionary<string, object> GetDefaults(MethodInfo mi)
        {
            Dictionary<string, object> defaults = new Dictionary<string, object>();

            foreach (UrlRouteParameterDefaultAttribute attrib in mi.GetCustomAttributes(typeof(UrlRouteParameterDefaultAttribute), true))
            {
                if (String.IsNullOrEmpty(attrib.Name))
                {
                    throw new ApplicationException(String.Format("UrlRouteParameterDefault attribute on {0}.{1} is missing the Name property.",
                        mi.DeclaringType.Name, mi.Name));
                }

                if (attrib.Value == null)
                {
                    throw new ApplicationException(String.Format("UrlRouteParameterDefault attribute on {0}.{1} is missing the Value property.",
                        mi.DeclaringType.Name, mi.Name));
                }

                defaults.Add(attrib.Name, attrib.Value);
            }

            return defaults;
        }

        class MapRouteParams
        {
            public int Order { get; set; }
            public string RouteName { get; set; }
            public string Path { get; set; }
            public string ControllerNamespace { get; set; }
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
            public Dictionary<string, object> Defaults { get; set; }
            public Dictionary<string, object> Constraints { get; set; }
        }
    }
}
