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
using HRMS.Prediction.DataStructures.RoomOccupancy;

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

        private readonly PredictionFunction<StdOccupancyData, OccupancyPrediction> StdOccupancyPredFunction;
        private readonly PredictionFunction<SprOccupancyData, OccupancyPrediction> SprOccupancyPredFunction;
        private readonly PredictionFunction<FamOccupancyData, OccupancyPrediction> FamOccupancyPredFunction;
        private readonly PredictionFunction<SuiteOccupancyData, OccupancyPrediction> SuiteOccupancyPredFunction;
        private readonly PredictionFunction<DlxOccupancyData, OccupancyPrediction> DlxOccupancyPredFunction;

        // Controller
        public HomeController(ISeederRepository repository, 
            PredictionFunction<StdRoomRateData, RoomRatePrediction> StdRoomRatePredFunction, 
            PredictionFunction<SprRoomRateData, RoomRatePrediction> SprRoomRatePredFunction,
            PredictionFunction<FamRoomRateData, RoomRatePrediction> FamRoomRatePredFunction,
            PredictionFunction<SuiteRoomRateData, RoomRatePrediction> SuiteRoomRatePredFunction,
            PredictionFunction<DlxRoomRateData, RoomRatePrediction> DlxRoomRatePredFunction,
            PredictionFunction<StdOccupancyData, OccupancyPrediction> StdOccupancyPredFunction,
            PredictionFunction<SprOccupancyData, OccupancyPrediction> SprOccupancyPredFunction,
            PredictionFunction<FamOccupancyData, OccupancyPrediction> FamOccupancyPredFunction,
            PredictionFunction<SuiteOccupancyData, OccupancyPrediction> SuiteOccupancyPredFunction,
            PredictionFunction<DlxOccupancyData, OccupancyPrediction> DlxOccupancyPredFunction)
        {
            this.repository = repository;

            this.StdRoomRatePredFunction = StdRoomRatePredFunction;
            this.SprRoomRatePredFunction = SprRoomRatePredFunction;
            this.FamRoomRatePredFunction = FamRoomRatePredFunction;
            this.SuiteRoomRatePredFunction = SuiteRoomRatePredFunction;
            this.DlxRoomRatePredFunction = DlxRoomRatePredFunction;

            this.StdOccupancyPredFunction = StdOccupancyPredFunction;
            this.SprOccupancyPredFunction = SprOccupancyPredFunction;
            this.FamOccupancyPredFunction = FamOccupancyPredFunction;
            this.SuiteOccupancyPredFunction = SuiteOccupancyPredFunction;
            this.DlxOccupancyPredFunction = DlxOccupancyPredFunction;
        }

        public List<Tuple<DateTime, float>> OccupancyPredictions(int roomid)
        {
            var RoomList = repository.GetLatestRoomTypeOccupancy(roomid);
            var prevList = repository.GetLatestRoomTypeOccupancy(roomid, RoomList.FirstOrDefault().Date.AddDays(-1));

            var date = RoomList.FirstOrDefault().Date;
            float occupied = RoomList.FirstOrDefault().RoomOccupied;
            float totalRoom = RoomList.FirstOrDefault().TotalRoom;
            float prevOccupied = prevList.FirstOrDefault().RoomOccupied;

            var predictions = new List<Tuple<DateTime, float>>();

            // Predictions for the next 14 days
            for (int i = 0; i < 14; i++)
            {
                if (roomid == 8)
                {
                    var sample = new StdOccupancyData(date, occupied, totalRoom, prevOccupied);
                    OccupancyPrediction pred = null;
                    pred = this.StdOccupancyPredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevOccupied = occupied;
                    occupied = pred.Score;
                }
                else if (roomid == 7)
                {
                    var sample = new SprOccupancyData(date, occupied, totalRoom, prevOccupied);
                    OccupancyPrediction pred = null;
                    pred = this.SprOccupancyPredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevOccupied = occupied;
                    occupied = pred.Score;
                }
                else if (roomid == 9)
                {
                    var sample = new FamOccupancyData(date, occupied, totalRoom, prevOccupied);
                    OccupancyPrediction pred = null;
                    pred = this.FamOccupancyPredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevOccupied = occupied;
                    occupied = pred.Score;
                }
                else if (roomid == 10)
                {
                    var sample = new SuiteOccupancyData(date, occupied, totalRoom, prevOccupied);
                    OccupancyPrediction pred = null;
                    pred = this.SuiteOccupancyPredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevOccupied = occupied;
                    occupied = pred.Score;
                }
                else
                {
                    var sample = new DlxOccupancyData(date, occupied, totalRoom, prevOccupied);
                    OccupancyPrediction pred = null;
                    pred = this.DlxOccupancyPredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevOccupied = occupied;
                    occupied = pred.Score;
                }
            }

            return predictions;
        }

        public List<Tuple<DateTime, float>> RoomRatePredictions(int roomid)
        {
            var RoomList = repository.GetLatestRoomRates(roomid);
            var prevList = repository.GetLatestRoomRates(roomid, RoomList.FirstOrDefault().Date.AddDays(-1));

            var date = RoomList.FirstOrDefault().Date;
            var prevAmount = GetAvgAmount(prevList);
            var Amount = GetAvgAmount(RoomList);

            var predictions = new List<Tuple<DateTime, float>>();

            // Predictions for the next 14 days
            for (int i = 0; i < 14; i++)
            {
                if (roomid == 8)
                {
                    var sample = new StdRoomRateData(date, prevAmount, Amount);
                    RoomRatePrediction pred = null;
                    pred = this.StdRoomRatePredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevAmount = Amount;
                    Amount = pred.Score;
                }
                else if (roomid == 7)
                {
                    var sample = new SprRoomRateData(date, prevAmount, Amount);
                    RoomRatePrediction pred = null;
                    pred = this.SprRoomRatePredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevAmount = Amount;
                    Amount = pred.Score;
                }
                else if (roomid == 9)
                {
                    var sample = new FamRoomRateData(date, prevAmount, Amount);
                    RoomRatePrediction pred = null;
                    pred = this.FamRoomRatePredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevAmount = Amount;
                    Amount = pred.Score;
                }
                else if (roomid == 10)
                {
                    var sample = new SuiteRoomRateData(date, prevAmount, Amount);
                    RoomRatePrediction pred = null;
                    pred = this.SuiteRoomRatePredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevAmount = Amount;
                    Amount = pred.Score;
                }
                else
                {
                    var sample = new DlxRoomRateData(date, prevAmount, Amount);
                    RoomRatePrediction pred = null;
                    pred = this.DlxRoomRatePredFunction.Predict(sample);
                    predictions.Add(new Tuple<DateTime, float>(date.AddDays(1), pred.Score));

                    date = date.AddDays(1);
                    prevAmount = Amount;
                    Amount = pred.Score;
                }
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
            var CalendarModel = new List<CalendarEvent>();
            // Check if database has existing prediction values
            if (repository.CheckPredictions())
            {
                // Get predicted rates for the different room type
                var SprRoomPred = RoomRatePredictions(7);
                var StdRoomPred = RoomRatePredictions(8);
                var FamRoomPred = RoomRatePredictions(9);
                var SuiteRoomPred = RoomRatePredictions(10);
                var DlxRoomPred = RoomRatePredictions(11);

                // Get the predicted occupancy for the different room type
                var SprOccPred = OccupancyPredictions(7);
                var StdOccPred = OccupancyPredictions(8);
                var FamOccPred = OccupancyPredictions(9);
                var SuiteOccPred = OccupancyPredictions(10);
                var DlxOccPred = OccupancyPredictions(11);

                var PredictionList = new List<DailyPredictionModel>();                

                foreach (var item in SprRoomPred)
                {
                    // Store into database
                    var pred = new DailyPredictionModel();
                    var modal = new CalendarEvent();

                    pred.Date = item.Item1;
                    pred.SprRoomRate = item.Item2;
                    pred.SprOccupancy = SprOccPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.StdRoomRate = StdRoomPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.StdOccupancy = StdOccPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.FamRoomRate = FamRoomPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.FamOccupancy = FamOccPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.SuiteRoomRate = SuiteRoomPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.SuiteOccupancy = SuiteOccPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.DlxRoomRate = SuiteRoomPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;
                    pred.DlxOccupancy = DlxOccPred.Where(i => i.Item1 == item.Item1).FirstOrDefault().Item2;

                    PredictionList.Add(pred);

                    // Calculate the ADR
                    var totalOccupancy = pred.SprOccupancy + pred.StdOccupancy + pred.FamOccupancy + pred.SuiteOccupancy + pred.DlxOccupancy;
                    var totalRevenue = (pred.SprRoomRate * pred.SprOccupancy) + (pred.StdRoomRate * pred.StdOccupancy) +
                        (pred.FamRoomRate * pred.FamOccupancy) + (pred.SuiteRoomRate * pred.SuiteOccupancy) +
                        (pred.DlxRoomRate * pred.DlxOccupancy);
                    var adr = totalRevenue / totalOccupancy;
                    var revpar = totalRevenue / 71;
                    var occupancyRate = totalOccupancy / 71;

                    modal.Date = item.Item1;
                    modal.Adr = adr.ToString();
                    modal.RevPar = revpar.ToString();
                    modal.Occupancy = occupancyRate.ToString("#0.##%");
                    modal.Type = "success";

                    CalendarModel.Add(modal);
                }

                repository.AddPredictions(PredictionList);
            }
            else
            {
                // If database already has existing predicted data
                var list = repository.GetPredictions();
                
                foreach(var pred in list)
                {
                    var modal = new CalendarEvent();

                    // Calculate the ADR
                    var totalOccupancy = pred.SprOccupancy + pred.StdOccupancy + pred.FamOccupancy + pred.SuiteOccupancy + pred.DlxOccupancy;
                    var totalRevenue = (pred.SprRoomRate * pred.SprOccupancy) + (pred.StdRoomRate * pred.StdOccupancy) +
                        (pred.FamRoomRate * pred.FamOccupancy) + (pred.SuiteRoomRate * pred.SuiteOccupancy) +
                        (pred.DlxRoomRate * pred.DlxOccupancy);
                    var adr = totalRevenue / totalOccupancy;
                    var revpar = totalRevenue / 71;
                    var occupancyRate = totalOccupancy / 71;

                    modal.Date = pred.Date;
                    modal.Adr = adr.ToString();
                    modal.RevPar = revpar.ToString();
                    modal.Occupancy = occupancyRate.ToString("#0.##%");
                    modal.Type = "success";

                    CalendarModel.Add(modal);
                }
            }

            return View(CalendarModel);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet("Home/Details/{selected}")]
        public IActionResult Details(string selected)
        {
            ViewData["id"] = selected;
            var model = new DetailsViewModel();

            var selectedModel = repository.GetPredictionByDate(DateTime.Parse(selected));
            var prevMonthOcc = repository.GetOccupancyByDate(DateTime.Parse(selected).AddMonths(-1)).RoomOccupied;
            var prevYearOcc = repository.GetOccupancyByDate(DateTime.Parse(selected).AddYears(-1)).RoomOccupied;

            var prevMonthSprRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddMonths(-1), 7));
            var prevMonthStdRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddMonths(-1), 8));
            var prevMonthFamRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddMonths(-1), 9));
            var prevMonthSuiteRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddMonths(-1), 10));
            var prevMonthDlxRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddMonths(-1), 11));

            var prevYearSprRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddYears(-1), 7));
            var prevYearStdRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddYears(-1), 8));
            var prevYearFamRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddYears(-1), 9));
            var prevYearSuiteRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddYears(-1), 10));
            var prevYearDlxRates = GetAvgAmount(repository.GetRoomRatesByDate(DateTime.Parse(selected).AddYears(-1), 11));

            // Calculate ADR/RevPar/Occupancy rate
            var totalOccupancy = selectedModel.SprOccupancy + selectedModel.StdOccupancy + selectedModel.FamOccupancy +
                selectedModel.SuiteOccupancy + selectedModel.DlxOccupancy;
            var totalRevenue = (selectedModel.SprRoomRate * selectedModel.SprOccupancy) + 
                (selectedModel.StdRoomRate * selectedModel.StdOccupancy) +
                (selectedModel.FamRoomRate * selectedModel.FamOccupancy) + 
                (selectedModel.SuiteRoomRate * selectedModel.SuiteOccupancy) +
                (selectedModel.DlxRoomRate * selectedModel.DlxOccupancy);

            var adr = totalRevenue / totalOccupancy;
            var revpar = totalRevenue / 71;
            var occupancyRate = totalOccupancy / 71;



            return View("Details");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
