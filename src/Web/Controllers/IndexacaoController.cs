namespace Web.Controllers
{
    using MvcContrib.Routing;
    using System.Web.Mvc;

    public class IndexacaoController : Controller
    {
        // GET: Upload
        [UrlRoute(Path = "Indexacao/Categorizar/{documentoId}")]
        public ActionResult Categorizar(string documentoId)
        {
            return PartialView();
        }
    }
}