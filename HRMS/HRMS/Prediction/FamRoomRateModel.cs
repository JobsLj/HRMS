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
    public class FamRoomRateModel
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;


        public FamRoomRateModel(MLContext mlContext, IConfiguration configuration)
        {
            _mlContext = mlContext;

            //Load the Standard rooms rate model from the .ZIP file
            using (var fileStream = File.OpenRead($"Prediction/ModelFiles/famRate_fastTreeTweedie.zip"))
            {
                _model = mlContext.Model.Load(fileStream);
            }
        }

        public PredictionFunction<FamRoomRateData, RoomRatePrediction> CreatePredictionFunction()
        {
            return _model.MakePredictionFunction<FamRoomRateData, RoomRatePrediction>(_mlContext);
        }

    }
}
