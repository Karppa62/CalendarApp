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
    public class EventController : ApiController
    {
        private CalendarAppContext db = new CalendarAppContext();

        // GET: api/Event
        public IQueryable<Event> GetEvents()
        {           
            var events = db.Events;
            return events;
        }
               
        // POST: api/Event
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
            
            Event newEvent = new Event{ EventName = eventView.EventName, DateTime = dateTime, Duration = eventView.Duration };
            db.Events.Add(newEvent);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newEvent.Id }, newEvent);
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