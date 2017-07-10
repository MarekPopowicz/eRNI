using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eRNI.Models;
using eRNI.ViewModels;

namespace eRNI.Controllers
{
    public class ProjectsController : BaseController
    {
       

        // GET: Projects
        public ActionResult Index()
        {
            var tblProjects = db.tblProjects.Include(p => p.projectCategory);
            return View(tblProjects.ToList().OrderByDescending(k=>k.projectID));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "projectID," +
                                                    "projectAdditionalInfo," +
                                                    "projectInflow," +
                                                    "projectStatus," +
                                                    "projectLastActivity," +
                                                    "projectLeader," +
                                                    "projectManager," +
                                                    "projectSapNo," +
                                                    "projectTask," +
                                                    "projectPriority," +
                                                    "projectCategoryID")] Project project)
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
        public ActionResult Edit([Bind(Include = "projectID," +
                                                "projectAdditionalInfo," +
                                                "projectInflow," +
                                                "projectStatus," +
                                                "projectLastActivity," +
                                                "projectLeader," +
                                                "projectManager," +
                                                "projectSapNo," +
                                                "projectTask," +
                                                "projectPriority," +
                                                "projectCategoryID")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = project.projectID });
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
            var action = db.tblActions.Where(p => p.projectID == id).OrderByDescending(k=>k.actionDate);
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


              public ActionResult Label(int id)
        {
            Project project = db.tblProjects.Find(id);
            List<Ownership> ownership = db.tblOwnerships.Where(o => o.tblLocalizations.projectID == id).ToList();
            
            ProjectLabel projectLabel = new ProjectLabel();
            if(projectLabel.IsAnyOwner(ownership))
            {
                List<Localization> projectLocalizations = db.tblLocalizations.Where(l => l.projectID == id).ToList();

                List<Owner> projectOwners = new List<Owner>();
                foreach (var ownerItem in ownership)
                {
                    Owner owner = db.tblOwners.Where(o => o.ownerID == ownerItem.ownerID).FirstOrDefault();
                    projectOwners.Add(owner);
                }

                projectLabel.SetProjectLabelData(project, projectLocalizations, projectOwners, ownership);
                return View(projectLabel);
            }
            return RedirectToAction("Details", "Projects", new { id });
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
