using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using HRMS.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using HRMS.Repositories;
using HRMS.Prediction.DataStructures.RoomRates;
using HRMS.Prediction.DataStructures;
using Microsoft.ML.Runtime.Data;
using HRMS.EntityModels;

namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeederRepository repository;
        private readonly PredictionFunction<StdRoomRateData, RoomRatePrediction> StdRoomRatePredFunction;
        private readonly PredictionFunction<SprRoomRateData, RoomRatePrediction> SprRoomRatePredFunction;
        private readonly PredictionFunction<FamRoomRateData, RoomRatePrediction> FamRoomRatePredFunction;
        private readonly PredictionFunction<SuiteRoomRateData, RoomRatePrediction> SuiteRoomRatePredFunction;
        private readonly PredictionFunction<DlxRoomRateData, RoomRatePrediction> DlxRoomRatePredFunction;

        public HomeController(ISeederRepository repository, 
            PredictionFunction<StdRoomRateData, RoomRatePrediction> StdRoomRatePredFunction, 
            PredictionFunction<SprRoomRateData, RoomRatePrediction> SprRoomRatePredFunction,
            PredictionFunction<FamRoomRateData, RoomRatePrediction> FamRoomRatePredFunction,
            PredictionFunction<SuiteRoomRateData, RoomRatePrediction> SuiteRoomRatePredFunction,
            PredictionFunction<DlxRoomRateData, RoomRatePrediction> DlxRoomRatePredFunction)
        {
            this.repository = repository;
            this.StdRoomRatePredFunction = StdRoomRatePredFunction;
            this.SprRoomRatePredFunction = SprRoomRatePredFunction;
            this.FamRoomRatePredFunction = FamRoomRatePredFunction;
            this.SuiteRoomRatePredFunction = SuiteRoomRatePredFunction;
            this.DlxRoomRatePredFunction = DlxRoomRatePredFunction;
        }

        public List<Tuple<DateTime, float>> StdRoomRatePredictions()
        {
            var stdRoomList = repository.GetLatestRoomRates();
            var prevList = repository.GetLatestRoomRates(stdRoomList.FirstOrDefault().Date.AddDays(-1));

            var date = stdRoomList.FirstOrDefault().Date;
            var prevAmount = GetAvgAmount(prevList);
            var Amount = GetAvgAmount(stdRoomList);

            var predictions = new List<Tuple<DateTime, float>>();

            for (int i = 0; i < 14; i++)
            {
                var sample = new StdRoomRateData(date, prevAmount, Amount);

                RoomRatePrediction pred = null;
                pred = this.StdRoomRatePredFunction.Predict(sample);
                predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                date = date.AddDays(1);
                prevAmount = Amount;
                Amount = pred.Score;
            }

            return predictions;
        }

        private float GetAvgAmount(List<DailyRoomRates> list)
        {
            var total = 0;
            foreach (var item in list)
            {
                total += item.AmountTypeExclusive;
            }
            var average = total / list.Count();
            return average;
        }

        public async Task<IActionResult> Index()
        {         
            var StdRoomPred = StdRoomRatePredictions();

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet("Home/Details/{selected}")]
        public IActionResult Details(string selected)
        {
            ViewData["id"] = selected;

            return View("Details");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
