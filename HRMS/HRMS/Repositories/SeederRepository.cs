using HRMS.Data;
using HRMS.EntityModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Repositories
{
    public class SeederRepository : ISeederRepository
    {
        private readonly ApplicationContext context;

        public SeederRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public bool CheckifEmpty()
        {
            return !context.RoomRates.Any()? true: false;
        }

        public bool CheckPredictions()
        {
            return !context.Predictions.Any() ? true : false;
        }

        public List<DailyRoomRates> GetLatestRoomRates(int roomid, DateTime? latest = null)
        {
            if (latest == null)
                latest = context.RoomRates.Max(r => r.Date);

            var dblist = context.RoomRates.Where(item => item.RoomTypeId == roomid && item.Date == latest).ToList();
            return dblist;
        }

        public DailyOccupancyRoomType GetLatestRoomTypeOccupancy(int roomid, DateTime? latest = null)
        {
            if (latest == null)
                latest = context.RoomTypeOccupancy.Max(r => r.Date);

            return context.RoomTypeOccupancy.Where(item => item.RoomTypeId == roomid && item.Date == latest).FirstOrDefault();
        }

        public List<DailyPredictionModel> GetPredictions()
        {
            var list = context.Predictions.OrderByDescending(x => x.Date).ToList();
            return list.Take(14).ToList();
        }

        public DailyPredictionModel GetPredictionByDate(DateTime date)
        {
            return context.Predictions.Where(d => d.Date == date).FirstOrDefault();
        }

        public DailyOccupancy GetOccupancyByDate(DateTime date)
        {
            return context.Occupancy.Where(d => d.Date == date).FirstOrDefault();
        }

        public void UpdatePredictions(DailyPredictionModel item)
        {
            context.Predictions.Update(item);
            context.SaveChanges();
        }

        public void AddPredictions(List<DailyPredictionModel> list)
        {
            foreach(var item in list)
            {
                context.Predictions.Add(item);
            }
            context.SaveChanges();
        }

        public void SeedRoomRates(string json)
        {
            var result = JObject.Parse(json);
            foreach(var item in result["data"])
            {
                DailyRoomRates rates = new DailyRoomRates();
                rates.Date = DateTime.Parse(item["date"].ToString());
                rates.RateCodeId = Convert.ToInt32(item["rate_code_id"].ToString());
                rates.RateCode = item["rate_code"].ToString();
                rates.RoomTypeId = Convert.ToInt32(item["room_type_id"].ToString());
                rates.RoomTypeCode = item["room_type_code"].ToString();
                rates.AmountTypeInclusive = Convert.ToInt32(item["amount_tax_inclusive"].ToString());
                rates.AmountTypeExclusive = Convert.ToInt32(item["amount_tax_exclusive"].ToString());
                rates.TaxAmount = Convert.ToInt32(item["tax_amount"].ToString());
                rates.RoomRateIsSet = Convert.ToBoolean(item["room_rate_is_set"].ToString());
                rates.Iscomplimentary = Convert.ToBoolean(item["is_complimentary"].ToString());

                context.RoomRates.Add(rates);
            }
            context.SaveChanges();
        }

        public void SeedOccupancy(string json)
        {
            var result = JObject.Parse(json);
            foreach (var item in result["data"])
            {
                DailyOccupancy occ = new DailyOccupancy();
                occ.Date = DateTime.Parse(item["date"].ToString());
                occ.ReservationNo = Convert.ToInt32(item["no_of_reservation"].ToString());
                occ.RoomOccupied = Convert.ToInt32(item["room_occupied"].ToString());
                occ.RoomSold = Convert.ToInt32(item["room_sold"].ToString());
                occ.Complimentary = Convert.ToInt32(item["complimentary"].ToString());
                occ.DayUse = Convert.ToInt32(item["day_use"].ToString());
                occ.Tentative = Convert.ToInt32(item["tentative"].ToString());
                occ.Unavailable = Convert.ToInt32(item["unavailable"].ToString());
                occ.TotalRoom = Convert.ToInt32(item["total_room"].ToString());
                occ.RoomInventory = Convert.ToInt32(item["room_inventory"].ToString());
                occ.VacantRoom = Convert.ToInt32(item["vacant_room"].ToString());
                occ.AssignedReservation = Convert.ToInt32(item["assigned_reservation"].ToString());
                occ.UnassignedReservation = Convert.ToInt32(item["unassigned_reservation"].ToString());
                occ.AdultNo = Convert.ToInt32(item["no_of_adult"].ToString());
                occ.ChildrenNo = Convert.ToInt32(item["no_of_children"].ToString());
                occ.TotalCancellation = Convert.ToInt32(item["total_cancellation"].ToString());
                occ.NoShow = Convert.ToInt32(item["no_show"].ToString());
                occ.UserCancellation = Convert.ToInt32(item["user_cancellation"].ToString());
                occ.TentativeCancellation = Convert.ToInt32(item["tentative_cancellation"].ToString());
                occ.Vip = Convert.ToInt32(item["vip"].ToString());
                occ.Departing = Convert.ToInt32(item["departing"].ToString());
                occ.Inhouse = Convert.ToInt32(item["inhouse"].ToString());
                occ.Arriving = Convert.ToInt32(item["arriving"].ToString());

                context.Occupancy.Add(occ);
            }
            context.SaveChanges();
        }
        public void SeedRoomTypeOccupancy(string json)
        {
            var result = JObject.Parse(json);
            foreach (var item in result["data"])
            {
                DailyOccupancyRoomType occ = new DailyOccupancyRoomType();
                occ.Date = DateTime.Parse(item["date"].ToString());
                occ.RoomTypeId = Convert.ToInt32(item["room_type_id"].ToString());
                occ.RoomTypeCode = item["room_type_code"].ToString();
                occ.RoomTypeDesc = item["room_type_description"].ToString();
                occ.ReservationNo = Convert.ToInt32(item["no_of_reservation"].ToString());
                occ.RoomOccupied = Convert.ToInt32(item["room_occupied"].ToString());
                occ.RoomSold = Convert.ToInt32(item["room_sold"].ToString());
                occ.Complimentary = Convert.ToInt32(item["complimentary"].ToString());
                occ.DayUse = Convert.ToInt32(item["day_use"].ToString());
                occ.Tentative = Convert.ToInt32(item["tentative"].ToString());
                occ.Unavailable = Convert.ToInt32(item["unavailable"].ToString());
                occ.TotalRoom = Convert.ToInt32(item["total_room"].ToString());
                occ.RoomInventory = Convert.ToInt32(item["room_inventory"].ToString());
                occ.VacantRoom = Convert.ToInt32(item["vacant_room"].ToString());
                occ.UnassignedReservation = Convert.ToInt32(item["unassigned_reservation"].ToString());
                occ.AdultNo = Convert.ToInt32(item["no_of_adult"].ToString());
                occ.ChildrenNo = Convert.ToInt32(item["no_of_children"].ToString());
                occ.TotalCancellation = Convert.ToInt32(item["total_cancellation"].ToString());
                occ.NoShow = Convert.ToInt32(item["no_show"].ToString());
                occ.UserCancellation = Convert.ToInt32(item["user_cancellation"].ToString());
                occ.TentativeCancellation = Convert.ToInt32(item["tentative_cancellation"].ToString());

                context.RoomTypeOccupancy.Add(occ);
            }
            context.SaveChanges();
        }
    }
}
