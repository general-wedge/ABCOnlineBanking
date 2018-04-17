using ABCBankingApplication.Filters;
using ABCBankingApplication.Models;
using ABCBankingApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABCBankingApplication.Controllers
{
    public class AccountSettingsController : Controller
    {
        // GET: AccountSettings
        public ActionResult AccountSettingsPage(Boolean? wrongPw, Boolean? pwSuccess, Boolean? activated, Boolean? addressChanged)
        {
            if(wrongPw == true)
            {
                ViewBag.WrongPw = true;
            }

            if(pwSuccess == true)
            {
                ViewBag.PwSuccess = true;
            }
            
            if(pwSuccess == false)
            {
                ViewBag.PwSuccess = false;
            }

            if(activated == true)
            {
                ViewBag.Activated = true;
            }

            if(addressChanged == true)
            {
                ViewBag.AddressChanged = true;
            }
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            UserViewModel userVm = new UserViewModel();
            using(DatabaseEntities db = new DatabaseEntities())
            {
                var user = db.Users.SingleOrDefault(x => x.user_id == userId);

                userVm.FirstName = user.first_name;
                userVm.LastName = user.last_name;
                userVm.Address = Session["Address"].ToString();
                userVm.Email = user.email;
                userVm.DOB = user.date_of_birth;
            }
            ViewBag.Customer = userVm;
            ViewBag.CustomerName = Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + "'s";
            return View();
        }

        [AuthorizationFilter]
        public ActionResult ChangeAddressPartial()
        {
            return PartialView("_ChangeAddress");
        }

        [AuthorizationFilter]
        public ActionResult ChangePasswordPartial()
        {
            return PartialView("_ChangePassword");
        }

        [AuthorizationFilter]
        public ActionResult ActivatePartial()
        {
            return PartialView("_ActivateCard");
        }

        [AuthorizationFilter]
        [HttpPost]
        public ActionResult ChangeAddress(AddressViewModel address)
        {
            Session["Address"] = address.NewAddress;
            return RedirectToAction("AccountSettingsPage", routeValues: new { addressChanged = true });
        }

        [AuthorizationFilter]
        [HttpPost]
        public ActionResult ActivateCard(ActivateCardViewModel cardNumber)
        {
            
            return RedirectToAction(actionName: "AccountSettingsPage", routeValues: new { activated = true });
        }

        [AuthorizationFilter]
        [HttpPost]
        public ActionResult ChangePassword(PasswordViewModel password)
        {
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var user = db.Users.SingleOrDefault(x => x.user_id == userId);

                if (password.NewPassword != password.ConfirmPassword)
                {
                    return RedirectToAction(actionName: "AccountSettingsPage", routeValues: new { pwSuccess = false });
                }

                if (password.OldPassword != user.password)
                { 
                    return RedirectToAction(actionName: "AccountSettingsPage", routeValues: new { wrongPw = true });
                }
 
                db.Users.Attach(user);
                user.password = password.NewPassword;
                db.SaveChanges();

            }
            return RedirectToAction(actionName: "AccountSettingsPage", routeValues: new { pwSuccess = true });
        }
    }
}