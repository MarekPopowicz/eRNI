using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eRNI.ViewModels;

namespace eRNI.Controllers
{   [Authorize]
    public class StatisticsController:BaseController
    {

        public ActionResult ProjectBurden()
        {
            ProjectBurdenViewModel model = new ProjectBurdenViewModel();

            return View(model);
        }

        public ActionResult ExecutionBalance()
        {
            ExecutionBalanceViewModel model = new ExecutionBalanceViewModel();
            return View("ExecutionBalance", model);
        }

        public ActionResult lastMonth()
        {
            TimeExecutionBalanceViewModel model = new TimeExecutionBalanceViewModel(1);
            return View("TimeExecutionBalance", model);
        }

        public ActionResult lastQuarter()
        {
            TimeExecutionBalanceViewModel model = new TimeExecutionBalanceViewModel(3);
            return View("TimeExecutionBalance", model);
        }

        public ActionResult lastHalfYear()
        {
            TimeExecutionBalanceViewModel model = new TimeExecutionBalanceViewModel(6);
            return View("TimeExecutionBalance", model);
        }

        public ActionResult CostsBalance(int id = 0)
        {
            CostsBalanceViewModel model = new CostsBalanceViewModel(id);
            return View("CostsBalance",model);
        }

        public ActionResult SpentWorkingTimeCurrentMonth()
        {
            SpentWorkingTimeViewModel model = new SpentWorkingTimeViewModel(User.Identity.Name, 0);
            return View("SpentWorkingTime", model);
        }

        public ActionResult SpentWorkingTimeLastMonth()
        {
            SpentWorkingTimeViewModel model = new SpentWorkingTimeViewModel(User.Identity.Name, 1);
            return View("SpentWorkingTime", model);
        }

        public ActionResult SpentWorkingTimeMonthAfterLastMonth()
        {
            SpentWorkingTimeViewModel model = new SpentWorkingTimeViewModel(User.Identity.Name, 2);
            return View("SpentWorkingTime", model);
        }

    }
}