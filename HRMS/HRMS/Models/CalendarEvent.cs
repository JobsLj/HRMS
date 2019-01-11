using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models
{
    public class CalendarEvent
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string RevPar { get; set; }
        public string Occupancy { get; set; }
        public string Adr { get; set; }
        public string Percent { get; set; }
        public int SelectedPlan { get; set; }
    }
}
