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
    public class RegistrationController : Controller
    {
        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Request");
            }

            using(DatabaseEntities db = new DatabaseEntities())
            {
                User newUser = new User();
                newUser.first_name = model.FirstName;
                newUser.last_name = model.LastName;
                newUser.date_of_birth = model.DOB;
                newUser.email = model.Email;
                newUser.password = model.Password;
                newUser.user_role_id = 2; // Customer Role ID

                db.Users.Add(newUser);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { redirectToLogin = true });
        }
    }
}