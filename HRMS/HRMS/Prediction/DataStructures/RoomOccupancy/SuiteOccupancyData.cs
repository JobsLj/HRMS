using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Prediction.DataStructures.RoomOccupancy
{
    public class SuiteOccupancyData
    {
        public SuiteOccupancyData(DateTime date, float occupied, float totalroom, float prev)
        {
            this.Date = date;
            this.Occupied = occupied;
            this.TotalRoom = totalroom;
            this.Prev = prev;
        }

        public float Next;

        public DateTime Date;

        public float Occupied;
        public float TotalRoom;
        public float Prev;
    }
}
