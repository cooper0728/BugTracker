using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//namespace BugTracker.Controllers
//{
//    public class RoleController : Controller
//    {
//        public ActionResult EditRole(string Id, List<string> SelectedRoles)
//        {
//            if (ModelState.IsValid)
//            {
//                UserRolesHelper helper = new UserRolesHelper(db);
//                var user = db.Users.Find(Id);
//                //Add new roles
//                foreach (var role in SelectedRoles)
//                {
//                    //If Role does not already exist for user
//                    helper.AddUserToRole(Id, role);
//                }

//                //REMOVE ROLES
//                //Loop through all roles
//                //if role not in SelectedRoles:
//                //helper.RemoveUserFromRole(Id,role)

//                db.Entry(user).State = EntityState.Modified;
//                db.Users.Attach(user);
//                db.SaveChanges();
//                return RedirectToAction("ListUsers");
//            }
//            return View(Id);
//        }
//    }
//}
