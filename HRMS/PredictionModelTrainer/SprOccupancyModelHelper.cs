using System;
using System.Collections.Generic;
using System.Linq;
using static PredictionModelTrainer.ConsoleView;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Core.Data;
using System.IO;
using Microsoft.ML.Runtime.Data;


namespace PredictionModelTrainer
{
    public class SprOccupancyModelHelper
    {
        public static void CreateModelPipeline(MLContext context, string outputModelPath = "sproccupancy_fastTreeTweedie.zip")
        {
            ConsoleWriteHeader("Training superior room occupancy prediction model");
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
            var crossValidateResults = context.Regression.CrossValidate(trainData, pipeline, numFolds: 10, labelColumn: "Label");
            PrintRegressionFoldsAverageMetrics(trainer.ToString(), crossValidateResults);

            // Create and train the model
            var model = pipeline.Fit(trainData);

            using (var file = File.OpenWrite(outputModelPath))
                model.SaveTo(context, file);
        }

        public static void Evaluate(MLContext context, string outputModelPath = "sproccupancy_fastTreeTweedie.zip")
        {
            ConsoleWriteHeader("Evaluate superior room occupancy Model");

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
                Date = DateTime.Parse("08-12-2018"),
                Occupied = 2,
                TotalRoom = 16,
                Prev = 2,
            };
            // Predict sample data
            var prediction = predictionFunct.Predict(dataSample);
            Console.WriteLine($"Date: {dataSample.Date}, date to predict: 09-12-2018, - Real value: 4, Predicted Forecast: {prediction.Score}");


            Console.WriteLine("\n\n=============== Attempt to predict the next 14 days occupancy ===============");
            float ps = 2;
            float oed = 4;
            var datey = DateTime.Parse("09-12-2018");
            for (int i = 0; i < 14; i++)
            {
                dataSample = new OccupancyTrainer()
                {
                    Date = datey,
                    Occupied = oed,
                    TotalRoom = 16,
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
                var dblist = context.RoomTypeOccupancy.Where(i => i.RoomTypeId == 7).ToList();
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
                var item = context.RoomTypeOccupancy.Where(i => i.Date == date && i.RoomTypeId == 7).FirstOrDefault();

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
                var item = context.RoomTypeOccupancy.Where(i => i.Date == date && i.RoomTypeId == 7).FirstOrDefault();

                if (item != null)
                    return item.RoomOccupied;
                else
                    return 0;
            }
        }
    }
}
