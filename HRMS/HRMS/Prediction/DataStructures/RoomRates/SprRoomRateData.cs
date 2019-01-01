using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Prediction.DataStructures.RoomRates
{
    public class SprRoomRateData
    {
        public SprRoomRateData(DateTime date, float prev, float amount)
        {
            this.Date = date;
            this.Prev = prev;
            this.Amount = amount;
        }

        public float Next;

        public DateTime Date;

        public float Prev;
        public float Amount;
    }
}
