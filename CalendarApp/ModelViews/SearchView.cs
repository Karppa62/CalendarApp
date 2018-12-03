using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.ModelViews
{
    public class SearchView
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Time { get; set; }
        public string Search { get; set; }
    }
}