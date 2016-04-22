namespace Web.Controllers
{
    using Extensions;
    using Filters;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class ViewController : Controller
    {
        // GET: Upload
        [UrlRoute("View/{documentoId}")]
        [PodeAcessarDocumento]
        public ActionResult Index(int documentoId)
        {
            var documento = new Documento { Id = documentoId };
            return PartialView(documento);
        }
    }
}