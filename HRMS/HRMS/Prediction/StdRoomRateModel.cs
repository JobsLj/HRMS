using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Core.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.Extensions.Configuration;

namespace HRMS.Prediction
{
    public class StdRoomRateModel
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;


        public StdRoomRateModel(MLContext mlContext, IConfiguration configuration)
        {
            _mlContext = mlContext;
            string modelFolder = configuration["ForecastModelsPath"];

            //Load the ProductSalesForecast model from the .ZIP file
            using (var fileStream = File.OpenRead($"{modelFolder}/stdRate_fastTreeTweedie.zip"))
            {
                _model = mlContext.Model.Load(fileStream);
            }
        }

        //public PredictionFunction<CountryData, CountrySalesPrediction> CreatePredictionFunction()
        //{
        //    return _model.MakePredictionFunction<CountryData, CountrySalesPrediction>(_mlContext);
        //}
    }
}
