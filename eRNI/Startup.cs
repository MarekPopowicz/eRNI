﻿using eRNI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eRNI.Startup))]
namespace eRNI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        private void createRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Utworzenie domyślnego Administratora   
            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Marek Popowicz";
                user.Email = "marek.popowicz@tauron-dystrybucja.pl";

                string userPWD = "P@ssw0rd";

                var chkUser = userManager.Create(user, userPWD);

                //Dodanie użytkownika do roli administratora 
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Administrator");
                }
            }

           
            // Utworzenie roli usera    
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
