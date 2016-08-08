using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Models
{
    public class UserRolesHelper
    {
        //we need 1) a database context, 2) a UserManager object, and 3) a RoleManager
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserRolesHelper(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            this.roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            this.db = context;

        }
            
        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public IList<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = userManager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var result = userManager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }      

        //public ICollection<UserDropDownViewModel> UsersinRole(string roleName)
        //{
        //    var userIDs = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
        //    return userManager.Users.Where(u => userIDs.Contains(u.Id)).Select(u =>
        //     new UserDropDownViewModel { Id = u.Id, Name = u.DisplayName }).ToList();
        //}











    }
}