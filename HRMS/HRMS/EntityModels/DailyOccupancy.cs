using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HRMS.EntityModels
{
    public class DailyOccupancy
    {
        [Key]
        public int DailyOccupancyId { get; set; }
        public DateTime Date { get; set; }
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
        public int AssignedReservation { get; set; }
        public int UnassignedReservation { get; set; }
        public int AdultNo { get; set; }
        public int ChildrenNo { get; set; }
        public int TotalCancellation { get; set; }
        public int NoShow { get; set; }
        public int UserCancellation { get; set; }
        public int TentativeCancellation { get; set; }
        public int Vip { get; set; }
        public int Departing { get; set; }
        public int Inhouse { get; set; }
        public int Arriving { get; set; }
    }
}
