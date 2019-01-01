using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.Extensions.Configuration;
using HRMS.Prediction.DataStructures;
using HRMS.Prediction.DataStructures.RoomRates;

namespace HRMS.Prediction
{
    public class StdRoomRateModel
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;


        public StdRoomRateModel(MLContext mlContext, IConfiguration configuration)
        {
            _mlContext = mlContext;

            //Load the Standard rooms rate model from the .ZIP file
            using (var fileStream = File.OpenRead($"Prediction/ModelFiles/stdRate_fastTreeTweedie.zip"))
            {
                _model = mlContext.Model.Load(fileStream);
            }
        }

        public PredictionFunction<StdRoomRateData, RoomRatePrediction> CreatePredictionFunction()
        {
            return _model.MakePredictionFunction<StdRoomRateData, RoomRatePrediction>(_mlContext);
        }
    }
}
