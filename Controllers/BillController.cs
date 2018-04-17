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
        public ActionResult BillsPage()
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
                        bill.BalanceOwing = b.balance_owing;
                        bill.BillType = b.bill_type;
                        bills.Add(bill);
                    }
                }
            }
            ViewBag.CustomerName = Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + "'s";
            ViewBag.Bills = bills;
            return View();
        }
    }
}