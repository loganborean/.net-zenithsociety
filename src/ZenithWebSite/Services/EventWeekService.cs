using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebSite.Models;

namespace ZenithWebSite.Services
{
    public class EventWeekService
    {
        public Dictionary<string, List<EventUi>> getThisWeeksEvents(List<Event> events)
        {
            List<Event> thisWeeksEvents = new List<Event>();


            DateTime currentDate = DateTime.Now;
            foreach (var singleEvent in events)
            {
                DateTime fromDateTime = singleEvent.EventFromDateTime;
                TimeSpan difference = currentDate - fromDateTime;

                System.Diagnostics.Debug.WriteLine(difference.TotalDays);

                if (difference.TotalDays <= 7 && singleEvent.IsActive)
                {
                    thisWeeksEvents.Add(singleEvent);
                }


            }
            return makeDays(thisWeeksEvents);
            //return thisWeeksEvents;
        }
        private Dictionary<string, List<EventUi>> makeDays(List<Event> events)
        {
            Dictionary<string, List<EventUi>> dict = new Dictionary<string, List<EventUi>>();
            foreach (Event singleEvent in events)
            {
                string day = String.Format("{0:dddd, MMMM d, yyyy}", singleEvent.EventFromDateTime);

                if (dict.ContainsKey(day))
                {
                    dict[day].Add(makeEventUiFromEvent(singleEvent));

                }
                else
                {
                    List<EventUi> eventsOnDay = new List<EventUi>();
                    eventsOnDay.Add(makeEventUiFromEvent(singleEvent));
                    dict[day] = eventsOnDay;
                }
            }
            return dict;
        }
        private EventUi makeEventUiFromEvent(Event singleEvent)
        {
            EventUi eventUi = new EventUi();
            eventUi.description = singleEvent.Activity.ActivityDescription;

            string fromTime = string.Format("{0:hh:mm tt}", singleEvent.EventFromDateTime);
            string toTime = string.Format("{0:hh:mm tt}", singleEvent.EventToDateTime);

            eventUi.dateFromTo = fromTime + " - " + toTime;

            return eventUi;
        }
    }
}
