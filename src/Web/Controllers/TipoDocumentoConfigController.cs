namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class TipoDocumentoConfigController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}