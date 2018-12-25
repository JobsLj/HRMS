using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class SprRates
    {
        public DateTime Date { get; set; }
        public float Next { get; set; }
        public float Prev { get; set; }
        public string RateCode { get; set; }
        public float Amount { get; set; }
        public float Avg { get; set; }
    }

    public class SprPrediction
    {
        public float Score;
    }
}
