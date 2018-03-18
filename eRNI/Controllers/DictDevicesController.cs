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
    public class DictDevicesController : BaseController
    {
        // GET: DictDevices
        [Authorize]
        public ActionResult Index()
        {
            var model = db.tblDeviceCategories.OrderBy(o => o.deviceCategoryName);
            return View(model);
        }


        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            List<DeviceCategory> devices = new List<DeviceCategory>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                devices = db.tblDeviceCategories.OrderBy(o => o.deviceCategoryName).ToList();
            }
            else
            {
                devices = db.tblDeviceCategories.Where(s => s.deviceCategoryName.Contains(searchTerm)).OrderBy(o => o.deviceCategoryName).ToList();
            }
            return View(devices);
        }


        // GET: DictDevices/Create
        [Authorize]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: DictDevices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deviceCategoryID, deviceCategoryName")] DeviceCategory device)
        {
            if (ModelState.IsValid)
            {
                db.tblDeviceCategories.Add(device);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(device, JsonRequestBehavior.AllowGet);
        }

        // GET: DictDevices/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeviceCategory device = db.tblDeviceCategories.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", device);
        }

        // POST: DictDevices/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "deviceCategoryID, deviceCategoryName")] DeviceCategory device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Edit", device);
        }

        // GET: DictDevices/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceCategory device = db.tblDeviceCategories.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            return PartialView("Delete", device);
        }

        // POST: DictDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceCategory device = db.tblDeviceCategories.Find(id);
            db.tblDeviceCategories.Remove(device);
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
