using HRMS.Data;
using HRMS.Models;
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
    }
}
