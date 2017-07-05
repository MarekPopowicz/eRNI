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
    public class ProjectsController : BaseController
    {
       

        // GET: Projects
        public ActionResult Index()
        {
            var tblProjects = db.tblProjects.Include(p => p.projectCategory);
            return View(tblProjects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            Session.Timeout = 60;
            Session["projectID"] = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

                       
            Project project = db.tblProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName");
            return View();
        }

        // POST: Projects/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "projectID,projectAdditionalInfo,projectInflow,projectStatus,projectLastActivity,projectLeader,projectManager,projectSapNo,projectTask,projectPriority,projectCategoryID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.tblProjects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName", project.projectCategoryID);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.tblProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName", project.projectCategoryID);
            return View(project);
        }

        // POST: Projects/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "projectID,projectAdditionalInfo,projectInflow,projectStatus,projectLastActivity,projectLeader,projectManager,projectSapNo,projectTask,projectPriority,projectCategoryID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName", project.projectCategoryID);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.tblProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.tblProjects.Find(id);
            db.tblProjects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProjectLocalizations(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var localization = db.tblLocalizations.Where(p => p.projectID == id);
            if (localization == null) RedirectToAction("Details", new { id });
            return PartialView("_ViewLocalizationsOfProject", localization.ToList());
        }

        public ActionResult ProjectActivities(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var action = db.tblActions.Where(p => p.projectID == id);
            if (action == null) RedirectToAction("Details", new { id });
            return PartialView("_ViewActivitiesOfProject", action.ToList());
        }

        public ActionResult ProjectInvoices(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var invoice = db.tblInvoices.Where(p => p.projectID == id);
            if (invoice == null) RedirectToAction("Details", new { id });
            return PartialView("_ViewInvoicesOfProject", invoice.ToList());
        }

        public ActionResult ProjectPropertyDocuments(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var propertyDocuments = db.tblPropertyDocuments.Where(p => p.projectID == id);
            if (propertyDocuments == null) RedirectToAction("Details", new { id });
            return PartialView("_ViewPropertyDocumentsOfProject", propertyDocuments.ToList());
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
