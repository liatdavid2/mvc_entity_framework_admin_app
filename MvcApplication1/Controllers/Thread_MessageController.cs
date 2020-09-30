using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class Thread_MessageController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        //
        // GET: /Thread_Message/

        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" :

           "";

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var threads = from s in db.Thread

                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {

                threads = threads.Where(s =>

               s.answer.ToUpper().Contains(searchString.ToUpper())

                ||

               s.posterName.ToUpper().Contains(searchString.ToUpper()));

            }

            switch (sortOrder)
            {

                case "name_desc":

                    threads = threads.OrderByDescending(s => s.answer);

                    break;

                case "Date":

                    threads = threads.OrderBy(s => s.posterName);

                    break;

                case "date_desc":

                    threads = threads.OrderByDescending(s => s.type);

                    break;

                default:

                    threads = threads.OrderBy(s => s.id);

                    break;

            }

            return View(threads.ToList());

        }

        //
        // GET: /Thread_Message/Details/5

        public ActionResult Details(int id = 0)
        {
            Thread thread = db.Thread.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        //
        // GET: /Thread_Message/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Thread_Message/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Thread.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thread);
        }

        //
        // GET: /Thread_Message/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Thread thread = db.Thread.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        //
        // POST: /Thread_Message/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Thread thread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thread);
        }

        //
        // GET: /Thread_Message/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Thread thread = db.Thread.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        //
        // POST: /Thread_Message/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Thread.Find(id);
            db.Thread.Remove(thread);
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