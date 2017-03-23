using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZenithWebSite.Data;
using ZenithWebSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ZenithWebSite.Controllers
{
    public class UserRoleController : Controller
    {

        private readonly ApplicationDbContext db;

        public UserRoleController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ActionResult> Index()
        {
            var query = from User in db.Users
                        join UserRoles in db.UserRoles on User.Id equals UserRoles.UserId
                        join Roles in db.Roles on UserRoles.RoleId equals Roles.Id
                        select new { User, Roles };

            var thing = query.ToList();
            Dictionary <ApplicationUser, List<IdentityRole>> userAndRole 
                = new Dictionary<ApplicationUser, List<IdentityRole>>();

            foreach(var item in thing)
            {
                if (userAndRole.ContainsKey(item.User))
                {
                    userAndRole[item.User].Add(item.Roles);
                } else
                {
                    List<IdentityRole> newList = new List<IdentityRole>();
                    newList.Add(item.Roles);
                    userAndRole.Add(item.User, newList);
                }
            }

            return View(userAndRole);
        }

        // GET: Role/Add
        public async Task<ActionResult> Add(string id)
        {
            ViewData["id"] = id;
            List<IdentityRole> roles = db.Roles.ToList();
            Dictionary<string, IdentityRole> map = new Dictionary<string, IdentityRole>();

            foreach (IdentityRole role in roles)
            {
                map.Add(role.Id, role);
            }

            List<IdentityUserRole<string>> rolesNotHadByUser = 
                db.UserRoles.Where(role => role.UserId != id).ToList();

            List<IdentityRole> rolesHadByUser = new List<IdentityRole>();

            foreach (var role in rolesNotHadByUser)
            {
                if (map.ContainsKey(role.RoleId))
                {
                    rolesHadByUser.Add(map[role.RoleId]);
                }
            }

            return View(rolesHadByUser);
        }

        // POST: Role/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(IFormCollection collection)
        {
            try
            {
                string roleId = Request.Form["id"];
                string userId = Request.Form["userId"];

                IdentityRole role = db.Roles.FirstOrDefault(r => r.Id == roleId);
                if (role == null)
                {
                    throw new Exception();
                }
                var userStore = new UserStore<ApplicationUser>(db);
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                await userStore.AddToRoleAsync(user, role.NormalizedName);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(db.Roles.ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(IFormCollection collection)
        {
            try
            {
                string roleId = Request.Form["roleId"];
                string userId = Request.Form["userId"];

                if (roleId == null || userId == null) throw new Exception();

                IdentityRole role = db.Roles.FirstOrDefault(r => r.Id == roleId);
                var userStore = new UserStore<ApplicationUser>(db);
                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (role == null || user == null) throw new Exception();

                await userStore.RemoveFromRoleAsync(user, role.NormalizedName);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}