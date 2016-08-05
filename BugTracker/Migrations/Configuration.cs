using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }

            //Replicate this structure for new roles

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "cooper0728@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "cooper0728@gmail.com",
                    Email = "cooper0728@gmail.com",
                    FirstName = "Charles",
                    LastName = "Cooper",
                    DisplayName = "COOPER0728"
                }, "Allen1981!");
            }

            if (!context.Users.Any(u => u.Email == "jtwichell@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jtwichell@coderfoundry.com",
                    Email = "jtwichell@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "J-Twich"
                }, "Abc&123!");
            }


            var userId = userManager.FindByEmail("cooper0728@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            var juserId = userManager.FindByEmail("jtwichell@coderfoundry.com").Id;
            userManager.AddToRole(juserId, "Project Manager");

            var duserId = userManager.FindByEmail("jtwichell@coderfoundry.com").Id;
            userManager.AddToRole(juserId, "Developer");

            var suserId = userManager.FindByEmail("jtwichell@coderfoundry.com").Id;
            userManager.AddToRole(juserId, "Submitter");




        }




    }
}

