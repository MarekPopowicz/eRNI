using eRNI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eRNI.ViewModels
{
    public class ProjectBurdenViewModel
    {
        public  List<ApplicationUser> Users { get; set; }
        public List<Project> Projects { get; set; }

        public ProjectBurdenViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Users = db.Users.ToList();
            Projects = db.tblProjects.Where(p => p.projectStatus == Status.Przygotowanie || p.projectStatus == Status.Realizacja).ToList();
        }
    }
}