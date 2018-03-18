using eRNI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class DictPlacesController : BaseController
    {
        // GET: DictPlaces/Index
        [Authorize]
        public ActionResult Index()
        {
            var model = db.tblPlaces.OrderBy(o => o.placeName);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<Place> places = new List<Place>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                places = db.tblPlaces.OrderBy(o => o.placeName).ToList();
            }
            else
            {
                places = db.tblPlaces.Where(s => s.placeName.Contains(searchTerm)).OrderBy(o => o.placeName).ToList();
            }
            return View(places);
        }

        // GET: DictPlaces/Create
        [Authorize]
        public ActionResult Create()
        {

            return PartialView("Create");
        }

        // POST: DictPlaces/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "placeID, placeName")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.tblPlaces.Add(place);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(place, JsonRequestBehavior.AllowGet);
        }

        // GET: DictPlaces/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Place place = db.tblPlaces.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", place);
        }

        // POST: DictPlaces/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "placeID, placeName")] Street place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", place);
        }

        // GET: DictPlaces/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.tblPlaces.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", place);
        }

        // POST: DictPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.tblPlaces.Find(id);
            db.tblPlaces.Remove(place);
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
