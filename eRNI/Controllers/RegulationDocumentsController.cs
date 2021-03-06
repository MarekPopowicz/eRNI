using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eRNI.Models;
using System.Collections;

namespace eRNI.Controllers
{
    public class RegulationDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegulationDocuments
        public ActionResult Index()
        {
            var tblRegulationDocuments = db.tblRegulationDocuments.Include(r => r.regulation);
            return View(tblRegulationDocuments.ToList());
        }

        // GET: RegulationDocuments/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            Regulation regulation = db.tblRegulations.Where(x => x.regulationID == id).SingleOrDefault();
            ViewBag.regulationID = new SelectList(db.tblRegulations.Where(x => x.regulationID == id).ToList(), "regulationID", "regulationID");
            ViewBag.regID = regulation.regulationID;
            ViewBag.sapNo = regulation.device.localization.project.projectSapNo;
            ViewBag.projectID = regulation.device.localization.projectID;
            var localization = db.tblLocalizations.Where(l => l.localizationID == regulation.device.localizationID).FirstOrDefault();
            string location = "dz. nr " + localization.localizationPlotNo + " AM-" + localization.localizationMapNo + " obr. " + localization.localizationPrecinct + " " + localization.place.placeName;
            ViewBag.localization = location;
            return View();
        }

        // POST: RegulationDocuments/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "regulationDocumentID," +
                                                    "regulationDocumentDate," +
                                                    "regulationDocumentSignature," +
                                                    "regulationDocumentSource," +
                                                    "regulationDocumentType," +
                                                    "regulationDocumentLink," +
                                                    "regulationID," +
                                                    "additionalInformation")] RegulationDocument regulationDocument)
        {
            if (ModelState.IsValid)
            {
                db.tblRegulationDocuments.Add(regulationDocument);
                db.SaveChanges();
                return RedirectToAction("Details","Regulations",new { id = regulationDocument.regulationID });
            }

            ViewBag.regulationID = new SelectList(db.tblRegulations.Where(x => x.regulationID == regulationDocument.regulationID).ToList(), "regulationID", "regulationID", regulationDocument.regulationID);
            return View(regulationDocument);
        }

        // GET: RegulationDocuments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegulationDocument regulationDocument = db.tblRegulationDocuments.Find(id);
            if (regulationDocument == null)
            {
                return HttpNotFound();
            }

            ViewBag.regulationID = new SelectList(db.tblRegulations.Where(x => x.regulationID == regulationDocument.regulationID).ToList(), "regulationID", "regulationID", regulationDocument.regulationID);
            Regulation regulation = db.tblRegulations.Where(x => x.regulationID == regulationDocument.regulationID).SingleOrDefault();
            ViewBag.sapNo = regulation.device.localization.project.projectSapNo;
            ViewBag.projectID = regulation.device.localization.projectID;
            var localization = db.tblLocalizations.Where(l => l.localizationID == regulation.device.localizationID).FirstOrDefault();
            string location = "dz. nr " + localization.localizationPlotNo + " AM-" + localization.localizationMapNo + " obr. " + localization.localizationPrecinct + " " + localization.place.placeName;
            ViewBag.localization = location;

            return View(regulationDocument);
        }

        // POST: RegulationDocuments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "regulationDocumentID," +
                                                    "regulationDocumentDate," +
                                                    "regulationDocumentSignature," +
                                                    "regulationDocumentSource," +
                                                    "regulationDocumentType," +
                                                    "regulationDocumentLink," +
                                                    "regulationID, " +
                                                    "additionalInformation")] RegulationDocument regulationDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regulationDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Regulations", new { id = regulationDocument.regulationID });
            }
            ViewBag.regulationID = new SelectList(db.tblRegulations.Where(x => x.regulationID == regulationDocument.regulationID).ToList(), "regulationID", "regulationID", regulationDocument.regulationID);
            return View(regulationDocument);
        }

        // GET: RegulationDocuments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegulationDocument regulationDocument = db.tblRegulationDocuments.Find(id);
            if (regulationDocument == null)
            {
                return HttpNotFound();
            }
            return View(regulationDocument);
        }

        // POST: RegulationDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegulationDocument regulationDocument = db.tblRegulationDocuments.Find(id);
            db.tblRegulationDocuments.Remove(regulationDocument);
            db.SaveChanges();
            return RedirectToAction("Details", "Regulations", new { id = regulationDocument.regulationID });
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
