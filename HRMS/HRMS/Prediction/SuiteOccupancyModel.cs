using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.Extensions.Configuration;
using HRMS.Prediction.DataStructures.RoomOccupancy;


namespace HRMS.Prediction
{
    public class SuiteOccupancyModel
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;


        public SuiteOccupancyModel(MLContext mlContext, IConfiguration configuration)
        {
            _mlContext = mlContext;

            //Load the Standard rooms rate model from the .ZIP file
            using (var fileStream = File.OpenRead($"Prediction/ModelFiles/suiteoccupancy_fastTreeTweedie.zip"))
            {
                _model = mlContext.Model.Load(fileStream);
            }
        }

        public PredictionFunction<SuiteOccupancyData, OccupancyPrediction> CreatePredictionFunction()
        {
            return _model.MakePredictionFunction<SuiteOccupancyData, OccupancyPrediction>(_mlContext);
        }
    }
}
