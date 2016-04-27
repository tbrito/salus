namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class EditarPerfilController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}