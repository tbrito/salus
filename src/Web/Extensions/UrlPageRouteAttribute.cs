﻿using System;

namespace Web.Extensions
{
    /// <summary>
    /// Assigns a URL route to web forms page class (derived from System.Web.UI.Page).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UrlPageRouteAttribute : Attribute
    {
        /// <summary>
        /// Optional name of the route.  Route names must be unique per route.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path of the URL route.  This is relative to the root of the web site.
        /// Do not append a "/" prefix.  Specify empty string for the root page.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Optional order in which to add the route (default is 0).  Routes
        /// with lower order values will be added before those with higher.
        /// Routes that have the same order value will be added in undefined
        /// order with respect to each other.
        /// </summary>
        public int Order { get; set; }
    }
}
