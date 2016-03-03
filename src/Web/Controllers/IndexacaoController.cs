namespace Web.Controllers
{
    using Salus.Model.Entidades;
    using System.Collections.Generic;
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