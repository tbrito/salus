namespace Web.Controllers
{
    using System.Web.Mvc;

    public class DocumentoController : Controller
    {
        // GET: Upload
        public ActionResult Novo()
        {
            return PartialView();
        }
    }
}