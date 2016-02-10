namespace Web
{
    using System.Web;
    using Modules;

    public class HttpModulesInitializer
    {
        public static void Execute(HttpApplication mvcApplication)
        {
            Init(mvcApplication, new NHibernateHttpModule());
        }

        private static void Init(HttpApplication mvcApplication, IHttpModule module)
        {
            module.Init(mvcApplication);
        }
    }
}