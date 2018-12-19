using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PredictionModelTrainer
{
    public class RoomRates
    {
        [Key]
        public int DailyRoomRateId { get; set; }
        public DateTime Date { get; set; }
        public int RateCodeId { get; set; }
        public string RateCode { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeCode { get; set; }
        public int AmountTypeInclusive { get; set; }
        public int AmountTypeExclusive { get; set; }
        public int TaxAmount { get; set; }
        public bool RoomRateIsSet { get; set; }
        public bool Iscomplimentary { get; set; }
    }
}
