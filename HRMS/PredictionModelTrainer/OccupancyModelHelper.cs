using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static PredictionModelTrainer.ConsoleView;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Core.Data;
using System.IO;
using Microsoft.ML.Runtime.Data;

namespace PredictionModelTrainer
{
    public class OccupancyModelHelper
    {
        public static void CreateModelPipeline(MLContext context, string outputModelPath = "occupancy_fastTreeTweedie.zip")
        {
            ConsoleWriteHeader("Training prediction model");
            IList<OccupancyTrainer> Data = GetOccupancyData();
            // Load the data
            var trainData = context.CreateDataView(Data);

            // Choosing regression algorithm
            var trainer = context.Regression.Trainers.FastTreeTweedie("Label", "Features");

            // Transform the data
            var pipeline = context.Transforms.Categorical.OneHotEncoding("Date")
                .Append(context.Transforms.CopyColumns("Next", "Label"))
                .Append(context.Transforms.Concatenate(outputColumn: "Features", "Date", "TotalRoom", "Prev", "Occupied"))
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

        public static void Evaluate(MLContext context, string outputModelPath = "occupancy_fastTreeTweedie.zip")
        {
            ConsoleWriteHeader("Evaluate Model");

            ITransformer trainedModel;
            using (var stream = File.OpenRead(outputModelPath))
            {
                trainedModel = context.Model.Load(stream);
            }

            var predictionFunct = trainedModel.MakePredictionFunction<OccupancyTrainer, OccupancyPrediction>(context);

            Console.WriteLine("** Testing sample date **");

            // Build sample data

            var dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("25-10-2018"),
                Occupied = 40,
                TotalRoom = 71,
                Prev = 51,
            };
            // Predict sample data
            var prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 26-10-2018, - Real value: 40, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("26-10-2018"),
                Occupied = 40,
                TotalRoom = 71,
                Prev = 40,
            };
            // Predict sample data
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 27-10-2018, - Real value: 44, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("05-12-2018"),
                Occupied = 25,
                TotalRoom = 71,
                Prev = 28,
            };
            // Predict sample data
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 06-12-2018, - Real value: 14, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("06-12-2018"),
                Occupied = 14,
                TotalRoom = 71,
                Prev = 25,
            };
            // Predict sample data
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 07-12-2018, - Real value: 16, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("07-12-2018"),
                Occupied = 16,
                TotalRoom = 71,
                Prev = 14,
            };
            // Predict sample data
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 08-12-2018, - Real value: 13, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("08-12-2018"),
                Occupied = 13,
                TotalRoom = 71,
                Prev = 16,
            };
            // Predict sample data
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 09-12-2018, - Real value: 27, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = DateTime.Parse("09-12-2018"),
                Occupied = 27,
                TotalRoom = 71,
                Prev = 13,
            };
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 10-12-2018, - Predicted Forecast: {prediction.Score}");



            Console.WriteLine("=============== Attempt to predict the next 30 days occupancy ===============");
            float ps = 38;
            float oed = 47;
            var datey = DateTime.Parse("09-12-2018");
            for (int i = 0; i < 30; i++)
            {
                dataSample = new OccupancyTrainer()
                {
                    Date = datey,
                    Occupied = oed,
                    TotalRoom = 71,
                    Prev = ps,
                };
                prediction = predictionFunct.Predict(dataSample);
                Console.WriteLine($"Date to predict: {dataSample.Date.AddDays(1)}, - Predicted Forecast: {prediction.Score}");

                datey = datey.AddDays(1);
                ps = oed;
                oed = prediction.Score;
            }
        }

        private static IList<OccupancyTrainer> GetOccupancyData()
        {
            IList<OccupancyTrainer> occupancyList = new List<OccupancyTrainer>();

            using (var context = new AppContext())
            {
                var dblist = context.Occupancy.ToList();
                foreach (var item in dblist)
                {
                    OccupancyTrainer modelOcc = new OccupancyTrainer();
                    modelOcc.Date = item.Date;
                    modelOcc.Occupied = item.RoomOccupied;
                    modelOcc.TotalRoom = item.TotalRoom;
                    modelOcc.Next = GetNextDayValue(item.Date);
                    modelOcc.Prev = GetPrevDayValue(item.Date);

                    occupancyList.Add(modelOcc);
                }
            }
            return occupancyList;
        }

        private static float GetNextDayValue(DateTime date)
        {
            using (var context = new AppContext())
            {
                date = date.AddDays(1);
                var item = context.Occupancy.Where(i => i.Date == date).FirstOrDefault();

                if (item != null)
                    return item.RoomOccupied;
                else
                    return 0;
            }
        }

        private static float GetPrevDayValue(DateTime date)
        {
            using (var context = new AppContext())
            {
                date = date.AddDays(-1);
                var item = context.Occupancy.Where(i => i.Date == date).FirstOrDefault();

                if (item != null)
                    return item.RoomOccupied;
                else
                    return 0;
            }
        }
    }
}
