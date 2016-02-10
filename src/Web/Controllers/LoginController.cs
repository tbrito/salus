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
        public LoginController()
             : this(new UserManager<Usuario>(new UserStore<Usuario>(NHibernateSession.Current)))
        {

        }

        public LoginController(UserManager<Usuario> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<Usuario> UserManager { get; private set; }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public Usuario Login(LoginViewModel loginViewModel)
        {
            var usuarioEncontrado = this.UserManager.Find(loginViewModel.UserName, loginViewModel.Senha);
            return usuarioEncontrado;
        }
    }
}