namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.UI;
    using System.Web.Mvc;

    public class AreaConfigController : Controller
    {
        // GET: Editar
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Editar
        [UrlRoute("AreaConfig/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var areaViewModel = new AreaViewModel { Id = id };
            return PartialView(areaViewModel);
        }
    }
}