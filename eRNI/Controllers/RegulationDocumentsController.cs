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
    public class RegulationDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegulationDocuments
        public ActionResult Index()
        {
            var tblRegulationDocuments = db.tblRegulationDocuments.Include(r => r.regulation);
            return View(tblRegulationDocuments.ToList());
        }

        // GET: RegulationDocuments/Details/5
        public ActionResult Details(int? id)
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

        // GET: RegulationDocuments/Create
        public ActionResult Create(int id)
        {
            ViewBag.regulationID = new SelectList(db.tblRegulations.Where(x => x.regulationID == id).ToList(), "regulationID", "regulationID");
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
                                                    "regulationID")] RegulationDocument regulationDocument)
        {
            if (ModelState.IsValid)
            {
                db.tblRegulationDocuments.Add(regulationDocument);
                db.SaveChanges();
                return RedirectToAction("Details","Regulations",new { id = regulationDocument.regulationID });
            }

            ViewBag.regulationID = new SelectList(db.tblRegulations, "regulationID", "regulationAdditionalInfo", regulationDocument.regulationID);
            return View(regulationDocument);
        }

        // GET: RegulationDocuments/Edit/5
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
            ViewBag.regulationID = new SelectList(db.tblRegulations, "regulationID", "regulationAdditionalInfo", regulationDocument.regulationID);
            return View(regulationDocument);
        }

        // POST: RegulationDocuments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "regulationDocumentID,regulationDocumentDate,regulationDocumentSignature,regulationDocumentSource,regulationDocumentType,regulationDocumentLink,regulationID")] RegulationDocument regulationDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regulationDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.regulationID = new SelectList(db.tblRegulations, "regulationID", "regulationAdditionalInfo", regulationDocument.regulationID);
            return View(regulationDocument);
        }

        // GET: RegulationDocuments/Delete/5
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
            return RedirectToAction("Index");
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
