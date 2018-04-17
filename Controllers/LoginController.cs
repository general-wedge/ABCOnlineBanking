using ABCBankingApplication.Models;
using ABCBankingApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ABCBankingApplication.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return PartialView("_Login");
        }

        [HttpPost] 
        public ActionResult Register(UserViewModel model) {

            if(!ModelState.IsValid)
            {
                return View();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home", routeValues: new { loginFailed = true });
            }
            else
            {
                using(DatabaseEntities db = new DatabaseEntities())
                {
                    var user = db.Users.Where(u => u.email.Equals(model.Email) && u.password.Equals(model.Password)).FirstOrDefault();

                    if(user != null)
                    {
                        if(user.UserRole.user_role_name == "Admin")
                        {
                            Session["Admin"] = user.UserRole.user_role_name.ToString();
                            Session["Email"] = user.email.ToString();
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            Session["UserID"] = user.user_id.ToString();
                            Session["Email"] = user.email.ToString();
                            Session["FirstName"] = user.first_name.ToString();
                            Session["LastName"] = user.last_name.ToString();
                            Session["Address"] = "123 Fake Street";
                            return RedirectToAction("AccountsPage", "Accounts");
                        }
                    }
                    else
                    {
                        return RedirectToAction(actionName: "Index", controllerName: "Home", routeValues: new { loginFailed = true });
                    }
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}