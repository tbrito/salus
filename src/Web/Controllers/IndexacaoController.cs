namespace Web.Controllers
{
    using Extensions;
    using System.Web.Mvc;

    public class IndexacaoController : Controller
    {
        // GET: Upload
        [UrlRoute("Indexacao/Categorizar/{documentoId}")]
        public ActionResult Categorizar(string documentoId)
        {
            return PartialView();
        }
    }
}