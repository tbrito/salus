using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Extensions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class UrlRouteBaseAttribute : Attribute
    {
        public string BasePath { get; set; }

        public UrlRouteBaseAttribute()
        {
        }

        public UrlRouteBaseAttribute(string basePath)
        {
            this.BasePath = basePath;
        }
    }
}
