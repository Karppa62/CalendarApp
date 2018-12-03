using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CalendarApp.Models;
using CalendarApp.ModelViews;

namespace CalendarApp.Controllers
{
    [RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        private CalendarAppContext db = new CalendarAppContext();

        /// <summary>
        /// Finds all events from the database
        /// </summary>
        /// <returns>all events</returns>
        // GET: api/Event
        [Route("")]
        public IQueryable<Event> GetEvents()
        {           
            var events = db.Events;
            return events;
        }

        /// <summary>
        /// Searches events based on search parameters
        /// </summary>
        /// <param name="search">Search parameters</param>
        /// <returns>events that fulfill search parameters</returns>  
        // GET: api/Event/search
        [Route("search")]
        public IQueryable<Event> GetSearchEvents(SearchView search)
        {
            if (search == null)
                return db.Events;

            int hours = 0;
            int minutes = 0;
            if (search.Time != null && search.Time != "")
            {
                try
                {
                    string[] values = search.Time.Split(':');
                    hours = int.Parse(values[0]);
                    minutes = int.Parse(values[1]);   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            var events = db.Events
                .Where(e => search.Year != 0 ? e.DateTime.Year == search.Year : true)
                .Where(e => search.Month != 0 ? e.DateTime.Month == search.Month : true)
                .Where(e => search.Day != 0 ? e.DateTime.Day == search.Day : true)
                .Where(e => (search.Time != null && search.Time != "") ? e.DateTime.Hour == hours && e.DateTime.Minute == minutes : true)
                .Where(e => search.Search != "" ? e.EventName.ToLower().Contains(search.Search.ToLower()) : true);    
            
            return events;
        }

        /// <summary>
        /// Adds a new event to database
        /// </summary>
        /// <param name="eventView">New event's values</param>
        /// <returns>Ok and the added new event</returns>
        // POST: api/Event
        [Route("")]
        [ResponseType(typeof(Event))]
        public async Task<IHttpActionResult> PostNewEvent(EventView eventView)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DateTime dateTime;
            try
            {
                char[] delimiters = new char[] { '.', ':', ' ' };
                string[] values = eventView.DateAndTime.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                List<int> numbers = new List<int>();
                
                foreach (string value in values)
                    numbers.Add(int.Parse(value));

                dateTime = new DateTime(numbers[2], numbers[1], numbers[0], numbers[3], numbers[4], 0);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            Event newEvent = new Event{
                EventName = eventView.EventName,
                DateTime = dateTime,
                Duration = eventView.Duration };

            db.Events.Add(newEvent);

            await db.SaveChangesAsync();

            return Ok(newEvent);//CreatedAtRoute("DefaultApi", new { id = newEvent.Id }, newEvent);
        }      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }
    }
}