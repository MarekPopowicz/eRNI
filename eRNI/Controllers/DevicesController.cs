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
    public class DevicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Devices
        public ActionResult Index()
        {
            var tblDevices = db.tblDevices.Include(d => d.deviceCategory).Include(d => d.localization);
            return View(tblDevices.ToList());
        }

        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.tblDevices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Devices/Create
        public ActionResult Create(int id)
        {
            ViewBag.localizationID = new SelectList(db.tblLocalizations.Where(l => l.localizationID == id).ToList(), "localizationID", "localizationPlotNo");
            ViewBag.deviceCategoryID = new SelectList(db.tblDeviceCategories, "deviceCategoryID", "deviceCategoryName");
            ViewBag.ID = id;
            return View();
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deviceID," +
                                                   "deviceLenght," +
                                                   "deviceWidth," +
                                                   "deviceVoltage," +
                                                   "deviceCategoryID," +
                                                   "localizationID")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.tblDevices.Add(device);
                db.SaveChanges();
                return RedirectToAction("Details","Localizations", new { id = device.localizationID });
            }

            ViewBag.deviceCategoryID = new SelectList(db.tblDeviceCategories, "deviceCategoryID", "deviceCategoryName", device.deviceCategoryID);
            ViewBag.localizationID = new SelectList(db.tblLocalizations, "localizationID", "localizationPlotNo", device.localizationID);
            return View(device);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.tblDevices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            ViewBag.deviceCategoryID = new SelectList(db.tblDeviceCategories, "deviceCategoryID", "deviceCategoryName", device.deviceCategoryID);
            ViewBag.localizationID = new SelectList(db.tblLocalizations, "localizationID", "localizationPlotNo", device.localizationID);
            ViewBag.ID = device.localizationID;
            return View(device);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deviceID," +
                                                 "deviceLenght," +
                                                 "deviceWidth," +
                                                 "deviceVoltage," +
                                                 "deviceCategoryID," +
                                                 "localizationID")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Localizations", new { id  = device.localizationID });
            }
            ViewBag.deviceCategoryID = new SelectList(db.tblDeviceCategories, "deviceCategoryID", "deviceCategoryName", device.deviceCategoryID);
            ViewBag.localizationID = new SelectList(db.tblLocalizations, "localizationID", "localizationPlotNo", device.localizationID);
            return View(device);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.tblDevices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }

            ViewBag.localizatonID = device.localizationID;
            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.tblDevices.Find(id);
            db.tblDevices.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Details", "Localizations", new { id = device.localizationID });
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
