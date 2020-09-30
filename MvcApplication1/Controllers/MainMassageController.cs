using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class MainMassageController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        //
        // GET: /MainMassage/

        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" :

           "";

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var Messages = from m in db.message

                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {

                Messages = Messages.Where(m =>

               m.question.ToUpper().Contains(searchString.ToUpper())

                ||

               m.name.ToUpper().Contains(searchString.ToUpper()));

            }

            switch (sortOrder)
            {

                case "name_desc":

                    Messages = Messages.OrderByDescending(s => s.question);

                    break;

                case "Date":

                    Messages = Messages.OrderBy(s => s.name);

                    break;

                case "date_desc":

                    Messages = Messages.OrderByDescending(s => s.type);

                    break;

                default:

                    Messages = Messages.OrderBy(s => s.id);

                    break;

            }

            return View(Messages.ToList());

        }

        //
        // GET: /MainMassage/Details/5

        public ActionResult Details(int id = 0)
        {
            message Message = db.message.Find(id);
            if (Message == null)
            {
                return HttpNotFound();
            }
            return View(Message);
        }

        //
        // GET: /MainMassage/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MainMassage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(message Message)
        {
            if (ModelState.IsValid)
            {
                db.message.Add(Message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Message);
        }

        //
        // GET: /MainMassage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            message Message = db.message.Find(id);
            if (Message == null)
            {
                return HttpNotFound();
            }
            return View(Message);
        }

        //
        // POST: /MainMassage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(message Message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Message);
        }

        //
        // GET: /MainMassage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            message Message = db.message.Find(id);
            if (Message == null)
            {
                return HttpNotFound();
            }
            return View(Message);
        }

        //
        // POST: /MainMassage/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            message message = db.message.Find(id);
            db.message.Remove(message);
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