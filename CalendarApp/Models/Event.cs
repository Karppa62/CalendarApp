using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public float Duration { get; set; }
    }
}