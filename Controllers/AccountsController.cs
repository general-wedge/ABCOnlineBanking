using ABCBankingApplication.Filters;
using ABCBankingApplication.Models;
using ABCBankingApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABCBankingApplication.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        [AuthorizationFilter]
        public ActionResult AccountsPage()
        {
            // NEVER WRITE YOUR METHODS LIKE THIS ---- THIS IS THE N+1 PROBLEM. IT WILL WORK FOR THE PRESENTATION
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            List<ChequeingAccount> allChequeingAccts = new List<ChequeingAccount>();
            List<SavingsAccount> allSavingsAccts = new List<SavingsAccount>();
            BankAccountViewModel viewModel = new BankAccountViewModel();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var bankAccounts = db.UserAccounts.Where(x => x.user_id == userId).ToList();
                viewModel.CustomerName = Session["FirstName"].ToString() + " " + Session["LastName"].ToString() + "'s";
                foreach (var acct in bankAccounts)
                {
                    foreach(var cheq in acct.BankAccount.ChequeingAccounts)
                    {
                        allChequeingAccts.Add(cheq);
                    }

                    foreach (var savings in acct.BankAccount.SavingsAccounts)
                    {
                        allSavingsAccts.Add(savings);
                    }
                }
            }
            viewModel.ChequeingAccts = allChequeingAccts;
            viewModel.SavingsAccts = allSavingsAccts;
            ViewBag.Accounts = viewModel;
            return View();
        }

        [AuthorizationFilter]
        public ActionResult CreateSavingsPartial()
        {
            return PartialView("_CreateSavings");
        }

        public ActionResult CreateChequeingPartial()
        {
            return PartialView("_CreateChequeing");
        }

        [AuthorizationFilter]
        public ActionResult CreateChequeing()
        {
            Random rnd = new Random();

            BankAccount stuAccount = new BankAccount();
            stuAccount.balance = 0;
            ChequeingAccount stuChequeAccount = new ChequeingAccount();
            stuChequeAccount.account_number = rnd.Next(10000, 20000);
            UserAccount userAccts = new UserAccount();

            using(DatabaseEntities db = new DatabaseEntities())
            {
                // Add Bank Account Object to the DB
                db.BankAccounts.Add(stuAccount);
                db.SaveChanges();
                int bankAcctId = stuAccount.account_id; // Get the ID of the enwly created Bank Account

                // Add Chequeing Account to DB and assign it the newly created Bank Account Id
                stuChequeAccount.account_id = bankAcctId;
                db.ChequeingAccounts.Add(stuChequeAccount);
                db.SaveChanges();

                // Map relationship between User and Account to the DB
                userAccts.account_id = bankAcctId;
                userAccts.user_id = Convert.ToInt32(Session["UserId"].ToString());
                db.UserAccounts.Add(userAccts);
                db.SaveChanges();
            }

            return RedirectToAction("AccountsPage");
            
        }


        [AuthorizationFilter]
        public ActionResult CreateSavings()
        {
            Random rnd = new Random();

            BankAccount account = new BankAccount();
            account.balance = 0;
            SavingsAccount savingsAccount = new SavingsAccount();
            savingsAccount.account_number = rnd.Next(10000, 20000);
            UserAccount userAccts = new UserAccount();

            using (DatabaseEntities db = new DatabaseEntities())
            {
                // Add Bank Account Object to the DB
                db.BankAccounts.Add(account);
                db.SaveChanges();
                int bankAcctId = account.account_id; // Get the ID of the enwly created Bank Account

                // Add Chequeing Account to DB and assign it the newly created Bank Account Id
                savingsAccount.account_id = bankAcctId;
                db.SavingsAccounts.Add(savingsAccount);
                db.SaveChanges();

                // Map relationship between User and Account to the DB
                userAccts.account_id = bankAcctId;
                userAccts.user_id = Convert.ToInt32(Session["UserId"].ToString());
                db.UserAccounts.Add(userAccts);
                db.SaveChanges();
            }

            return RedirectToAction("AccountsPage");

        }

        [AuthorizationFilter]
        public ActionResult Transfer()
        {
            return View();
        }

        [AuthorizationFilter]
        public ActionResult ViewStatement()
        {
            return View();
        }
    }
}