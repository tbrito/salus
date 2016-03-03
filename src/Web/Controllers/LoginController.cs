namespace Web.Controllers
{
    using System.Web.Mvc;

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