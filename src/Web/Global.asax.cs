namespace Web
{
    using Salus.Infra;
    using Salus.Infra.Logs;
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    public class MvcApplication : HttpApplication
    {
        public override void Init()
        {
            HttpModulesInitializer.Execute(this);
            base.Init();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         
            Aplicacao.Boot(Server.MapPath("bin"));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Log.App.Error("Erro ao processar requisicao.", exception);
            Server.ClearError();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }

        }
    }
}
