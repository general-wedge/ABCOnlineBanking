using ABCBankingApplication.Models;
using ABCBankingApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABCBankingApplication.Controllers
{
    public class BillController : Controller
    {
        // GET: Bill
        public ActionResult BillsPage(bool? overPaid, bool? alreadyPaid, bool? success)
        {
            var userId = Convert.ToInt32(Session["UserId"].ToString());
            List<BillViewModel> bills = new List<BillViewModel>();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var accts = db.UserAccounts.Where(x => x.user_id == userId).ToList();
                foreach(var acct in accts)
                {
                    foreach(var b in acct.BankAccount.Bills)
                    {
                        BillViewModel bill = new BillViewModel();
                        bill.BillId = b.bill_id;
                        bill.BalanceOwing = b.balance_owing;
                        bill.BillType = b.bill_type;
                        bills.Add(bill);
                    }
                }
            }

            if(overPaid != false)
                ViewBag.OverPaid = overPaid;

            if(alreadyPaid != false)
                ViewBag.Paid = alreadyPaid;

            if (success != false)
                ViewBag.Success = success;

            ViewBag.CustomerName = Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + "'s";
            ViewBag.Bills = bills;
            return View();
        }

        [HttpPost]
        public ActionResult PayBill(int billId, string Payment)
        {
            long payment = Convert.ToInt32(Payment);
            bool overPaid = false;
            bool alreadyPaid = false;
            bool success = false;
            using(DatabaseEntities db = new DatabaseEntities())
            {
                var bill = db.Bills.Where(x => x.bill_id == billId).SingleOrDefault();

                if (bill.balance_owing == 0)
                {
                    alreadyPaid = true;
                }

                if (bill.balance_owing >= payment)
                {
                    db.Bills.Attach(bill);
                    bill.balance_owing = bill.balance_owing - payment;
                    db.SaveChanges();
                    success = true;
                }
                else
                {
                    overPaid = true;
                }

            }
            return RedirectToAction(actionName: "BillsPage", routeValues: new { overPaid,  alreadyPaid, success });
        }
    }
}