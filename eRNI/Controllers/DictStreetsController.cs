using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using eRNI.Models;
using System.Data.Entity;

namespace eRNI.Controllers
{
    public class DictStreetsController : BaseController
    {
        // GET: DictStreets
        [Authorize]
        public ActionResult Index()
        {
            var model = db.tblStreets.OrderBy(o => o.streetName);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<Street> street = new List<Street>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                street = db.tblStreets.OrderBy(o => o.streetName).ToList();
            }
           else
            {
                street = db.tblStreets.Where(s => s.streetName.Contains(searchTerm)).OrderBy(o => o.streetName).ToList();
            }
            return View(street);
        }

        // GET: DictStreets/Create
        [Authorize]
        public ActionResult Create()
        {
          
            return PartialView("Create");
        }

        // POST: DictStreets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "streetID, streetName")] Street street)
        {
            if (ModelState.IsValid)
            {
                db.tblStreets.Add(street);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(street, JsonRequestBehavior.AllowGet);
        }


        // GET: DictStreets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Street street = db.tblStreets.Find(id);
            if (street == null)
            {
                return HttpNotFound();
            }
            
            return PartialView("Edit", street);
        }

        // POST: DictStreets/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "streetID, streetName" )] Street street)
        {
            if (ModelState.IsValid)
            {
                db.Entry(street).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", street);
        }


        // GET: DictStreets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Street street = db.tblStreets.Find(id);
            if (street == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete",street);
        }

        // POST: DictStreets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
                Street street = db.tblStreets.Find(id);
                db.tblStreets.Remove(street);
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
