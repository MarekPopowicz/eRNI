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
    public class AddressBooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddressBooks
        public ActionResult Index()
        {
            var tblAddressBook = db.tblAddressBook.Include(a => a.street);
            return View(tblAddressBook.ToList());
        }

        // GET: AddressBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressBook addressBook = db.tblAddressBook.Find(id);
            if (addressBook == null)
            {
                return HttpNotFound();
            }
            return View(addressBook);
        }

        // GET: AddressBooks/Create
        public ActionResult Create()
        {
            ViewBag.Streets = StreetList();
            return View();
        }


        private List<SelectListItem> StreetList()
        {
            List<SelectListItem> street = new List<SelectListItem>();
            SelectListItem defaultItem = new SelectListItem() { Value = null, Text = string.Empty, Selected = true };
            street = db.tblStreets.ToList().Select(u => new SelectListItem
            {
                Text = u.streetName,
                Value = u.streetID.ToString()
            }).ToList();
            street.Add(defaultItem);

            return street.OrderBy(o => o.Text).ToList();
        }


        // POST: AddressBooks/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "addresseeID," +
            "addresseeName," +
            "addresseeHouseNo," +
            "addresseeAptNo," +
            "addresseePhone," +
            "addresseeCellPhone," +
            "addresseePostOffice," +
            "addresseeZipCode," +
            "addresseeEmail," +
            "addresseeCity," +
            "streetID")] AddressBook addressBook)
        {
            if (ModelState.IsValid)
            {
                db.tblAddressBook.Add(addressBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Streets = new SelectList(db.tblStreets, "streetID", "streetName", addressBook.streetID);
            return View(addressBook);
        }

        // GET: AddressBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressBook addressBook = db.tblAddressBook.Find(id);
            if (addressBook == null)
            {
                return HttpNotFound();
            }
              ViewBag.Streets = StreetList();

            if (addressBook.street == null)
            {
                ViewBag.streetName = string.Empty;
            }
            else
            {
                ViewBag.streetName = StreetList().Where(s => s.Text.Equals(addressBook.street.streetName)).FirstOrDefault();
            }


            return View(addressBook);
        }

        // POST: AddressBooks/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "addresseeID," +
            "addresseeName," +
            "addresseeHouseNo," +
            "addresseeAptNo," +
            "addresseePhone," +
            "addresseeCellPhone," +
            "addresseePostOffice," +
            "addresseeZipCode," +
            "addresseeEmail," +
            "addresseeCity," +
            "streetID")] AddressBook addressBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addressBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Streets = new SelectList(db.tblStreets, "streetID", "streetName", addressBook.streetID);
            return View(addressBook);
        }

        // GET: AddressBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressBook addressBook = db.tblAddressBook.Find(id);
            if (addressBook == null)
            {
                return HttpNotFound();
            }
            return View(addressBook);
        }

        // POST: AddressBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddressBook addressBook = db.tblAddressBook.Find(id);
            db.tblAddressBook.Remove(addressBook);
            db.SaveChanges();
            return RedirectToAction("Index");
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
