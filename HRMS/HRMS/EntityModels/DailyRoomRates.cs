using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.EntityModels
{
    public class DailyRoomRates
    {
        [Key]
        public int DailyRoomRateId { get; set; }
        public DateTime Date { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeCode { get; set; }
        public int AmountTypeInclusive { get; set; }
        public int AmountTypeExclusive { get; set; }
        public int TaxAmount { get; set; }
    }
}
