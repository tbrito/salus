﻿namespace Web.Controllers
{
    using System.Web.Mvc;
    
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}