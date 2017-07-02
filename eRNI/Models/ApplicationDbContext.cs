﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace eRNI.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
          : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Action> tblActions { get; set; }
        public virtual DbSet<DeviceCategory> tblDeviceCategories { get; set; }
        public virtual DbSet<Device> tblDevices { get; set; }
        public virtual DbSet<Invoice> tblInvoices { get; set; }
        public virtual DbSet<Localization> tblLocalizations { get; set; }
        public virtual DbSet<Ownership> tblOwnerships { get; set; }
        public virtual DbSet<Owner> tblOwners { get; set; }
        public virtual DbSet<Place> tblPlaces { get; set; }
        public virtual DbSet<ProjectCategory> tblProjectCategories { get; set; }
        public virtual DbSet<Project> tblProjects { get; set; }
        public virtual DbSet<PropertyDocument> tblPropertyDocuments { get; set; }
        public virtual DbSet<ReguationCategory> tblReguationCategories { get; set; }
        public virtual DbSet<RegulationDocument> tblRegulationDocuments { get; set; }
        public virtual DbSet<Regulation> tblRegulations { get; set; }
        public virtual DbSet<Street> tblStreets { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
    }
}