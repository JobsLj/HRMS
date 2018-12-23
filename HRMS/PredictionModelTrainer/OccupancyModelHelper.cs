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
                .Append(context.Transforms.Concatenate(outputColumn: "Features", "Date", "TotalRoom", "RoomInventory", "AssignedReservation"
                , "UnassignedReservation", "AdultNo", "ChildrenNo", "Arriving", "Occupied"))
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
                Date = "07-12-2018",
                Occupied = 16,
                RoomInventory = 70,
                TotalRoom = 71,
                AssignedReservation = 16,
                UnassignedReservation = 0,
                AdultNo = 33,
                ChildrenNo = 1,
                Arriving = 4,
            };
            // Predict sample data
            var prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 08-12-2018, - Real value: 13, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = "08-12-2018",
                Occupied = 13,
                RoomInventory = 70,
                TotalRoom = 71,
                AssignedReservation = 13,
                UnassignedReservation = 0,
                AdultNo = 27,
                ChildrenNo = 1,
                Arriving = 0,
            };
            // Predict sample data
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 09-12-2018, - Real value: 27, Predicted Forecast: {prediction.Score}");

            dataSample = new OccupancyTrainer()
            {
                Date = "09-12-2018",
                Occupied = 27,
                RoomInventory = 70,
                TotalRoom = 71,
                AssignedReservation = 27,
                UnassignedReservation = 0,
                AdultNo = 54,
                ChildrenNo = 1,
                Arriving = 15,
            };
            prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 10-12-2018, - Predicted Forecast: {prediction.Score}");
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
                    modelOcc.Date = item.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    modelOcc.Occupied = item.RoomOccupied;
                    modelOcc.TotalRoom = item.TotalRoom;
                    modelOcc.RoomInventory = item.RoomInventory;
                    modelOcc.AssignedReservation = item.AssignedReservation;
                    modelOcc.UnassignedReservation = item.UnassignedReservation;
                    modelOcc.AdultNo = item.AdultNo;
                    modelOcc.ChildrenNo = item.ChildrenNo;
                    modelOcc.Arriving = item.Arriving;
                    modelOcc.Next = GetNextDayValue(item.Date);

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
    }
}
