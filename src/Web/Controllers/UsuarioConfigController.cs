namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.UI;
    using System.Web.Mvc;

    public class UsuarioConfigController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        [UrlRoute("UsuarioConfig/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var usuarioViewModel = new UsuarioViewModel { Id = id };
            return PartialView(usuarioViewModel);
        }
    }
}