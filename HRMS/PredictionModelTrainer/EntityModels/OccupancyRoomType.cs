using System;
using System.ComponentModel.DataAnnotations;

namespace PredictionModelTrainer.EntityModels
{
    public class OccupancyRoomType
    {
        [Key]
        public int DailyOccupancyId { get; set; }
        public DateTime Date { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeCode { get; set; }
        public string RoomTypeDesc { get; set; }
        public int ReservationNo { get; set; }
        public int RoomOccupied { get; set; }
        public int RoomSold { get; set; }
        public int Complimentary { get; set; }
        public int DayUse { get; set; }
        public int Tentative { get; set; }
        public int Unavailable { get; set; }
        public int TotalRoom { get; set; }
        public int RoomInventory { get; set; }
        public int VacantRoom { get; set; }
        public int UnassignedReservation { get; set; }
        public int AdultNo { get; set; }
        public int ChildrenNo { get; set; }
        public int TotalCancellation { get; set; }
        public int NoShow { get; set; }
        public int UserCancellation { get; set; }
        public int TentativeCancellation { get; set; }
    }
}
