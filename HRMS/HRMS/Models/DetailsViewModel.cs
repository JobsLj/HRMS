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
        public string SprRate { get; set; }
        public string StdRate { get; set; }
        public string FamRate { get; set; }
        public string SuiteRate { get; set; }
        public string DlxRate { get; set; }
        public string yestAdr { get; set; }
        public string yestRevPar { get; set; }
        public string yestOccupancy { get; set; }
        public string prevMonthAdr { get; set; }
        public string prevMonthRevPar { get; set; }
        public string prevMonthOccupancy { get; set; }
        public string prevYearAdr { get; set; }
        public string prevYearRevPar { get; set; }
        public string prevYearOccupancy { get; set; }
        public int selectedPlan { get; set; }
        public string defaultAdr { get; set; }
        public string defaultRevpar { get; set; }
    }
}
