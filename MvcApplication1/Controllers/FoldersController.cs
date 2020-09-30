using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class FoldersController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        //
        // GET: /Folders/

        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" :

           "";

            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var user_Folders = from s in db.User_Folder

                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {

                user_Folders = user_Folders.Where(s =>

               s.FolderName.ToUpper().Contains(searchString.ToUpper())

                ||

               s.UserName.ToUpper().Contains(searchString.ToUpper()));

            }

            switch (sortOrder)
            {

                case "name_desc":

                    user_Folders = user_Folders.OrderByDescending(s => s.FolderName);

                    break;

                case "Date":

                    user_Folders = user_Folders.OrderBy(s => s.UserName);

                    break;

                case "date_desc":

                    user_Folders = user_Folders.OrderByDescending(s => s.ForumId);

                    break;

                default:

                    user_Folders = user_Folders.OrderBy(s => s.FolderName);

                    break;

            }

            return View(user_Folders.ToList());

        }

        //
        // GET: /Folders/Details/5

        public ActionResult Details(int id = 0)
        {
            User_Folder user_folder = db.User_Folder.Find(id);
            if (user_folder == null)
            {
                return HttpNotFound();
            }
            return View(user_folder);
        }

        //
        // GET: /Folders/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Folders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User_Folder user_folder)
        {
            if (ModelState.IsValid)
            {
                db.User_Folder.Add(user_folder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_folder);
        }

        //
        // GET: /Folders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User_Folder user_folder = db.User_Folder.Find(id);
            if (user_folder == null)
            {
                return HttpNotFound();
            }
            return View(user_folder);
        }

        //
        // POST: /Folders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User_Folder user_folder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_folder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_folder);
        }

        //
        // GET: /Folders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User_Folder user_folder = db.User_Folder.Find(id);
            if (user_folder == null)
            {
                return HttpNotFound();
            }
            return View(user_folder);
        }

        //
        // POST: /Folders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_Folder user_folder = db.User_Folder.Find(id);
            db.User_Folder.Remove(user_folder);
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