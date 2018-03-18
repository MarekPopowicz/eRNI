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
    public class DictDocumentsController : BaseController
    {

        // GET: DictDocuments
        [Authorize]
        public ActionResult Index()
        {
            var model = db.tblKeyDocumentCategories.OrderBy(o => o.keyDocumentName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<KeyDocumentCategory> keyDocument = new List<KeyDocumentCategory>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                keyDocument = db.tblKeyDocumentCategories.OrderBy(o => o.keyDocumentName).ToList();
            }
            else
            {
                keyDocument = db.tblKeyDocumentCategories.Where(s => s.keyDocumentName.Contains(searchTerm)).OrderBy(o => o.keyDocumentName).ToList();
            }
            return View(keyDocument);
        }



        // GET: DictDocuments/Create
        [Authorize]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: DictDocuments/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeyDocumentCategoryID,keyDocumentName")] KeyDocumentCategory keyDocumentCategory)
        {
            if (ModelState.IsValid)
            {
                db.tblKeyDocumentCategories.Add(keyDocumentCategory);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(keyDocumentCategory, JsonRequestBehavior.AllowGet);
        }

        // GET: DictDocuments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyDocumentCategory keyDocumentCategory = db.tblKeyDocumentCategories.Find(id);
            if (keyDocumentCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView("Edit", keyDocumentCategory);
        }

        // POST: DictDocuments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeyDocumentCategoryID,keyDocumentName")] KeyDocumentCategory keyDocumentCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyDocumentCategory).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", keyDocumentCategory);
        }

        // GET: DictDocuments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeyDocumentCategory keyDocumentCategory = db.tblKeyDocumentCategories.Find(id);
            if (keyDocumentCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", keyDocumentCategory);
        }

        // POST: DictDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KeyDocumentCategory keyDocumentCategory = db.tblKeyDocumentCategories.Find(id);
            db.tblKeyDocumentCategories.Remove(keyDocumentCategory);
            db.SaveChanges();
            return Json(new { success = true });
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
