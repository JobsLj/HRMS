using HRMS.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Repositories
{
    public interface ISeederRepository
    {
        bool CheckifEmpty();
        List<DailyRoomRates> GetLatestRoomRates(DateTime? latest = null);
        void SeedRoomRates(string json);
        void SeedOccupancy(string json);
        void SeedRoomTypeOccupancy(string json);
    }
}
