using eRNI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
       {
       }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Role
        public ActionResult Index()
        {
            
            List<RoleViewModel> list = new List<RoleViewModel>();
            
            foreach (var role in RoleManager.Roles )
           {
             list.Add(new RoleViewModel(role));
            }
            TempData["info"] = TempData["msg"];
            return View(list);
        }

        // Create: Role
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() { Name = model.Name };
            await RoleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        // Edit: Role
        public async Task<ActionResult> Edit(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }


        // Details: Role
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }


        // Delete: Role
        public async Task<ActionResult> Delete(string id)
        {
            var isUsed = RoleManager.Roles.Where(r => r.Id == id).Select(u => u.Users);
            if (isUsed.Count() == 0)
            {
                var role = await RoleManager.FindByIdAsync(id);
                return View(new RoleViewModel(role));
            }
            else
                TempData["msg"] = "Operacja usunięcia jest niedozwolona, gdy rola jest przypisana do użytkownika(ów).";
                return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}