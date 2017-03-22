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

namespace ZenithWebSite.Controllers
{
    public class RoleController : Controller
    {

        private readonly ApplicationDbContext db;

        public RoleController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Role
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


        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}