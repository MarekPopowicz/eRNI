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
    public class PropertyDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PropertyDocuments
        public ActionResult Index()
        {
            var tblPropertyDocuments = db.tblPropertyDocuments.Include(p => p.project);
            return View(tblPropertyDocuments.ToList());
        }

        // GET: PropertyDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyDocument propertyDocument = db.tblPropertyDocuments.Find(id);
            if (propertyDocument == null)
            {
                return HttpNotFound();
            }
            return View(propertyDocument);
        }

        // GET: PropertyDocuments/Create
        public ActionResult Create()
        {
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectAdditionalInfo");
            return View();
        }

        // POST: PropertyDocuments/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "propertyDocumentID,propertyDocumentAdditionalInfo,propertyDocumentSapRegisterNo,propertyDocumentSapRegistrationDate,propertyDocumentType,projectID")] PropertyDocument propertyDocument)
        {
            if (ModelState.IsValid)
            {
                db.tblPropertyDocuments.Add(propertyDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectAdditionalInfo", propertyDocument.projectID);
            return View(propertyDocument);
        }

        // GET: PropertyDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyDocument propertyDocument = db.tblPropertyDocuments.Find(id);
            if (propertyDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectAdditionalInfo", propertyDocument.projectID);
            return View(propertyDocument);
        }

        // POST: PropertyDocuments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "propertyDocumentID,propertyDocumentAdditionalInfo,propertyDocumentSapRegisterNo,propertyDocumentSapRegistrationDate,propertyDocumentType,projectID")] PropertyDocument propertyDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertyDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectAdditionalInfo", propertyDocument.projectID);
            return View(propertyDocument);
        }

        // GET: PropertyDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyDocument propertyDocument = db.tblPropertyDocuments.Find(id);
            if (propertyDocument == null)
            {
                return HttpNotFound();
            }
            return View(propertyDocument);
        }

        // POST: PropertyDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropertyDocument propertyDocument = db.tblPropertyDocuments.Find(id);
            db.tblPropertyDocuments.Remove(propertyDocument);
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
