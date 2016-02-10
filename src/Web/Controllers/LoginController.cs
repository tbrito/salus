using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;
using Salus.Infra.UI;
using Salus.Model.Entidades;
using SharpArch.NHibernate;
using System.Web.Mvc;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public Usuario Login(LoginViewModel loginViewModel)
        {
            return null;
        }
    }
}