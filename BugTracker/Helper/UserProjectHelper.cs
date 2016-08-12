using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class UserProjectsHelper
    {
        //we need 1) a database context, 2) a UserManager object, and 3) a RoleManager
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserProjectsHelper(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            this.roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            this.db = context;

        }

        public bool IsUserInProject(string userId, int projectId)
        {
            return userManager.IsInProject(userId, projectId);
        }


        public bool AddUserToProject(string userId, int projectId)
        {
            if (!IsUserInProject(userId, projectId))
            {
                var user = userManager.FindById(userId);
                db.Projects.Find(projectId).ToStringAdd(user);
                db.SaveChanges();
            }
            return IsUserInProject(userId, projectId);
        }

        //remove user from Project
        public bool RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserInProject(userId, projectId))
            {
                var user = userManager.FindById(userId);
                db.Projects.Find(projectId).roleName.Remove(user);
                db.SaveChanges();
            }
            return !IsUserInProject(userId, projectId);

        }

        //get user Project
        public IList<int> GetUserProjects(string userId)
        {
            return userManager.FindById(userId).DevProjects.Select(p => p.Id).ToList();
        }

        //get users in Project
        public IList<string> GetUsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Developers.Select(u => u.Id).ToList();
        }

        //get users in Project
        public IList<string> GetUsersNotOnProject(int projectId)
        {
            var usersInProject = GetUsersOnProject(projectId);
            var developers = roleManager.FindByName(roleName).Users.Select(u => u.UserId).ToList();
            return developers.Where(id => !usersInProject.Contains(id)).ToList();
        }

        //get users not in any Project
        public IList<string> GetUsersNotOnAnyProject()
        {
            var devRoleId = roleManager.FindByName("Developer").Id;
            var developers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == devRoleId));
            return developers.Where(u => u.DevProjects.Count == 0).Select(um => um.Id).ToList();
        }
    }
}