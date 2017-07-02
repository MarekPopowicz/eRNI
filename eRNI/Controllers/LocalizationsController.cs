using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class LocalizationsController : BaseController
    {

        // GET: Localizations
        public ActionResult Index()
        {
            var tblLocalizations = db.tblLocalizations.Include(l => l.place).Include(l => l.project);
            return View(tblLocalizations.ToList());
        }

        // GET: Localizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["localizationID"] = id;

            Localization localization = db.tblLocalizations.Find(id);
            if (localization == null)
            {
                return HttpNotFound();
            }

            IEnumerable<SelectListItem> sapNo = db.tblProjects
                                       .Where(i => i.projectID == localization.projectID)
                                       .Select(s => new SelectListItem { Value = s.projectID.ToString(), Text = s.projectSapNo });

            ViewBag.projectId = sapNo.First().Value;
            ViewBag.projctSapNo =  sapNo.First().Text;

            return View(localization);
        }

        // GET: Localizations/Create
        public ActionResult Create(int? id)
        {
            var projID = (int)Session["projectID"];
            ViewBag.placeID = new SelectList(db.tblPlaces, "placeID", "placeName");
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == projID).ToList(), "projectID", "projectSapNo");
            ViewBag.projID = projID;
            return View();
        }
        

        // POST: Localizations/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "localizationID," +
                                                    "localizationLandRegister," +
                                                    "localizationMapNo," +
                                                    "localizationPlotNo," +
                                                    "localizationPrecinct," +
                                                    "localizationRegulationStatus," +
                                                    "localizationStreets," +
                                                    "placeID," +
                                                    "projectID")] Localization localization)
        {
            if (ModelState.IsValid)
            {
                db.tblLocalizations.Add(localization);
                Ownership ownership = new Ownership();
                ownership.localizationID = localization.localizationID;
                db.tblOwnerships.Add(ownership);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new {id = (int)Session["projectID"] });
            }

            ViewBag.placeID = new SelectList(db.tblPlaces, "placeID", "placeName", localization.placeID);
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", localization.projectID);
            return View(localization);
        }

        // GET: Localizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localization localization = db.tblLocalizations.Find(id);
            if (localization == null)
            {
                return HttpNotFound();
            }

            ViewBag.placeID = new SelectList(db.tblPlaces, "placeID", "placeName", localization.placeID);
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == localization.projectID).ToList(), "projectID", "projectSapNo");
            return View(localization);
        }

        // POST: Localizations/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "localizationID, " +
                                                 "localizationLandRegister, " +
                                                 "localizationMapNo, " +
                                                 "localizationPlotNo, " +
                                                 "localizationPrecinct, " +
                                                 "localizationRegulationStatus, " +
                                                 "localizationStreets, " +
                                                 "placeID, " +
                                                 "projectID")] Localization localization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = (int)Session["projectID"] });
            }
            ViewBag.placeID = new SelectList(db.tblPlaces, "placeID", "placeName", localization.placeID);
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", localization.projectID);
            return RedirectToAction("BackToProjectDetails");
        }

        // GET: Localizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localization localization = db.tblLocalizations.Find(id);
            if (localization == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectId = localization.projectID;
            return View(localization);
        }

        // POST: Localizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            List<Ownership> ownersIDs = db.tblOwnerships.Where(l => l.localizationID == id).ToList();

            foreach (var localizationItem in ownersIDs)
            {
                Ownership owners = localizationItem;
                db.tblOwnerships.Remove(owners);
            }
            db.SaveChanges();

            Localization localization = db.tblLocalizations.Find(id);
            db.tblLocalizations.Remove(localization);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = (int)Session["projectID"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

   
        public ActionResult LocalizationOwners(int? id)
        {
            if (id == null) RedirectToAction("Index");
            List<Ownership> ownersIDs = db.tblOwnerships.Where(o => o.localizationID == id).ToList();
            List<Owner> owners = new List<Owner>();
            foreach (var ownerItem in ownersIDs)
            {
                if (ownerItem.ownerID != null)
                {
                    var owner = db.tblOwners.Single(o => o.ownerID == ownerItem.ownerID);
                    owners.Add(owner);
                }
            }
            
            return PartialView("_ViewOwnersOfLocalization", owners);
        }

    }
}
