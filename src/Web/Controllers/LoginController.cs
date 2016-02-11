using Salus.Infra.UI;
using System.Web.Mvc;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}