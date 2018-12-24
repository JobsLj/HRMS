using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class OccupancyTrainer
    {
        public DateTime Date { get; set; }
        public float Occupied { get; set; }
        public float TotalRoom { get; set; }
        public float Next { get; set; }
        public float Prev { get; set; }
    }

    public class OccupancyPrediction
    {
        public float Score;
    }
}
