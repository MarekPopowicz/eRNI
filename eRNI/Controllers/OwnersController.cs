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
    public class OwnersController : BaseController
    {
            
       
        // GET: Owners
        public ActionResult Index()
        {
            var tblOwners = db.tblOwners.Include(o => o.street);
            return View(tblOwners.ToList());
        }

        // GET: Owners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.tblOwners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // GET: Owners/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.localizationID = Session["localizationID"];

            List<SelectListItem> streets = new List<SelectListItem>();
            SelectListItem defaultItem = new SelectListItem() { Value = null, Text = string.Empty, Selected = true };
            streets = db.tblStreets.ToList().Select(s => new SelectListItem
            {
                Text = s.streetName,
                Value = s.streetID.ToString()
            }).ToList();
            streets.Add(defaultItem);

            ViewBag.Streets = streets.OrderBy(o => o.Text).ToList();

            return View();
        }

        // POST: Owners/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ownerAdditionalInfo," +
                                                   "ownerName,ownerHouseNo," +
                                                   "ownerAptNo,ownerPhone," +
                                                   "ownerCellPhone," +
                                                   "ownerPostOffice," +
                                                   "ownerZipCode," +
                                                   "ownerEmail," +
                                                   "ownerCity," +
                                                   "streetID")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.tblOwners.Add(owner);
                db.SaveChanges();

                Ownership ownership = new Ownership();
                ownership.ownerID = owner.ownerID;
                ownership.localizationID = (int)Session["localizationID"];
                db.tblOwnerships.Add(ownership);
                db.SaveChanges();

                return RedirectToAction("Details", "Localizations", new { id = (int)Session["localizationID"] });
            }

            
            ViewBag.streetID = new SelectList(db.tblStreets, "streetID", "streetName", owner.streetID);
            return View(owner);
        }

        private List<SelectListItem> SetStreets()
        {
            List<SelectListItem> streets = new List<SelectListItem>();
            SelectListItem defaultItem = new SelectListItem() { Value = null, Text = string.Empty, Selected = true };
            streets = db.tblStreets.ToList().Select(u => new SelectListItem
            {
                Text = u.streetName,
                Value = u.streetID.ToString()
            }).ToList();
            streets.Add(defaultItem);

            return streets.OrderBy(o => o.Text).ToList();
        }

        // GET: Owners/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.tblOwners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            ViewBag.localizationID = Session["localizationID"];
            var streets = SetStreets();
            ViewBag.Streets = streets;
            if (owner.street == null)
            {
                ViewBag.streetName = string.Empty;
            }
            else
            {
                ViewBag.streetName = streets.Where(s => s.Text.Equals(owner.street.streetName)).FirstOrDefault();
            }
            return View(owner);
        }

        // POST: Owners/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ownerID," +
                                                 "ownerAdditionalInfo," +
                                                 "ownerName,ownerHouseNo," +
                                                 "ownerAptNo,ownerPhone," +
                                                 "ownerCellPhone," +
                                                 "ownerPostOffice," +
                                                 "ownerZipCode," +
                                                 "ownerEmail," +
                                                 "ownerCity,streetID")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(owner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Localizations", new { id = (int)Session["localizationID"] });
            }
            ViewBag.streetID = new SelectList(db.tblStreets, "streetID", "streetName", owner.streetID);
            return View(owner);
        }

        // GET: Owners/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.tblOwners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }

            ViewBag.localizationID = Session["localizationID"];
            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Ownership> ownersLocalizationsID = db.tblOwnerships.Where(o => o.ownerID == id).ToList();
          
            foreach (var localizationItem in ownersLocalizationsID)
            {
                Ownership ownerLocalization = localizationItem;
                db.tblOwnerships.Remove(ownerLocalization);
            }
            db.SaveChanges();

            Owner owner = db.tblOwners.Find(id);
            db.tblOwners.Remove(owner);
            db.SaveChanges();

            return RedirectToAction("Details","Localizations",new { id = (int)Session["localizationID"] });
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
