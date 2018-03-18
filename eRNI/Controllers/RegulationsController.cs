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
    public class RegulationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Regulations
        public ActionResult Index()
        {
            var tblRegulations = db.tblRegulations.Include(r => r.device).Include(r => r.reguationCategory);
            return View(tblRegulations.ToList());
        }


        // GET: Regulations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regulation regulation = db.tblRegulations.Find(id);
            if (regulation == null)
            {
                return HttpNotFound();
            }

            IEnumerable<SelectListItem> sapNo = db.tblProjects
                                      .Where(i => i.projectID == regulation.device.localization.projectID)
                                      .Select(s => new SelectListItem { Value = s.projectID.ToString(), Text = s.projectSapNo });

            ViewBag.projectId = sapNo.First().Value;
            ViewBag.projctSapNo = sapNo.First().Text;

            return View(regulation);
        }


        // GET: Regulations/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            var device = db.tblDevices.Find(id);
            ViewBag.deviceID = new SelectList(db.tblDevices.Where(x => x.deviceID == id).ToList(), "deviceID", "deviceID");
            ViewBag.regulationCategoryID = new SelectList(db.tblReguationCategories, "regulationCategoryID", "regulationCategoryName");
            ViewBag.devID = device.deviceID;
            return View();
        }

        // POST: Regulations/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "regulationID," +
                                                   "regulationAdditionalInfo," +
                                                   "regulationSapElement," +
                                                   "regulationCost," +
                                                   "deviceID," +
                                                   "regulationCategoryID")] Regulation regulation)
        {
            if (ModelState.IsValid)
            {
                db.tblRegulations.Add(regulation);
                db.SaveChanges();
                return RedirectToAction("Details","Devices", new { id = regulation.deviceID });
            }

            ViewBag.deviceID = new SelectList(db.tblDevices, "deviceID", "deviceID", regulation.deviceID);
            ViewBag.regulationCategoryID = new SelectList(db.tblReguationCategories, "regulationCategoryID", "regulationCategoryName", regulation.regulationCategoryID);
            return View(regulation);
        }

        // GET: Regulations/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regulation regulation = db.tblRegulations.Find(id);
            if (regulation == null)
            {
                return HttpNotFound();
            }
            ViewBag.deviceID = new SelectList(db.tblDevices.Where(x => x.deviceID == regulation.deviceID).ToList(), "deviceID", "deviceID", regulation.deviceID);
            ViewBag.regulationCategoryID = new SelectList(db.tblReguationCategories, "regulationCategoryID", "regulationCategoryName", regulation.regulationCategoryID);
            return View(regulation);
        }

        // POST: Regulations/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "regulationID," +
                                                 "regulationAdditionalInfo," +
                                                 "regulationSapElement," +
                                                 "regulationCost," +
                                                 "deviceID," +
                                                 "regulationCategoryID")] Regulation regulation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regulation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Regulations", new { id = regulation.regulationID });
            }
            ViewBag.deviceID = new SelectList(db.tblDevices, "deviceID", "deviceID", regulation.deviceID);
            ViewBag.regulationCategoryID = new SelectList(db.tblReguationCategories, "regulationCategoryID", "regulationCategoryName", regulation.regulationCategoryID);
            return View(regulation);
        }

        // GET: Regulations/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Regulation regulation = db.tblRegulations.Find(id);
            if (regulation == null)
            {
                return HttpNotFound();
            }
            return View(regulation);
        }

        // POST: Regulations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Regulation regulation = db.tblRegulations.Find(id);
            db.tblRegulations.Remove(regulation);
            db.SaveChanges();
            return RedirectToAction("Details", "Devices", new { id = regulation.deviceID });
        }

        public ActionResult RegulationDocuments(int? id)
        {
            if (id == null) RedirectToAction("Details", new { id });
            var regulationDocuments = db.tblRegulationDocuments.Where(d => d.regulationID == id);
            if (regulationDocuments == null) RedirectToAction("Details", new { id });
            return PartialView("_ViewDokumentsOfRegulation", regulationDocuments.ToList());
        }

        public ActionResult SumRegulationCharges(int id)
        {
            var regulationDocumentCharges = db.tblRegulationDocumentCharges.Where(i => i.regulationDocuments.regulationID == id).ToList();
            var regulation = db.tblRegulations.Where(r => r.regulationID == id).FirstOrDefault();
            var sum = regulation.regulationCost;

            if (regulationDocumentCharges.Count != 0)
            {
                sum += regulationDocumentCharges.Sum(s => s.regulationDocumentFee);
            }
            else
            {
                sum += 0;
            }
            return PartialView("_ViewDocumentChargesPartialSummary", String.Format("{0:C}", sum));
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
