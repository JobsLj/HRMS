using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class Rates
    {
        public string Date { get; set; }
        public float Day { get; set; }
        public float Month { get; set; }
        public float Year { get; set; }
        public string RoomType { get; set; }
        public string RateCode { get; set; }
        public float Amount { get; set; }
    }
}
