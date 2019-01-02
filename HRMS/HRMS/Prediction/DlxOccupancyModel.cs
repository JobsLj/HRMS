using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.Extensions.Configuration;
using HRMS.Prediction.DataStructures.RoomOccupancy;


namespace HRMS.Prediction
{
    public class DlxOccupancyModel
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;


        public DlxOccupancyModel(MLContext mlContext, IConfiguration configuration)
        {
            _mlContext = mlContext;

            //Load the Standard rooms rate model from the .ZIP file
            using (var fileStream = File.OpenRead($"Prediction/ModelFiles/dlxoccupancy_fastTreeTweedie.zip"))
            {
                _model = mlContext.Model.Load(fileStream);
            }
        }

        public PredictionFunction<DlxOccupancyData, OccupancyPrediction> CreatePredictionFunction()
        {
            return _model.MakePredictionFunction<DlxOccupancyData, OccupancyPrediction>(_mlContext);
        }
    }
}
