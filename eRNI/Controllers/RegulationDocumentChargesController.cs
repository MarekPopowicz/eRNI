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
    public class RegulationDocumentChargesController : BaseController
    {
      
        // GET: RegulationDocumentCharges
        public ActionResult Index()
        {
            var tblRegulationDocumentCharges = db.tblRegulationDocumentCharges.Include(r => r.regulationDocuments);
            return View(tblRegulationDocumentCharges.ToList());
        }

        // GET: RegulationDocumentCharges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegulationDocumentCharge regulationDocumentCharge = db.tblRegulationDocumentCharges.Find(id);
            if (regulationDocumentCharge == null)
            {
                return HttpNotFound();
            }
            return View(regulationDocumentCharge);
        }

        private List<SelectListItem> SetTaxList()
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                
                new SelectListItem { Text = "23%", Value = "23,00" },
                new SelectListItem { Text = "22%", Value = "22,00" },
                new SelectListItem { Text = "7%", Value = "7,00" },
                new SelectListItem { Text = "2%", Value = "2,00" },
                new SelectListItem { Text = "0%", Value = "0,00" }
            };
            return items;
        }

        private List<SelectListItem> SetTitleList()
        {
            List<SelectListItem> titles = new List<SelectListItem> {
               
                new SelectListItem { Text = "Taksa notarialna", Value = "Taksa notarialna" },
                new SelectListItem { Text = "Opłata sądowa", Value = "Opłata sądowa" },
                new SelectListItem { Text = "Opłata za wypisy", Value = "Opłata za wypisy" },
                new SelectListItem { Text = "Opłata za pełnomocnictwo", Value = "Opłata za pełnomocnictwo" },
                new SelectListItem { Text = "Opłata za wpis do KW", Value = "Opłata za wpis do KW" },
                new SelectListItem { Text = "Opłata za wypis i wyrys", Value = "Opłata za wypis i wyrys" },
                new SelectListItem { Text = "Opłata administracyjna", Value = "Opłata administracyjna" },
                new SelectListItem { Text = "Inna opłata", Value = "Inna opłata" }
            };
            return titles;
        }


        // GET: RegulationDocumentCharges/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            RegulationDocument regulationDocument = db.tblRegulationDocuments.Find(id);
            RegulationDocumentCharge regulationDocumentCharges = new RegulationDocumentCharge();

            ViewBag.regulationDocumentChargeDate = String.Format("{0:yyyy-MM-dd}", regulationDocument.regulationDocumentDate);
            ViewBag.regulationDocumentName = regulationDocument.regulationDocumentType + " " + regulationDocument.regulationDocumentSignature;
            ViewBag.returnID = regulationDocument.regulationID;

            ViewBag.regulationDocumentID = new SelectList(db.tblRegulationDocuments.Where(d=>d.regulationDocumentID==id), "regulationDocumentID", "regulationDocumentSignature");
            ViewBag.regulationDocumentTax = SetTaxList();
            ViewBag.regulationDocumentChargeTitle = SetTitleList();
            
            return View(regulationDocumentCharges);
        }

        // POST: RegulationDocumentCharges/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "regulationDocumentCostID," +
                                                    "regulationDocumentName," +
                                                    "regulationDocumentChargeDate," +
                                                    "regulationDocumentChargeTitle," +
                                                    "regulationDocumentSellerName," +
                                                    "regulationDocumentFee," +
                                                    "regulationDocumentTax," +
                                                    "regulationDocumentCostAdditionalInfo," +
                                                    "regulationDocumentID")] RegulationDocumentCharge regulationDocumentCharge)
        {
           
            if (ModelState.IsValid)
            {
                var returnID = db.tblRegulationDocuments.Find(regulationDocumentCharge.regulationDocumentID).regulationID;
                db.tblRegulationDocumentCharges.Add(regulationDocumentCharge);
                db.SaveChanges();
                return RedirectToAction("Details", "Regulations", routeValues: new { id = returnID });
            }

            ViewBag.regulationDocumentID = new SelectList(db.tblRegulationDocuments, "regulationDocumentID", "regulationDocumentSignature", regulationDocumentCharge.regulationDocumentID);
            return View(regulationDocumentCharge);
        }

        // GET: RegulationDocumentCharges/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegulationDocumentCharge regulationDocumentCharge = db.tblRegulationDocumentCharges.Find(id);
            if (regulationDocumentCharge == null)
            {
                return HttpNotFound();
            }
            ViewBag.regulationDocumentID = new SelectList(db.tblRegulationDocuments.Where(d => d.regulationDocumentID == regulationDocumentCharge.regulationDocumentID), "regulationDocumentID", "regulationDocumentSignature", regulationDocumentCharge.regulationDocumentID);
            ViewBag.chargeTax = SetTaxList();
            ViewBag.chargeTitle = SetTitleList();
            
            return View(regulationDocumentCharge);
        }

        // POST: RegulationDocumentCharges/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "regulationDocumentCostID," +
                                                 "regulationDocumentName," +
                                                 "regulationDocumentChargeDate," +
                                                 "regulationDocumentChargeTitle," +
                                                 "regulationDocumentSellerName," +
                                                 "regulationDocumentFee," +
                                                 "regulationDocumentTax," +
                                                 "regulationDocumentCostAdditionalInfo," +
                                                 "regulationDocumentID")] RegulationDocumentCharge regulationDocumentCharge)
        {
            if (ModelState.IsValid)
            {
                var returnID = db.tblRegulationDocuments.Find(regulationDocumentCharge.regulationDocumentID).regulationID;
                db.Entry(regulationDocumentCharge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Regulations", new { id = returnID });
            }
            ViewBag.regulationDocumentID = new SelectList(db.tblRegulationDocuments.Where(d => d.regulationDocumentID == regulationDocumentCharge.regulationDocumentID), "regulationDocumentID", "regulationDocumentSignature", regulationDocumentCharge.regulationDocumentID);
            ViewBag.chargeTax = SetTaxList();
            ViewBag.chargeTitle = SetTitleList();

            return View(regulationDocumentCharge);
        }

        // GET: RegulationDocumentCharges/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegulationDocumentCharge regulationDocumentCharge = db.tblRegulationDocumentCharges.Find(id);
            if (regulationDocumentCharge == null)
            {
                return HttpNotFound();
            }
            return View(regulationDocumentCharge);
        }

        // POST: RegulationDocumentCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegulationDocumentCharge regulationDocumentCharge = db.tblRegulationDocumentCharges.Find(id);
            var returnID = db.tblRegulationDocuments.Find(regulationDocumentCharge.regulationDocumentID).regulationID;
            db.tblRegulationDocumentCharges.Remove(regulationDocumentCharge);
            db.SaveChanges();
            return RedirectToAction("Details", "Regulations", routeValues: new { id = returnID });
        }

        public ActionResult Charges(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var regulationDocumentsCharges = db.tblRegulationDocumentCharges.Where(d => d.regulationDocuments.regulationID == id).OrderBy(o => o.regulationDocumentCostID);
            
            return PartialView("_ViewRegulationDocumentCharges", regulationDocumentsCharges.ToList());
        }

        public ActionResult SumItemCharges(int id)
        {
            var regulationDocumentCharges = db.tblRegulationDocumentCharges.Where(i => i.regulationDocumentID == id).ToList();
            decimal sum;
            if (regulationDocumentCharges.Count != 0)
            {
                sum = regulationDocumentCharges.Sum(s => s.regulationDocumentFee);
            }
            else
            {
                sum = 0;
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
