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
        public int RoomPrice { get; set; }
    }
}
