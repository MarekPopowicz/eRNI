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
using Microsoft.AspNet.Identity.Owin;
using Rotativa.MVC;

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

       

        [HttpPost]
        public ActionResult FindProject(string searchTerm)
        {
            if (searchTerm == "") return RedirectToAction("Index");

            var project = db.tblProjects.Where(p => p.projectAdditionalInfo.Contains(searchTerm));
            if (project == null )
            {
                return RedirectToAction("Index");
            }
                ViewBag.searchTerm = searchTerm;
                return View(project.ToList());
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
        [Authorize]
        public ActionResult Create()
        {
           
            ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName");
            ViewBag.projectLeader = SetUserList();
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
            ViewBag.projectLeader = SetUserList();
            return View(project);
        }

         private List<SelectListItem> SetUserList()
        {
            ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            List<SelectListItem> userList = new List<SelectListItem>();
            foreach (var user in UserManager.Users)
            {
                userList.Add(new SelectListItem() { Value = user.UserName, Text = user.UserName });
            }


            return userList.OrderBy(u => u.Text).ToList();
        }



        // GET: Projects/Edit/5
        [Authorize]
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

            ViewBag.projectLeader = new SelectList(SetUserList(), "Value", "Text", project.projectLeader);

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
                if(project.projectStatus == Status.Wykonany || project.projectStatus == Status.Przekazanie || project.projectStatus == Status.Rezygnacja)
                {
                    bool status = CheckStatus(project.projectID);
                    if (!status)
                    {
                        project.projectClosed = null;
                        ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName", project.projectCategoryID);
                        ViewBag.projectLeader = SetUserList();
                        TempData["Message"] = "Nie można zamknąć projektu ponieważ istnieją nieruchomości w toku regulacji.";
                        return View(project);
                    }
                   else
                    {
                        project.projectClosed = DateTime.Now.Date;
                    }
                }
                else
                {
                    bool status = CheckStatus(project.projectID);
                    if (status)
                    {
                        
                        ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName", project.projectCategoryID);
                        ViewBag.projectLeader = SetUserList();
                        TempData["Message"] = "Nie można otworzyć projektu ponieważ nie istnieją nieruchomości w toku regulacji.";
                        return View(project);
                    }
                    else
                    {
                        project.projectClosed = null;
                    }
                }
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = project.projectID });
            }
            ViewBag.projectCategoryID = new SelectList(db.tblProjectCategories, "projectCategoryID", "projectCategryName", project.projectCategoryID);
            ViewBag.projectLeader = SetUserList();
            return View(project);
        }

        private bool CheckStatus(int id)
        {
            var localizationStatus = db.tblLocalizations.Where(p => p.projectID == id).ToList();

            if(localizationStatus.Count==0) return false;

            foreach (var status in localizationStatus)
            {
                if( status.localizationRegulationStatus == Status.Przygotowanie || 
                    status.localizationRegulationStatus == Status.Realizacja || 
                    status.localizationRegulationStatus == Status.Zawieszenie )
                {
                    return false;
                }
            }
            return true;
        }

        // GET: Projects/Delete/5
        [Authorize]
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
            ViewBag.projectID = id;
            return PartialView("_ViewActivitiesOfProject", action.ToList());
        }

        [Authorize]
        public ActionResult PrintActivities(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var action = db.tblActions.Where(p => p.projectID == id).OrderByDescending(k => k.actionDate);
            ViewBag.projectID = id;
            return new ViewAsPdf("PdfViewActivitiesOfProject", action.ToList());
        }

        public ActionResult ProjectInvoices(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var invoice = db.tblInvoices.Where(p => p.projectID == id);
            if (invoice == null) RedirectToAction("Details", new { id });
            ViewBag.projectID = id;
            return PartialView("_ViewInvoicesOfProject", invoice.ToList());
        }

        [Authorize]
        public ActionResult PrintInvoices(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var invoice = db.tblInvoices.Where(p => p.projectID == id);
            ViewBag.projectID = id;
            return new ViewAsPdf("PdfViewInvoicesOfProject", invoice.ToList());
        }

        public ActionResult ProjectPropertyDocuments(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var propertyDocuments = db.tblPropertyDocuments.Where(p => p.projectID == id);
            if (propertyDocuments == null) RedirectToAction("Details", new { id });
            ViewBag.projectID = id;
            return PartialView("_ViewPropertyDocumentsOfProject", propertyDocuments.ToList());
        }

        [Authorize]
        public ActionResult PrintPropertyDocuments(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var propertyDocuments = db.tblPropertyDocuments.Where(p => p.projectID == id);
            ViewBag.projectID = id;
            return new ViewAsPdf("PdfViewPropertyDocumentsOfProject", propertyDocuments.ToList());
        }

        public ActionResult ProjectKeyDocuments(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var keyDocuments = db.tblKeyDocuments.Where(d => d.projectID == id);
            if (keyDocuments == null) RedirectToAction("Details", new { id });
            ViewBag.projectID = id;
            return PartialView("_ViewKeyDocumentsOfProject", keyDocuments.ToList());
        }

        [Authorize]
        public ActionResult PrintKeyDocuments(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var keyDocuments = db.tblKeyDocuments.Where(d => d.projectID == id);
            ViewBag.projectID = id;
            return new ViewAsPdf("PdfViewKeyDocumentsOfProject", keyDocuments.ToList());
        }

        public ActionResult ProjectCorrespondence(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var Correspondence = db.tblProjectCorrespondence.Where(d => d.projectID == id).OrderByDescending(o => o.projectCorrespondenceDate);
            if (Correspondence == null) RedirectToAction("Details", new { id });
            ViewBag.projectID = id;
            return PartialView("_ViewCorrespondenceOfProject", Correspondence.ToList());
        }

        [Authorize]
        public ActionResult PrintCorrespondence(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var Correspondence = db.tblProjectCorrespondence.Where(d => d.projectID == id).OrderByDescending(o => o.projectCorrespondenceDate);
            ViewBag.projectID = id;
            return new ViewAsPdf("PdfViewCorrespondenceOfProject", Correspondence.ToList());
        }



        [Authorize]
        public ActionResult Label(int id)
        {
            Project project = db.tblProjects.Find(id);
            List<Ownership> ownership = db.tblOwnerships.Where(o => o.tblLocalizations.projectID == id).ToList();
            
            ProjectLabel projectLabel = new ProjectLabel();

            if(projectLabel.IsAnyOwner(ownership))
            {
                List<Localization> projectLocalizations = new List<Localization>();
                List<Owner> projectOwners = new List<Owner>();

                foreach (var ownerItem in ownership)
                {
                    Localization localization = db.tblLocalizations.Where(l => l.localizationID == ownerItem.localizationID).FirstOrDefault();
                    Owner owner = db.tblOwners.Where(o => o.ownerID == ownerItem.ownerID).FirstOrDefault();
                    projectOwners.Add(owner);
                    projectLocalizations.Add(localization);
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
