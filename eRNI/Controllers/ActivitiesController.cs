using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eRNI.Models;

namespace eRNI.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var tblActions = db.tblActions.Include(a => a.project);
            return View(tblActions.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.tblActions.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create(int id)
        {
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == id).ToList(), "projectID", "projectSapNo");

            return View();
        }

        // POST: Activities/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "actionID,actionDate,actionDescription,projectID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.tblActions.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = activity.projectID });
            }

            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", activity.projectID);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.tblActions.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == activity.projectID).ToList(), "projectID", "projectSapNo");

            return View(activity);
        }

        // POST: Activities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "actionID,actionDate,actionDescription,projectID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = activity.projectID });
            }
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", activity.projectID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.tblActions.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }

            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.tblActions.Find(id);
            db.tblActions.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = activity.projectID });
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
