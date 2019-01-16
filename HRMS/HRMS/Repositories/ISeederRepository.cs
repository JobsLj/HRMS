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
        bool CheckPredictions();
        bool CheckIfNeedsPredictions(DateTime date);
        List<DailyRoomRates> GetLatestRoomRates(int roomid, DateTime? latest = null);
        DailyOccupancyRoomType GetLatestRoomTypeOccupancy(int roomid, DateTime? latest = null);
        List<DailyPredictionModel> GetPredictions();
        DailyPredictionModel GetPredictionByDate(DateTime date);
        DailyOccupancy GetOccupancyByDate(DateTime date);
        void UpdatePredictions(DailyPredictionModel item);
        void AddPredictions(List<DailyPredictionModel> list);
        void SeedRoomRates(string json);
        void SeedOccupancy(string json);
        void SeedRoomTypeOccupancy(string json);
    }
}
