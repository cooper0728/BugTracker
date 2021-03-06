﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System;

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

        public UserRolesHelper()
        {
        }

        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }

        public IList<string> AssignedUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        }

        public IList<string> ListAbsentUserRoles(string userId)
        {
            var roles = db.Roles.Select(r=>r.Name).ToList();
            var AbsentUserRoles = new List<string>();
            foreach(var role in roles)
            {
                if (!IsUserInRole(userId, role))
                {
                    AbsentUserRoles.Add(role);
                }
            }
            return AbsentUserRoles;
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

        public IList<ApplicationUser> UsersinRole(string roleName)
        {
            var userIDs = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
            return userManager.Users.Where(u => userIDs.Contains(u.Id)).ToList();
        }

        internal void AddUserToRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicationUser> UsersNotInRole(string roleName)
        {
            var userIDs = System.Web.Security.Roles.GetUsersInRole(roleName);
            return userManager.Users.Where(u => !userIDs.Contains(u.Id)).ToList();

        }


    }
}