namespace Web.Controllers
{
    using System.Web.Mvc;

    public class AcessoFuncionalidadeConfig : Controller
    {
        // GET: Editar
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}