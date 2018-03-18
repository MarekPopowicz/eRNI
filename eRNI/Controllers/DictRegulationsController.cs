using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class DictRegulationsController : BaseController
    {
        // GET: DictRegulations
        [Authorize]
        public ActionResult Index()
        {
            var model = db.tblReguationCategories.OrderBy(o => o.regulationCategoryName);
            return View(model);
        }


        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<ReguationCategory> regulation = new List<ReguationCategory>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                regulation = db.tblReguationCategories.OrderBy(o => o.regulationCategoryName).ToList();
            }
            else
            {
                regulation = db.tblReguationCategories.Where(s => s.regulationCategoryName.Contains(searchTerm)).OrderBy(o => o.regulationCategoryName).ToList();
            }
            return View(regulation);
        }


        // GET: DictRegulations/Create
        [Authorize]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: DictRegulations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "regulationCategoryID, regulationCategoryName")] ReguationCategory regulation)
        {
            if (ModelState.IsValid)
            {
                db.tblReguationCategories.Add(regulation);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(regulation, JsonRequestBehavior.AllowGet);
        }


        // GET: DictRegulations/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReguationCategory regulation = db.tblReguationCategories.Find(id);
            if (regulation == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", regulation);
        }

        // POST: DictProjects/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "regulationCategoryID, regulationCategoryName")] ReguationCategory regulation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regulation).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", regulation);
        }

        // GET: DictRegulations/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReguationCategory regulation = db.tblReguationCategories.Find(id);
            if (regulation == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", regulation);
        }

        // POST: DictRegulations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReguationCategory regulation = db.tblReguationCategories.Find(id);
            db.tblReguationCategories.Remove(regulation);
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
