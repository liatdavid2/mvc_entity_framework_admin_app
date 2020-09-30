using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class UserController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        //
        // GET: /User/

        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" :

           "";

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var users = from s in db.users

                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {

                users = users.Where(s =>

               s.name.ToUpper().Contains(searchString.ToUpper()));

            }

            switch (sortOrder)
            {

                case "name_desc":

                    users = users.OrderByDescending(s => s.name);

                    break;

                case "Date":

                    users = users.OrderBy(s => s.name);

                    break;

                case "date_desc":

                    users = users.OrderByDescending(s => s.password);

                    break;

                default:

                    users = users.OrderBy(s => s.id);

                    break;

            }

            return View(users.ToList());

        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(users users)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            users users = db.users.Find(id);
            db.users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}