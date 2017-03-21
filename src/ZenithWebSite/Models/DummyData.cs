using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebSite.Models;
using ZenithWebSite.Data;

namespace ZenithWebSite.Models
{
    public class DummyData
    {
        public static void Initialize(ApplicationDbContext db)
        {

            //var thing = db.Users.FirstOrDefault(u => u.UserName == "b");
            //db.Users.Remove(thing);
            //db.SaveChanges();
            SeedUsers(db);
            foreach (Activity activity in getActivities())
            {
                db.Add(activity);
            }
            db.SaveChanges();

            foreach (Event myEvent in getEvents(db))
            {
                db.Add(myEvent);
            }
            db.SaveChanges();
        }
        public static void SeedUsers(ApplicationDbContext _context)
        {
            var admin = new ApplicationUser
            {
                UserName = "a",
                Email = "a@a.a",
                EmailConfirmed = true,
                NormalizedEmail = "A@A.A",
                NormalizedUserName = "A",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var member = new ApplicationUser
            {
                UserName = "b",
                Email = "b@b.b",
                EmailConfirmed = true,
                NormalizedEmail = "B@B.B",
                NormalizedUserName = "B",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            }

            if (!_context.Roles.Any(r => r.Name == "Member"))
            {
                roleStore.CreateAsync(new IdentityRole { Name = "Member", NormalizedName = "MEMBER" });
            }

            if (!_context.Users.Any(u => u.UserName == "a"))
            {

                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(admin, "P@$$w0rd");
                admin.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                userStore.CreateAsync(admin);
                userStore.AddToRoleAsync(admin, "ADMIN");

            }


            if (!_context.Users.Any(u => u.UserName == "b"))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(member, "P@$$w0rd");
                member.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                userStore.CreateAsync(member);
                userStore.AddToRoleAsync(member, "MEMBER");

            }

            _context.SaveChangesAsync();
        }

        private static Event[] getEvents(ApplicationDbContext context)
        {
            List<Event> events = new List<Event>()
            {

                new Event()
                {
                    EventFromDateTime= Convert.ToDateTime("2017/03/18 6:00"),
                    EventToDateTime= Convert.ToDateTime("2017/03/18 12:00"),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Golf").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= Convert.ToDateTime("2017/03/17 13:00"),
                    EventToDateTime= Convert.ToDateTime("2017/03/17 18:00"),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Golf").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "otherguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Golf").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Swimming").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Swimming").ActivityId,
                    Activity = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Swimming"),
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Swimming").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Basketball").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Basketball").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                },
                new Event()
                {
                    EventFromDateTime= new DateTime(2017, 2, 3, 3, 3, 3),
                    EventToDateTime = new DateTime(2017, 2, 3, 3, 3, 3),
                    EnteredByUsername = "thatguy",
                    ActivityId = context.Activity.FirstOrDefault(a => a.ActivityDescription=="Basketball").ActivityId,
                    CreationDate = new DateTime(2017, 1, 3, 3, 3, 3),
                    IsActive = true
                }
            };
            return events.ToArray();
        }


        public static void DeleteAll(ApplicationDbContext db)
        {
            foreach (var entity in db.Activity)
            {
                db.Activity.Remove(entity);
            }

            db.SaveChanges();
            foreach (var entity in db.Event)
            {
                db.Event.Remove(entity);

            }
            db.SaveChanges();
        }

        private static Activity[] getActivities()
        {
            List<Activity> activities = new List<Activity>()
                {
                    new Activity()
                    {
                        ActivityDescription = "Basketball",
                        CreationDate = new DateTime(2017, 1, 1)
                    },
                    new Activity()
                    {
                        ActivityDescription = "Swimming",
                        CreationDate = new DateTime(2017, 1, 3)
                    },
                    new Activity()
                    {
                        ActivityDescription = "Golf",
                        CreationDate = new DateTime(2017, 1, 8)
                    }

                };
            return activities.ToArray();
        }
    }
}
