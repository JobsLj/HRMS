using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Repositories
{
    public interface ISeederRepository
    {
        bool CheckifEmpty();
        void SeedRoomRates(string json);
        void SeedOccupancy(string json);
        void SeedRoomTypeOccupancy(string json);
    }
}
