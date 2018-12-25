using System;
using System.Collections.Generic;
using System.Globalization;
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
            ConsoleWriteHeader("Training prediction model");
            IList<SprRates> roomData = GetRoomRates();
            // Load the data
            var trainData = context.CreateDataView(roomData);

            // Choosing regression algorithm
            var trainer = context.Regression.Trainers.FastTreeTweedie("Label", "Features");

            // Transform the data
            var pipeline = context.Transforms.Categorical.OneHotEncoding("Date")
                .Append(context.Transforms.CopyColumns("Amount", "Label"))
                .Append(context.Transforms.Concatenate(outputColumn: "Features", "Date", "Next", "Prev", "Avg"))
                .Append(trainer);

            // Cross-Validate with single dataset
            Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
            var crossValidateResults = context.Regression.CrossValidate(trainData, pipeline, numFolds: 5, labelColumn: "Label");
            PrintRegressionFoldsAverageMetrics(trainer.ToString(), crossValidateResults);

            // Create and train the model
            var model = pipeline.Fit(trainData);
        }

        public static void Evaluate(MLContext context)
        {
            ConsoleWriteHeader("Evaluate Model");


        }

        private static IList<SprRates> GetRoomRates()
        {
            IList<SprRates> roomRateList = new List<SprRates>();

            using (var context = new AppContext())
            {
                var dblist = context.RoomRates.Where(item => item.RoomTypeId == 7).ToList();
                foreach (var item in dblist)
                {
                    if(item.AmountTypeExclusive != 0)
                    {
                        SprRates modelRate = new SprRates();
                        modelRate.Date = item.Date;
                        modelRate.RateCode = item.RateCode;
                        modelRate.Amount = item.AmountTypeExclusive;
                        modelRate.Avg = GetAvgAmount(item.Date);
                        modelRate.Next = GetNextAmount(item.Date);
                        modelRate.Prev = GetPrevAmount(item.Date);

                        roomRateList.Add(modelRate);
                    }
                }
            }
            return roomRateList;
        }

        private static float GetAvgAmount(DateTime date)
        {
            using (var context = new AppContext())
            {
                var items = context.RoomRates.Where(i => i.Date == date).ToList();

                var total = 0;
                foreach(var item in items)
                {
                    total += item.AmountTypeExclusive;
                }

                var average = total / items.Count;
                return average;
            }
        }
        private static float GetNextAmount(DateTime date)
        {
            using (var context = new AppContext())
            {
                date = date.AddDays(1);
                var items = context.RoomRates.Where(i => i.Date == date && i.RoomTypeId == 7).ToList();

                if (items.Count != 0)
                {
                    var total = 0;
                    foreach (var item in items)
                    {
                        total += item.AmountTypeExclusive;
                    }

                    var average = total / items.Count;
                    return average;
                }
                else
                    return 0;
            }
        }
        private static float GetPrevAmount(DateTime date)
        {
            using (var context = new AppContext())
            {
                date = date.AddDays(-1);
                var items = context.RoomRates.Where(i => i.Date == date && i.RoomTypeId == 7).ToList();

                if (items.Count != 0)
                {
                    var total = 0;
                    foreach (var item in items)
                    {
                        total += item.AmountTypeExclusive;
                    }
                    var average = total / items.Count;
                    return average;
                }
                else
                    return 0;
            }
        }
    }
}
