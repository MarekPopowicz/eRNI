using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace eRNI.Controllers
{
    [Authorize]
    public class MenuFiltersController : BaseController
    {

        public PartialViewResult ProjectCategoryMenu()
        {
            List<ProjectCategory> projects = db.tblProjectCategories.OrderBy(p => p.projectCategryName).ToList();
            return PartialView("~/Views/Shared/_ProjectCategoriesMenu.cshtml", projects);
        }

        public PartialViewResult DeviceCategoryMenu()
        {
            List<DeviceCategory> devices = db.tblDeviceCategories.OrderBy(p => p.deviceCategoryName).ToList();
            return PartialView("~/Views/Shared/_DeviceCategoriesMenu.cshtml", devices);
        }

        public PartialViewResult RegulationCategoryMenu()
        {
            List<ReguationCategory> regulations = db.tblReguationCategories.OrderBy(p => p.regulationCategoryName).ToList();
            return PartialView("~/Views/Shared/_RegulationCategoriesMenu.cshtml", regulations);
        }

        public PartialViewResult ProjectLeaderMenu()
        {
            List<ApplicationUser> users = db.Users.OrderBy(u => u.UserName).ToList();
            return PartialView("~/Views/Shared/_ProjectLeadersMenu.cshtml", users);
        }

        public PartialViewResult KeyDocumentCategoryMenu()
        {
            List<KeyDocumentCategory> keyDocument = db.tblKeyDocumentCategories.OrderBy(p => p.keyDocumentName).ToList();
            return PartialView("~/Views/Shared/_KeyDocumentCategoriesMenu.cshtml", keyDocument);
        }

       


        // GET: MenuFilters
        public ActionResult ProjectStatus(int id)
        {
            var tblProjects = (db.tblProjects.Include(p => p.projectCategory)).Where(s => s.projectStatus == (Models.Status)id);
            return View("~/Views/Projects/Index.cshtml", tblProjects.ToList());
        }

        public ActionResult ExecutionGroup(string id)
        {
            var tblProjects = (db.tblProjects.Include(p => p.projectCategory)).Where(s => s.projectSapNo.Contains(id));
            return View("~/Views/Projects/Index.cshtml", tblProjects.ToList());
        }

        public ActionResult ProjectCategory(string id)
        {
            var tblProjects = (db.tblProjects.Include(p => p.projectCategory)).Where(p => p.projectCategory.projectCategryName.Contains(id));
            return View("~/Views/Projects/Index.cshtml", tblProjects.ToList());
        }

        public ActionResult Localization(string id)
        {
            IQueryable<Localization> tblLocalizations;
            if (id =="Wrocław")
                tblLocalizations = (db.tblLocalizations.Include(l => l.place).Include(l => l.project)).Where(l => l.place.placeName.StartsWith(id));
            else
                tblLocalizations = (db.tblLocalizations.Include(l => l.place).Include(l => l.project)).Where(l => l.place.placeName.Contains(id));

            return View("~/Views/Localizations/Index.cshtml", tblLocalizations.ToList());
        }

        public ActionResult DeviceCategory(string id)
        {
            var tblDevices = (db.tblDevices.Include(d => d.deviceCategory).Include(d => d.localization)).Where(d => d.deviceCategory.deviceCategoryName.Contains(id));
            return View("~/Views/Devices/Index.cshtml", tblDevices.ToList());
        }

        public ActionResult DeviceVoltage(int id)
        {
            var tblDevices = (db.tblDevices.Include(d => d.deviceCategory).Include(d => d.localization)).Where(d => d.deviceVoltage == (Models.Voltage)id);
            return View("~/Views/Devices/Index.cshtml", tblDevices.ToList());
        }

        public ActionResult DocumentType(string id)
        {
            var tblRegulationDocuments = (db.tblRegulationDocuments.Include(r => r.regulation)).Where(r => r.regulationDocumentType == id);
            return View("~/Views/RegulationDocuments/Index.cshtml", tblRegulationDocuments.ToList());
        }

        public ActionResult Regulation(string id)
        {
            var tblRegulations = (db.tblRegulations.Include(r => r.device).Include(r => r.reguationCategory)).Where(r => r.reguationCategory.regulationCategoryName == id);
            return View("~/Views/Regulations/Index.cshtml", tblRegulations.ToList());
        }

        public ActionResult PropertyDocument(string id)
        {
            var tblPropertyDocuments = (db.tblPropertyDocuments.Include(p => p.project)).Where(p => p.propertyDocumentType == id);
            return View("~/Views/PropertyDocuments/Index.cshtml", tblPropertyDocuments.ToList());
        }

        public ActionResult ProjectLeader(string id)
        {
            var tblProjects = (db.tblProjects.Include(p => p.projectCategory)).Where(u => u.projectLeader == id);
            return View("~/Views/Projects/Index.cshtml", tblProjects.ToList());
        }

        public ActionResult KeyDocument(string id)
        {
            var tblKeyDocuments = (db.tblKeyDocuments.Include(r => r.keyDocumentCategory)).Where(r => r.keyDocumentCategory.keyDocumentName == id);
            return View("~/Views/KeyDocuments/Index.cshtml", tblKeyDocuments.ToList());
        }

        public ActionResult Activity(int id)
        {
            List<Activity> tblActions = new List<Activity>();
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);

            if (id > 0)
            {
                var first = month.AddMonths(-id);
                var last = month.AddDays(-1);
                tblActions = (db.tblActions.Where(p => p.actionDate >= first && p.actionDate <= last).ToList());
            }
            else
            {
                var first = month.AddMonths(-id);
                var last = month.AddMonths(1).AddDays(-1);
                tblActions = (db.tblActions.Where(p => p.actionDate >= first && p.actionDate <= last).ToList());
            }
            
            return View("~/Views/Activities/Index.cshtml", tblActions);
        }


    }
}