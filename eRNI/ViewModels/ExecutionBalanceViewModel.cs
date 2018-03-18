using eRNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eRNI.ViewModels
{
    public class ExecutionBalanceViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Project> Projects { get; set; }

        public ExecutionBalanceViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Users = db.Users.ToList();
            Projects = db.tblProjects.ToList();
        }
    }
}