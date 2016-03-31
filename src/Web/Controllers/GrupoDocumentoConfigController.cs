namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System;
    using System.Web.Mvc;

    public class GrupoDocumentoConfigController : Controller
    {
        // GET: Editar
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Editar
        [UrlRoute("GrupoDocumentoConfig/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var tipoDocumento = new TipoDocumento { Id = id };
            return PartialView(tipoDocumento);
        }
    }
}