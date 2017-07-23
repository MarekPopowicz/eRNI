using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace eRNI.Controllers
{
    public class MenuFiltersController : BaseController
    {
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
            var tblLocalizations = (db.tblLocalizations.Include(l => l.place).Include(l => l.project)).Where(l => l.place.placeName.Contains(id));
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
    }
}