using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Admin/EditUser
        // **** See Handout for Completed Example ****
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            var model = db.Users.ToList();
            return View(model);
        }

        // GET: /Admin/EditUser
        // **** See Handout for Completed Example ****
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string id)
        {
            var user = db.Users.Find(id);
            var roles = db.Roles.ToList();

            UserRolesHelper helper = new UserRolesHelper(db);
            var currentRoles = helper.AssignedUserRoles(id);
           
            //Create a list of roles that we are not assigned too.
            var absentRoles = new List<string>();
            foreach (var role in roles)
            {
                if(!helper.IsUserInRole(id, role.Name))
                {
                    absentRoles.Add(role.Name);
                }    
            }

            AdminUserViewModel AdminModel = new AdminUserViewModel();
            AdminModel.Roles = new MultiSelectList(currentRoles);
            AdminModel.AbsentRoles = new MultiSelectList(absentRoles);
            AdminModel.User = user;
            return View(AdminModel);
        }

        // POST: Add User Role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddRole(string AddId, List<string>SelectedAbsentRoles)
        {

            if (ModelState.IsValid)
            {
                // Loop through roles, add each user to role
                UserRolesHelper helper = new UserRolesHelper(db);
                var user = db.Users.Find(AddId);
                foreach(var role in SelectedAbsentRoles)
                {
                    //helper is the object of UserRolesHelper
                    helper.AddUserToRole(AddId, role);
                }

                db.Entry(user).State = EntityState.Modified;
                db.Users.Attach(user);
                db.SaveChanges();
                return RedirectToAction("AdminDashboard");

            }
            return View("AdminDashboard");
        }

        // POST: Remove User Role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RemoveRole(string RemoveId, List<string> SelectedCurrentRoles)
        {
            if (ModelState.IsValid)
            {
                // Loop through roles, remove each role from user
                UserRolesHelper helper = new UserRolesHelper(db);
                var user = db.Users.Find(RemoveId);
                foreach (var role in SelectedCurrentRoles)
                {
                    helper.RemoveUserFromRole(RemoveId, role);
                }

                db.Entry(user).State = EntityState.Modified;
                db.Users.Attach(user);
                db.SaveChanges();
                return RedirectToAction("AdminDashboard");

            }
            return View("AdminDashboard");
        }
    }
}











    
