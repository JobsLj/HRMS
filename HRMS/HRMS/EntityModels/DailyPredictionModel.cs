using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.EntityModels
{
    public class DailyPredictionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float StdRoomRate { get; set; }
        public float SprRoomRate { get; set; }
        public float FamRoomRate { get; set; }
        public float SuiteRoomRate { get; set; }
        public float DlxRoomRate { get; set; }
        public float StdOccupancy { get; set; }
        public float SprOccupancy { get; set; }
        public float FamOccupancy { get; set; }
        public float SuiteOccupancy { get; set; }
        public float DlxOccupancy { get; set; }
        public int SelectedRoomRate { get; set; }
        public float AdjStdRoomRate { get; set; }
        public float AdjSprRoomRate { get; set; }
        public float AdjFamRoomRate { get; set; }
        public float AdjSuiteRoomRate { get; set; }
        public float AdjDlxRoomRate { get; set; }
    }
}
