using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class BaseController: Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}