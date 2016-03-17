namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class CategorizarController : Controller
    {
        // GET: Upload
        [UrlRoute("Categorizar/{documentoId}")]
        public ActionResult Index(int documentoId)
        {
            var documento = new Documento { Id = documentoId };
            return PartialView(documento);
        }
    }
}