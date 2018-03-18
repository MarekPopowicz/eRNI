using eRNI.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class LocalizationsController : BaseController
    {

        // GET: Localizations
        public ActionResult Index()
        {
            var tblLocalizations = db.tblLocalizations.Include(l => l.place).Include(l => l.project);
            return View(tblLocalizations.ToList());
        }

        // GET: Localizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["localizationID"] = id;

            Localization localization = db.tblLocalizations.Find(id);
            if (localization == null)
            {
                return HttpNotFound();
            }

            IEnumerable<SelectListItem> sapNo = db.tblProjects
                                       .Where(i => i.projectID == localization.projectID)
                                       .Select(s => new SelectListItem { Value = s.projectID.ToString(), Text = s.projectSapNo });

            ViewBag.projectId = sapNo.First().Value;
            ViewBag.projctSapNo =  sapNo.First().Text;

            return View(localization);
        }

        // GET: Localizations/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.Places = SetPlaces();
            ViewBag.Regions = SetRegions();
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == id).ToList(), "projectID", "projectSapNo");
            ViewBag.returnID = id;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "localizationID," +
                                                    "localizationLandRegister," +
                                                    "localizationMapNo," +
                                                    "localizationPlotNo," +
                                                    "localizationPrecinct," +
                                                    "localizationRegulationStatus," +
                                                    "localizationStreets," +
                                                    "localizationRegion," +
                                                    "placeID," +
                                                    "projectID")] Localization localization)
        {
            if (ModelState.IsValid)
            {
                db.tblLocalizations.Add(localization);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new {id = localization.projectID });
            }

            ViewBag.Places = SetPlaces();
            ViewBag.Regions = SetRegions();
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", localization.projectID);
            return View(localization);
        }

        // GET: Localizations/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localization localization = db.tblLocalizations.Find(id);
            if (localization == null)
            {
                return HttpNotFound();
            }

            ViewBag.placeID = new SelectList(db.tblPlaces, "placeID", "placeName", localization.placeID).OrderBy(n=>n.Text);
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == localization.projectID).ToList(), "projectID", "projectSapNo");
            ViewBag.Regions = SetRegions();

            return View(localization);
        }

        // POST: Localizations/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "localizationID, " +
                                                 "localizationLandRegister, " +
                                                 "localizationMapNo, " +
                                                 "localizationPlotNo, " +
                                                 "localizationPrecinct, " +
                                                 "localizationRegulationStatus, " +
                                                 "localizationStreets, " +
                                                 "localizationRegion," +
                                                 "placeID, " +
                                                 "projectID")] Localization localization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = localization.projectID });
            }
            ViewBag.placeID = new SelectList(db.tblPlaces, "placeID", "placeName", localization.placeID);
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", localization.projectID);
            return RedirectToAction("Details", "Projects", new { id = localization.projectID });
        }


        private List<SelectListItem> SetPlaces()
        {
            List<SelectListItem> places = new List<SelectListItem>();
            SelectListItem defaultItem = new SelectListItem() { Value = null, Text = string.Empty, Selected = true };
            places = db.tblPlaces.ToList().Select(u => new SelectListItem
            {
                Text = u.placeName,
                Value = u.placeID.ToString()
            }).ToList();
            places.Add(defaultItem);

            return places.OrderBy(o => o.Text).ToList();
        }

        private List<SelectListItem> SetRegions()
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = string.Empty, Value = null, Selected = true },
                new SelectListItem { Text = "51 (Wrocław)", Value = "51 (Wrocław)" },
                new SelectListItem { Text = "52 (Oborniki Śląskie)", Value = "52 (Oborniki Śląskie)" },
                new SelectListItem { Text = "53 (Oleśnica)", Value = "53 (Oleśnica)" },
                new SelectListItem { Text = "54 (Strzelin)", Value = "54 (Strzelin)" },
                new SelectListItem { Text = "55 (Środa Śląska)", Value = "55 (Środa Śląska)" }
            };

            return items;
        }




        // GET: Localizations/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localization localization = db.tblLocalizations.Find(id);
            if (localization == null)
            {
                return HttpNotFound();
            }

            return View(localization);
        }

        // POST: Localizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            List<Ownership> ownersIDs = db.tblOwnerships.Where(l => l.localizationID == id).ToList();

            foreach (var localizationItem in ownersIDs)
            {
                Ownership owners = localizationItem;
                db.tblOwnerships.Remove(owners);
            }
            db.SaveChanges();

            Localization localization = db.tblLocalizations.Find(id);
            db.tblLocalizations.Remove(localization);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = localization.projectID});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

   
        public ActionResult LocalizationOwners(int? id)
        {
            if (id == null) RedirectToAction("Index");
            List<Ownership> ownersIDs = db.tblOwnerships.Where(o => o.localizationID == id).ToList();
            List<Owner> owners = new List<Owner>();
            foreach (var ownerItem in ownersIDs)
            {
                if (ownerItem.ownerID != null)
                {
                    var owner = db.tblOwners.Single(o => o.ownerID == ownerItem.ownerID);
                    owners.Add(owner);
                }
            }
            
            return PartialView("_ViewOwnersOfLocalization", owners);
        }

        public ActionResult LocalizationDevices(int? id)
        {
            if (id == null) RedirectToAction("Index");
            List<Device> devices = db.tblDevices.Where(d => d.localizationID == id).ToList();
           
            return PartialView("_ViewDevicesOfLocalization", devices);
        }

    }
}
