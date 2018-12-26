using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class Rates
    {
        public DateTime Date { get; set; }
        public float Next { get; set; }
        public float Prev { get; set; }
        public float Amount { get; set; }
    }

    public class Prediction
    {
        public float Score;
    }
}
