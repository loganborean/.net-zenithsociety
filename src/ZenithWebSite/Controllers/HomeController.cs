using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenithWebSite.Data;
using ZenithWebSite.Services;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Models;

namespace ZenithWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private EventWeekService eventWeekService { get; set; }


        public HomeController(ApplicationDbContext context, EventWeekService eventWeekService)
        {
            db = context;
            this.eventWeekService = eventWeekService;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Event.Include(a => a.Activity);
            List<Event> allEvents = await applicationDbContext.ToListAsync();
            Dictionary<string, List<EventUi>> eventsByWeek = eventWeekService.getThisWeeksEvents(allEvents);

            return View(eventsByWeek);
        }

    }
}
