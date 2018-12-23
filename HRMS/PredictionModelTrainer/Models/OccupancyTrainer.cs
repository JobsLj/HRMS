using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class OccupancyTrainer
    {
        public string Date { get; set; }
        public float Occupied { get; set; }
        public float TotalRoom { get; set; }
        public float RoomInventory { get; set; }
        public float AssignedReservation { get; set; }
        public float UnassignedReservation { get; set; }
        public float AdultNo { get; set; }
        public float ChildrenNo { get; set; }
        public float Arriving { get; set; }
        public float Next { get; set; }
    }

    public class OccupancyPrediction
    {
        public float Score;
    }
}
