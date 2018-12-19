using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static PredictionModelTrainer.ConsoleView;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;

namespace PredictionModelTrainer
{
    public class RoomRatesModelHelper
    {
        public static void CreateModelPipeline(MLContext context)
        {
            ConsoleWriteHeader("Training Predictions");
            IList<Rates> roomData = GetRoomRates();
            var trainData = context.CreateDataView(roomData);
            var trainer = context.Regression.Trainers.FastTreeTweedie("Label", "Features");
            var pipeline = context.Transforms.Categorical.OneHotEncoding("")
                .Append(trainer);
            var model = pipeline.Fit(trainData);
        }

        public static void Evaluate(MLContext context)
        {
            ConsoleWriteHeader("Evaluate Model");


        }

        private static IList<Rates> GetRoomRates()
        {
            IList<Rates> roomRateList = new List<Rates>();

            using (var context = new AppContext())
            {
                var dblist = context.RoomRates.ToList();
                foreach (var item in dblist)
                {
                    if(item.AmountTypeExclusive != 0)
                    {
                        Rates modelRate = new Rates();
                        modelRate.Day = item.Date.Day.ToString();
                        modelRate.Month = item.Date.Month.ToString();
                        modelRate.Year = item.Date.Year.ToString();
                        modelRate.RoomType = item.RoomTypeCode;
                        modelRate.RateCode = item.RateCode;
                        modelRate.Amount = item.AmountTypeExclusive;

                        roomRateList.Add(modelRate);
                    }
                }
            }

            return roomRateList;
        }
    }
}
