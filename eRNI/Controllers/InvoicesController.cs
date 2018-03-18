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
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        public ActionResult Index()
        {
            var tblInvoices = db.tblInvoices.Include(i => i.projects);
            return View(tblInvoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.tblInvoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == id).ToList(), "projectID", "projectSapNo");
            ViewBag.returnID = id;
            List<SelectListItem> items = new List<SelectListItem>
            { 
                new SelectListItem { Text = "23%", Value = "23,00", Selected = true },
                new SelectListItem { Text = "22%", Value = "22,00" },
                new SelectListItem { Text = "7%", Value = "7,00" },
                new SelectListItem { Text = "0%", Value = "0,00" }
            };

            ViewBag.TaxRate = items;

            return View();
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invoiceID," +
                                                   "invoiceAdditionalInfo," +
                                                   "invoiceIssueDate," +
                                                   "invoiceNettoValue," +
                                                   "invoiceTax," +
                                                   "invoiceNo," +
                                                   "invoiceSapRegisterNo," +
                                                   "invoiceSapRegistrationDate," +
                                                   "invoiceSellerName," +
                                                   "invoiceTitle," +
                                                   "projectID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.tblInvoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = invoice.projectID });
            }

            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", invoice.projectID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.tblInvoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.projectID = new SelectList(db.tblProjects.Where(x => x.projectID == invoice.projectID).ToList(), "projectID", "projectSapNo", invoice.projectID);
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "23%", Value = "23,00" },
                new SelectListItem { Text = "22%", Value = "22,00" },
                new SelectListItem { Text = "7%", Value = "7,00" },
                new SelectListItem { Text = "0%", Value = "0,00" }
            };

            ViewBag.TaxRate = items;
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invoiceID," +
                                                 "invoiceAdditionalInfo," +
                                                 "invoiceIssueDate," +
                                                 "invoiceNettoValue," +
                                                 "invoiceTax,invoiceNo," +
                                                 "invoiceSapRegisterNo," +
                                                 "invoiceSapRegistrationDate," +
                                                 "invoiceSellerName," +
                                                 "invoiceTitle," +
                                                 "projectID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = invoice.projectID });
            }
            ViewBag.projectID = new SelectList(db.tblProjects, "projectID", "projectSapNo", invoice.projectID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.tblInvoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.tblInvoices.Find(id);
            db.tblInvoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Details", "Projects", new { id = invoice.projectID });
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
