using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class DictProjectsController : BaseController
    {
        // GET: DictProjects
        [Authorize]
        public ActionResult Index()
        {
            var model = db.tblProjectCategories.OrderBy(o => o.projectCategryName);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<ProjectCategory> projects = new List<ProjectCategory>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                projects = db.tblProjectCategories.OrderBy(o => o.projectCategryName).ToList();
            }
            else
            {
                projects = db.tblProjectCategories.Where(s => s.projectCategryName.Contains(searchTerm)).OrderBy(o => o.projectCategryName).ToList();
            }
            return View(projects);
        }


        // GET: DictProjects/Create
        [Authorize]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: DictProjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "projectCategoryID, projectCategryName")] ProjectCategory project)
        {
            if (ModelState.IsValid)
            {
                db.tblProjectCategories.Add(project);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(project, JsonRequestBehavior.AllowGet);
        }

        // GET: DictProjects/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectCategory project = db.tblProjectCategories.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", project);
        }

        // POST: DictProjects/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "projectCategoryID, projectCategryName")] ProjectCategory project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", project);
        }


        // GET: DictProjects/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory project = db.tblProjectCategories.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", project);
        }

        // POST: DictProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectCategory project = db.tblProjectCategories.Find(id);
            db.tblProjectCategories.Remove(project);
            db.SaveChanges();

            return Json(new { success = true });
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
