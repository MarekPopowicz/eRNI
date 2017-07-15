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
    public class KeyDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: KeyDcuments
        public ActionResult Index()
        {
            var tblKeyDocuments = db.tblKeyDocuments.Include(k => k.keyDocumentCategory).Include(k => k.project);

            return View(tblKeyDocuments.ToList());
        }

        // GET: KeyDcuments/Create
        public ActionResult Create(int id)
        {
            ViewBag.KeyDocumentCategoryID = new SelectList(db.tblKeyDocumentCategories, "KeyDocumentCategoryID", "keyDocumentName");
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == id).ToList(), "projectID", "projectSapNo");
            return View();
        }

        // POST: KeyDcuments/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "keyDocumentID," +
                                                    "keyDocumentDate," +
                                                    "keyDocumentSign," +
                                                    "KeyDocumentCategoryID," +
                                                    "keyDocumentObtainment," +
                                                    "projectID")] KeyDcument keyDcument)
        {
            if (ModelState.IsValid)
            {
                db.tblKeyDocuments.Add(keyDcument);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = keyDcument.projectID });
            }

            ViewBag.KeyDocumentCategoryID = new SelectList(db.tblKeyDocumentCategories, "KeyDocumentCategoryID", "keyDocumentName", keyDcument.KeyDocumentCategoryID);
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", keyDcument.projectID);
            return View(keyDcument);
        }

        // GET: KeyDcuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyDcument keyDcument = db.tblKeyDocuments.Find(id);
            if (keyDcument == null)
            {
                return HttpNotFound();
            }
            ViewBag.KeyDocumentCategoryID = new SelectList(db.tblKeyDocumentCategories, "KeyDocumentCategoryID", "keyDocumentName", keyDcument.KeyDocumentCategoryID);
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == id).ToList(), "projectID", "projectSapNo", keyDcument.projectID);
            return View(keyDcument);
        }

        // POST: KeyDcuments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "keyDocumentID," +
                                                    "keyDocumentDate," +
                                                    "keyDocumentSign," +
                                                    "KeyDocumentCategoryID," +
                                                    "keyDocumentObtainment," +
                                                    "projectID")] KeyDcument keyDcument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyDcument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = keyDcument.projectID });
            }
            ViewBag.KeyDocumentCategoryID = new SelectList(db.tblKeyDocumentCategories, "KeyDocumentCategoryID", "keyDocumentName", keyDcument.KeyDocumentCategoryID);
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", keyDcument.projectID);
            return View(keyDcument);
        }

        // GET: KeyDcuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyDcument keyDcument = db.tblKeyDocuments.Find(id);
            if (keyDcument == null)
            {
                return HttpNotFound();
            }
            return View(keyDcument);
        }

        // POST: KeyDcuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyDcument keyDcument = db.tblKeyDocuments.Find(id);
            db.tblKeyDocuments.Remove(keyDcument);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = keyDcument.projectID });
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
