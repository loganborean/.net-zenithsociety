using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Models;
using ZenithWebSite.Services;
using ZenithWebSite.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ZenithSociety.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private EventWeekService eventWeekService { get; set; }

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public EventsController(ApplicationDbContext context, EventWeekService eventWeekService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.eventWeekService = eventWeekService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Event.Include(a => a.Activity);
            List<Event> allEvents = await applicationDbContext.ToListAsync();

            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = _context.Users.FirstOrDefault(user => user.Id == userId);

            var adminRole = await roleManager.FindByNameAsync("Admin");

            if (await userManager.IsInRoleAsync(currentUser, adminRole.Name))
            {
                return View("Index", await applicationDbContext.ToListAsync());

            }

            Dictionary<string, List<EventUi>> eventsByWeek = eventWeekService.getThisWeeksEvents(allEvents);
            return View("unAuthenticatedIndex", eventsByWeek);
        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityId", "ActivityId");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,ActivityId,CreationDate,EnteredByUsername,EventFromDateTime,EventToDateTime,IsActive")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityId", "ActivityId", @event.ActivityId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityId", "ActivityId", @event.ActivityId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,ActivityId,CreationDate,EnteredByUsername,EventFromDateTime,EventToDateTime,IsActive")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ActivityId"] = new SelectList(_context.Activity, "ActivityId", "ActivityId", @event.ActivityId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.SingleOrDefaultAsync(m => m.EventId == id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}
