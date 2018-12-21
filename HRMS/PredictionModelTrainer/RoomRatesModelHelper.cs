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
            IList<Rates> roomData = GetRoomRates();
            // Load the data
            var trainData = context.CreateDataView(roomData);

            // Choosing regression algorithm
            var trainer = context.Regression.Trainers.FastTreeTweedie("Label", "Features");

            // Transform the data
            var pipeline = context.Transforms.Categorical.OneHotEncoding("Date")
                .Append(context.Transforms.Categorical.OneHotEncoding("RateCode"))
                .Append(context.Transforms.CopyColumns("Amount", "Label"))
                .Append(context.Transforms.Concatenate(outputColumn: "Features", "Date", "Month", "Date", "Year"))
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

        private static IList<Rates> GetRoomRates()
        {
            IList<Rates> roomRateList = new List<Rates>();

            using (var context = new AppContext())
            {
                var dblist = context.RoomRates.Where(item => item.RoomTypeId == 7).ToList();
                foreach (var item in dblist)
                {
                    if(item.AmountTypeExclusive != 0)
                    {
                        Rates modelRate = new Rates();
                        modelRate.Date = item.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                        modelRate.Day = item.Date.Day;
                        modelRate.Month = item.Date.Month;
                        modelRate.Year = item.Date.Year;
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
