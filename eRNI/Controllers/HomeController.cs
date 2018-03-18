using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRNI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "(c) 2017 Marek Popowicz";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Marek Popowicz";

            return View();
        }
    }
}