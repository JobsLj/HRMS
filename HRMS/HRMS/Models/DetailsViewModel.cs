using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models
{
    public class DetailsViewModel
    {
        public DateTime Date { get; set; }
        public string RevPar { get; set; }
        public string Occupancy { get; set; }
        public string Adr { get; set; }
        public string prevMonthAdr { get; set; }
        public string prevMonthRevPar { get; set; }
        public string prevMonthOccupancy { get; set; }
        public string prevYearAdr { get; set; }
        public string prevYearRevPar { get; set; }
        public string prevYearOccupancy { get; set; }
    }
}
