using System;
using System.Collections.Generic;
using System.Linq;
using static PredictionModelTrainer.ConsoleView;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using System.IO;

namespace PredictionModelTrainer
{
    public class FamRoomRatesModelHelper
    {
        public static void CreateModelPipeline(MLContext context, string outputModelPath = "famRate_fastTreeTweedie.zip")
        {
            ConsoleWriteHeader("Training FAM prediction model");
            IList<Rates> roomData = GetRoomRates();
            // Load the data
            var trainData = context.CreateDataView(roomData);

            // Choosing regression algorithm
            var trainer = context.Regression.Trainers.FastTreeTweedie("Label", "Features");

            // Transform the data
            var pipeline = context.Transforms.Categorical.OneHotEncoding("Date")
                .Append(context.Transforms.CopyColumns("Next", "Label"))
                .Append(context.Transforms.Concatenate(outputColumn: "Features", "Date", "Amount", "Prev"))
                .Append(trainer);

            // Cross-Validate with single dataset
            Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
            var crossValidateResults = context.Regression.CrossValidate(trainData, pipeline, numFolds: 5, labelColumn: "Label");
            PrintRegressionFoldsAverageMetrics(trainer.ToString(), crossValidateResults);

            // Create and train the model
            var model = pipeline.Fit(trainData);

            using (var file = File.OpenWrite(outputModelPath))
                model.SaveTo(context, file);
        }

        public static void Evaluate(MLContext context, string outputModelPath = "famRate_fastTreeTweedie.zip")
        {
            ConsoleWriteHeader("Evaluate FAM Model");

            ITransformer trainedModel;
            using (var stream = File.OpenRead(outputModelPath))
            {
                trainedModel = context.Model.Load(stream);
            }

            var predictionFunct = trainedModel.MakePredictionFunction<Rates, Prediction>(context);

            Console.WriteLine("** Testing sample date **");

            // Build sample data

            var dataSample = new Rates()
            {
                Date = DateTime.Parse("08-12-2018"),
                Amount = 830886,
                Prev = 830886,
            };
            // Predict sample data
            var prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 09-12-2018, - Real value: 830886, Predicted Forecast: {prediction.Score}");

            Console.WriteLine("\n\n=============== Attempt to predict the next 14 days Room rates ===============\n");
            float ps = 830886;
            float amt = 830886;
            var datey = DateTime.Parse("09-12-2018");
            for (int i = 0; i < 14; i++)
            {
                dataSample = new Rates()
                {
                    Date = datey,
                    Amount = amt,
                    Prev = ps,
                };

                prediction = predictionFunct.Predict(dataSample);
                Console.WriteLine($"Date to predict: {dataSample.Date.AddDays(1)}, - Predicted Forecast: {prediction.Score}");

                datey = datey.AddDays(1);
                ps = amt;
                amt = prediction.Score;
            }
        }

        private static IList<Rates> GetRoomRates()
        {
            IList<Rates> roomRateList = new List<Rates>();

            using (var context = new AppContext())
            {
                var dblist = context.RoomRates.Where(item => item.RoomTypeId == 9).GroupBy(i => i.Date).ToList();
                foreach (var item in dblist)
                {
                    if (item.Key != DateTime.Parse("08-1-2019"))
                    {
                        Rates modelRate = new Rates();
                        modelRate.Date = item.Key;
                        modelRate.Amount = GetAvgAmount(item);

                        var prevday = item.Key.AddDays(-1);
                        var previtem = dblist.Where(i => i.Key == prevday).FirstOrDefault();

                        var nextday = item.Key.AddDays(1);
                        var nextitem = dblist.Where(i => i.Key == nextday).FirstOrDefault();

                        if (previtem != null)
                            modelRate.Prev = GetAvgAmount(previtem);
                        else
                            modelRate.Prev = 0;

                        if (nextitem != null)
                            modelRate.Next = GetAvgAmount(nextitem);
                        else
                            modelRate.Next = 0;

                        roomRateList.Add(modelRate);
                    }
                }
            }
            return roomRateList;
        }

        private static float GetAvgAmount(IGrouping<DateTime, RoomRates> list)
        {
            var total = 0;
            foreach (var item in list)
            {
                total += item.AmountTypeExclusive;
            }
            var average = total / list.Count();
            return average;
        }
    }
}
