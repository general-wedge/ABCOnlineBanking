using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABCBankingApplication.Models;
using ABCBankingApplication.Filters;

namespace ABCBankingApplication.Controllers
{
    public class AdminController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Admin
        [AdminAuthFilter]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.UserRole).ToList();
            ViewBag.Users = users;
            return View();
        }

        // GET: Admin/Create
        [AdminAuthFilter]
        public ActionResult Create()
        {
            ViewBag.user_role_id = new SelectList(db.UserRoles, "user_role_id", "user_role_name");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthFilter]
        public async Task<ActionResult> Create([Bind(Include = "user_id,account_number,password,salt,email,first_name,last_name,date_of_birth,user_role_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_role_id = new SelectList(db.UserRoles, "user_role_id", "user_role_name", user.user_role_id);
            return View(user);
        }

        // GET: Admin/Edit/5
        [AdminAuthFilter]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_role_id = new SelectList(db.UserRoles, "user_role_id", "user_role_name", user.user_role_id);
            return View(user);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthFilter]
        public async Task<ActionResult> Edit([Bind(Include = "user_id,account_number,password,salt,email,first_name,last_name,date_of_birth,user_role_id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_role_id = new SelectList(db.UserRoles, "user_role_id", "user_role_name", user.user_role_id);
            return View(user);
        }

        // GET: Admin/Delete/5
        [AdminAuthFilter]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminAuthFilter]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            List<UserAccount> accounts = await db.UserAccounts.Where(x => x.user_id == id).ToListAsync();
            foreach(var account in accounts)
            {
                db.UserAccounts.Remove(account);
                await db.SaveChangesAsync();
            }
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
