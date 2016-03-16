namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.Entidades;
    using System.Web.Mvc;

    public class IndexacaoController : Controller
    {
        // GET: Upload
        [UrlRoute("Indexacao/Categorizar/{documentoId}")]
        public ActionResult Categorizar(int documentoId)
        {
            var documento = new Documento { Id = documentoId };

            return PartialView(documento);
        }
    }
}