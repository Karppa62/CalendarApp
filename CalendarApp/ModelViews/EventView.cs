using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarApp.ModelViews
{
    public class EventView
    {
        public string EventName { get; set; }

        public string DateAndTime { get; set; }

        public float Duration { get; set; }
    }
}