namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class ConfiguracaoController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        [UrlRoute("Configuracao/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var perfil = new Configuracao { Id = id };
            return PartialView(perfil);
        }
    }
}