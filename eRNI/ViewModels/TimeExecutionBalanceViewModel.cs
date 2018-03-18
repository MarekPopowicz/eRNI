using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eRNI.ViewModels
{
    public class TimeExecutionBalanceViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Project> InflowProjects { get; set; }
        public List<Project> DoneProjects { get; set; }
        public string TimeOffset;

        public TimeExecutionBalanceViewModel(int timeOffset)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-timeOffset);
            var last = month.AddDays(-1);

            ApplicationDbContext db = new ApplicationDbContext();
            Users = db.Users.ToList();
            InflowProjects = db.tblProjects.Where(p => p.projectInflow >= first && p.projectInflow <= last).ToList();
            DoneProjects = db.tblProjects.Where(p => p.projectClosed >= first && p.projectClosed <= last).ToList();
            TimeOffset = "od " + String.Format("{0:yyyy-MM-dd}",first) + " do " + String.Format("{0:yyyy-MM-dd}", last);
        }
    }
}