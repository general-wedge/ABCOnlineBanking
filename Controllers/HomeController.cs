using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABCBankingApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? loginFailed, bool? redirectToLogin)
        {
            if (redirectToLogin == true)
            {
                ViewBag.Login = "Login into your newly created account!";
            }

            if (loginFailed == true)
            {
                ViewBag.Failed = "The email or password provided does not match any records we have. Please try again!";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return PartialView("_Login");
        }

        public ActionResult Register()
        {
            return PartialView("_Register");
        }
    }
}