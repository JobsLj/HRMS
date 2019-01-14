using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Models
{
    public class WhatIfAnalysisViewModel
    {
        public DateTime Date { get; set; }
        public int RevPar { get; set; }
        public string Occupancy { get; set; }
        public string Adr { get; set; }
        public int SprRate { get; set; }
        public int StdRate { get; set; }
        public int FamRate { get; set; }
        public int SuiteRate { get; set; }
        public int DlxRate { get; set; }
        public List<string> DatesLabel { get; set; }
        public List<string> PredictedAdr { get; set; }
        public List<string> AdjustedAdr { get; set; }
        public List<string> PredictedRevPar { get; set; }
        public List<string> AdjustedRevPar { get; set; }
    }
}
