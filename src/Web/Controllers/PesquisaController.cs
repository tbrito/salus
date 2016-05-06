namespace Web.Controllers
{
    using Extensions;
    using Salus.Model.UI;
    using System.Web.Mvc;

    public class PesquisaController : Controller
    {
        [UrlRoute("Pesquisa/Resultado/{termo}")]
        public ActionResult Resultado(string termo)
        {
            var pesquisaViewModel = new PesquisaViewModel { Texto = termo };
            return PartialView(pesquisaViewModel);
        }
    }
}