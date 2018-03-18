using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eRNI.Models;
using eRNI.ViewModels;
using System.Text;
using Rotativa.MVC;

namespace eRNI.Controllers
{
    public class ProjectCorrespondenceController : BaseController
    {

        // GET: ProjectCorrespondence
        public ActionResult Index()
        {
            var tblProjectCorrespondence = db.tblProjectCorrespondence.Include(p => p.project);
            return View(tblProjectCorrespondence.ToList());
        }


        // GET: ProjectCorrespondence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCorrespondence projectCorrespondence = db.tblProjectCorrespondence.Find(id);
            if (projectCorrespondence == null)
            {
                return HttpNotFound();
            }
            return View(projectCorrespondence);
        }

      
        public ActionResult InsertAddressee()
        {

            ViewBag.Addressee = AddresseeList();
            return PartialView("_ProjectCorrespondenceAddressee");
           
        }


        private List<SelectListItem> AddresseeList()
        {
            List<SelectListItem> addressee = new List<SelectListItem>();
            SelectListItem defaultItem = new SelectListItem() { Value = null, Text = string.Empty, Selected = true };
            addressee = db.tblAddressBook.ToList().Select(u => new SelectListItem
            {
                Text = u.addresseeName,
                Value = u.addresseeID.ToString()
            }).ToList();
            addressee.Add(defaultItem);

            return addressee.OrderBy(o => o.Text).ToList();
        }


        // GET: ProjectCorrespondence/Create
        public ActionResult Create(int id)
        {
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == id).ToList(), "projectID", "projectSapNo");
            ViewBag.projectCorrespondenceTemplate = new SelectList(db.tblDocumentsTemplates.ToList(), "documentTemplateName", "documentTemplateName");
            ViewBag.Direction = SetDirection();

            var projectData = db.tblProjects.Where(x => x.projectID == id).FirstOrDefault();
            var nextCorrespondenceNum = (int)db.tblProjectCorrespondence.Max(f => f.projectCorrespondenceID) + 1;

            ViewBag.Sign = "(" + projectData.projectID + ") " + projectData.projectSapNo + "/" + nextCorrespondenceNum + "/" + DateTime.Today.Year.ToString();

            var locationData = db.tblLocalizations.Where(x => x.projectID == id).ToList();
            StringBuilder parcels = new StringBuilder();
            String precinct = string.Empty;
            foreach (var item in locationData)
            {
                parcels.Append("dz. nr " + item.localizationPlotNo + ", AM-" + item.localizationMapNo + "; ");
                precinct = "obręb " + item.localizationPrecinct + " " + item.place.placeName;
            }

            ProjectCorrespondence projectCorrespondence = new ProjectCorrespondence();
            projectCorrespondence.projectCorrespondenceDate = DateTime.Today.Date;
            projectCorrespondence.projectCorrespondenceObtainment = DateTime.Today.Date;
            projectCorrespondence.projectCorrespondenceSubject = projectData.projectTask + " na " + parcels + precinct;

            return View(projectCorrespondence);
        }

        private List<SelectListItem> SetDirection()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Przychodzące", Value = "Przychodzące" },
                new SelectListItem { Text = "Wychodzące", Value = "Wychodzące", Selected = true }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "projectCorrespondenceID," +
                                                    "projectCorrespondenceDirection," +
                                                    "projectCorrespondenceSender," +
                                                    "projectCorrespondenceTemplate," +
                                                    "projectCorrespondenceDate," +
                                                    "projectCorrespondenceSign," +
                                                    "projectCorrespondenceSubject," +
                                                    "projectCorrespondenceObtainment," +
                                                    "projectID")] ProjectCorrespondence projectCorrespondence)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    db.tblProjectCorrespondence.Add(projectCorrespondence);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Projects", new { id = projectCorrespondence.projectID });
                }
                catch
                {
                    ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", projectCorrespondence.projectID);
                    ViewBag.projectCorrespondenceTemplate = new SelectList(db.tblDocumentsTemplates.ToList(), "documentTemplateName", "documentTemplateName");
                    ViewBag.Direction = SetDirection();
                    TempData["msg"] = "Nieprawidłowa wartość w polu data.";
                    return View(projectCorrespondence);
                }
            }

            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", projectCorrespondence.projectID);
            ViewBag.projectCorrespondenceTemplate = new SelectList(db.tblDocumentsTemplates.ToList(), "documentTemplateName", "documentTemplateName");
            ViewBag.Direction = SetDirection();
            return View(projectCorrespondence);
        }

        // GET: ProjectCorrespondence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectCorrespondence projectCorrespondence = db.tblProjectCorrespondence.Find(id);
            if (projectCorrespondence == null)
            {
                return HttpNotFound();
            }

           // ViewBag.Sign = projectCorrespondence.projectCorrespondenceSign;
            ViewBag.Direction = SetDirection();
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == projectCorrespondence.projectID).ToList(), "projectID", "projectSapNo");
            ViewBag.projectCorrespondenceTemplate = new SelectList(db.tblDocumentsTemplates.ToList(), "documentTemplateName", "documentTemplateName");
            return View(projectCorrespondence);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "projectCorrespondenceID," +
                                                 "projectCorrespondenceDirection," +
                                                 "projectCorrespondenceSender," +
                                                 "projectCorrespondenceTemplate," +
                                                 "projectCorrespondenceDate," +
                                                 "projectCorrespondenceSign," +
                                                 "projectCorrespondenceSubject," +
                                                 "projectCorrespondenceObtainment," +
                                                 "projectID")] ProjectCorrespondence projectCorrespondence)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(projectCorrespondence).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Projects", new { id = projectCorrespondence.projectID });
                }
                catch
                {
                    ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", projectCorrespondence.projectID);
                    ViewBag.projectCorrespondenceTemplate = new SelectList(db.tblDocumentsTemplates.ToList(), "documentTemplateName", "documentTemplateName");
                    ViewBag.Direction = SetDirection();
                    TempData["msg"] = "Nieprawidłowa wartość w polu data.";
                    return View(projectCorrespondence);
                }
            }
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", projectCorrespondence.projectID);
            ViewBag.projectCorrespondenceTemplate = new SelectList(db.tblDocumentsTemplates.ToList(), "documentTemplateName", "documentTemplateName");
            ViewBag.Direction = SetDirection();
            return View(projectCorrespondence);
        }

        // GET: ProjectCorrespondence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCorrespondence projectCorrespondence = db.tblProjectCorrespondence.Find(id);
            if (projectCorrespondence == null)
            {
                return HttpNotFound();
            }
            return View(projectCorrespondence);
        }

        // POST: ProjectCorrespondence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectCorrespondence projectCorrespondence = db.tblProjectCorrespondence.Find(id);
            db.tblProjectCorrespondence.Remove(projectCorrespondence);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = projectCorrespondence.projectID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Print(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}
            var model = new CorrespondenceViewModel(id);
            var templ = "~/Views/CorrespondenceTemplates/" + model.projectCorrespondence.projectCorrespondenceTemplate + ".cshtml";
            return new ViewAsPdf(templ, model);
        }


    }
}
