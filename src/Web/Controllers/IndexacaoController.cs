namespace Web.Controllers
{
    using System.Web.Mvc;

    public class IndexacaoController : Controller
    {
        // GET: Upload
        public ActionResult Categorizar(string documentoId)
        {
            return PartialView();
        }
    }
}