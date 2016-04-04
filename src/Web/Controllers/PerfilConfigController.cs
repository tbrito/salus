namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class PerfilConfigController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        [UrlRoute("PerfilConfig/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var perfil = new Perfil { Id = id };
            return PartialView(perfil);
        }
    }
}