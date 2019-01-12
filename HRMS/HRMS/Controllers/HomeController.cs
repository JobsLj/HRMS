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

        private static readonly int DEFAULT_SPR = 743802;
        private static readonly int DEFAULT_STD = 589659;
        private static readonly int DEFAULT_FAM = 818182;
        private static readonly int DEFAULT_SUITE = 1368595;
        private static readonly int DEFAULT_DLX = 920000;
        private static readonly int DEFAULT_ADR = 900000;

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
            var prevList = repository.GetLatestRoomTypeOccupancy(roomid, RoomList.Date.AddDays(-1));

            var date = RoomList.Date;
            float occupied = RoomList.RoomOccupied;
            float totalRoom = RoomList.TotalRoom;
            float prevOccupied = prevList.RoomOccupied;

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

        private List<Tuple<string, float>> GetPreviousMonthDetails(string selected)
        {
            var prevMonthDetails = new List<Tuple<string, float>>();

            var prevMonthSprRates = GetAvgAmount(repository.GetLatestRoomRates(7, DateTime.Parse(selected).AddMonths(-1)));
            var prevMonthStdRates = GetAvgAmount(repository.GetLatestRoomRates(8, DateTime.Parse(selected).AddMonths(-1)));
            var prevMonthFamRates = GetAvgAmount(repository.GetLatestRoomRates(9, DateTime.Parse(selected).AddMonths(-1)));
            var prevMonthSuiteRates = GetAvgAmount(repository.GetLatestRoomRates(10, DateTime.Parse(selected).AddMonths(-1)));
            var prevMonthDlxRates = GetAvgAmount(repository.GetLatestRoomRates(11, DateTime.Parse(selected).AddMonths(-1)));

            var prevMonthSprOcc = repository.GetLatestRoomTypeOccupancy(7, DateTime.Parse(selected).AddMonths(-1));
            var prevMonthStdOcc = repository.GetLatestRoomTypeOccupancy(8, DateTime.Parse(selected).AddMonths(-1));
            var prevMonthFamOcc = repository.GetLatestRoomTypeOccupancy(9, DateTime.Parse(selected).AddMonths(-1));
            var prevMonthSuiteOcc = repository.GetLatestRoomTypeOccupancy(10, DateTime.Parse(selected).AddMonths(-1));
            var prevMonthDlxOcc = repository.GetLatestRoomTypeOccupancy(11, DateTime.Parse(selected).AddMonths(-1));

            // Calculate ADR/RevPar/Occupancy rate
            var totalOccupancy = repository.GetOccupancyByDate(DateTime.Parse(selected).AddMonths(-1)).RoomOccupied;

            var totalRevenue = (prevMonthSprRates * prevMonthSprOcc.RoomOccupied) +
                (prevMonthStdRates * prevMonthStdOcc.RoomOccupied) +
                (prevMonthFamRates * prevMonthFamOcc.RoomOccupied) +
                (prevMonthSuiteRates * prevMonthSuiteOcc.RoomOccupied) +
                (prevMonthDlxRates * prevMonthDlxOcc.RoomOccupied);

            var adr = totalRevenue / totalOccupancy;
            var revpar = totalRevenue / 71;
            var occupancyRate = totalOccupancy *100 / 71;

            prevMonthDetails.Add(new Tuple<string, float>("adr", adr));
            prevMonthDetails.Add(new Tuple<string, float>("revpar", revpar));
            prevMonthDetails.Add(new Tuple<string, float>("occ", occupancyRate));

            return prevMonthDetails;
        }

        private List<Tuple<string, float>> GetPreviousYearDetails(string selected)
        {
            var prevYearDetails = new List<Tuple<string, float>>();

            var prevYearSprRates = GetAvgAmount(repository.GetLatestRoomRates(7, DateTime.Parse(selected).AddYears(-1)));
            var prevYearStdRates = GetAvgAmount(repository.GetLatestRoomRates(8, DateTime.Parse(selected).AddYears(-1)));
            var prevYearFamRates = GetAvgAmount(repository.GetLatestRoomRates(9, DateTime.Parse(selected).AddYears(-1)));
            var prevYearSuiteRates = GetAvgAmount(repository.GetLatestRoomRates(10, DateTime.Parse(selected).AddYears(-1)));
            var prevYearDlxRates = GetAvgAmount(repository.GetLatestRoomRates(11, DateTime.Parse(selected).AddYears(-1)));

            var prevYearSprOcc = repository.GetLatestRoomTypeOccupancy(7, DateTime.Parse(selected).AddYears(-1));
            var prevYearStdOcc = repository.GetLatestRoomTypeOccupancy(8, DateTime.Parse(selected).AddYears(-1));
            var prevYearFamOcc = repository.GetLatestRoomTypeOccupancy(9, DateTime.Parse(selected).AddYears(-1));
            var prevYearSuiteOcc = repository.GetLatestRoomTypeOccupancy(10, DateTime.Parse(selected).AddYears(-1));
            var prevYearDlxOcc = repository.GetLatestRoomTypeOccupancy(11, DateTime.Parse(selected).AddYears(-1));

            // Calculate ADR/RevPar/Occupancy rate
            var totalOccupancy = repository.GetOccupancyByDate(DateTime.Parse(selected).AddYears(-1)).RoomOccupied;

            var totalRevenue = (prevYearSprRates * prevYearSprOcc.RoomOccupied) +
                (prevYearStdRates * prevYearStdOcc.RoomOccupied) +
                (prevYearFamRates * prevYearFamOcc.RoomOccupied) +
                (prevYearSuiteRates * prevYearSuiteOcc.RoomOccupied) +
                (prevYearDlxRates * prevYearDlxOcc.RoomOccupied);

            var adr = totalRevenue / totalOccupancy;
            var revpar = totalRevenue / 71;
            var occupancyRate = totalOccupancy * 100 / 71;

            prevYearDetails.Add(new Tuple<string, float>("adr", adr));
            prevYearDetails.Add(new Tuple<string, float>("revpar", revpar));
            prevYearDetails.Add(new Tuple<string, float>("occ", occupancyRate));

            return prevYearDetails;
        }

        private List<Tuple<string, float>> GetYesterdayDetails(string selected)
        {
            var yesterdayDetails = new List<Tuple<string, float>>();

            if (repository.GetOccupancyByDate(DateTime.Parse(selected).AddDays(-1)) ==  null)
            {
                var yesterdayModel = repository.GetPredictionByDate(DateTime.Parse(selected).AddDays(-1));

                var totalOccupancy = yesterdayModel.SprOccupancy + yesterdayModel.StdOccupancy + yesterdayModel.FamOccupancy +
                yesterdayModel.SuiteOccupancy + yesterdayModel.DlxOccupancy;
                var totalRevenue = (yesterdayModel.SprRoomRate * yesterdayModel.SprOccupancy) +
                    (yesterdayModel.StdRoomRate * yesterdayModel.StdOccupancy) +
                    (yesterdayModel.FamRoomRate * yesterdayModel.FamOccupancy) +
                    (yesterdayModel.SuiteRoomRate * yesterdayModel.SuiteOccupancy) +
                    (yesterdayModel.DlxRoomRate * yesterdayModel.DlxOccupancy);

                var adr = totalRevenue / totalOccupancy;
                var revpar = totalRevenue / 71;
                var occupancyRate = totalOccupancy * 100 / 71;

                yesterdayDetails.Add(new Tuple<string, float>("adr", adr));
                yesterdayDetails.Add(new Tuple<string, float>("revpar", revpar));
                yesterdayDetails.Add(new Tuple<string, float>("occ", occupancyRate));
            }
            else
            {
                var yesterdaySprRates = GetAvgAmount(repository.GetLatestRoomRates(7, DateTime.Parse(selected).AddDays(-1)));
                var yesterdayStdRates = GetAvgAmount(repository.GetLatestRoomRates(8, DateTime.Parse(selected).AddDays(-1)));
                var yesterdayFamRates = GetAvgAmount(repository.GetLatestRoomRates(9, DateTime.Parse(selected).AddDays(-1)));
                var yesterdaySuiteRates = GetAvgAmount(repository.GetLatestRoomRates(10, DateTime.Parse(selected).AddDays(-1)));
                var yesterdayDlxRates = GetAvgAmount(repository.GetLatestRoomRates(11, DateTime.Parse(selected).AddDays(-1)));

                var yesterdaySprOcc = repository.GetLatestRoomTypeOccupancy(7, DateTime.Parse(selected).AddDays(-1));
                var yesterdayStdOcc = repository.GetLatestRoomTypeOccupancy(8, DateTime.Parse(selected).AddDays(-1));
                var yesterdayFamOcc = repository.GetLatestRoomTypeOccupancy(9, DateTime.Parse(selected).AddDays(-1));
                var yesterdaySuiteOcc = repository.GetLatestRoomTypeOccupancy(10, DateTime.Parse(selected).AddDays(-1));
                var yesterdayDlxOcc = repository.GetLatestRoomTypeOccupancy(11, DateTime.Parse(selected).AddDays(-1));

                // Calculate ADR/RevPar/Occupancy rate
                var totalOccupancy = repository.GetOccupancyByDate(DateTime.Parse(selected).AddDays(-1)).RoomOccupied;

                var totalRevenue = (yesterdaySprRates * yesterdaySprOcc.RoomOccupied) +
                    (yesterdayStdRates * yesterdayStdOcc.RoomOccupied) +
                    (yesterdayFamRates * yesterdayFamOcc.RoomOccupied) +
                    (yesterdaySuiteRates * yesterdaySuiteOcc.RoomOccupied) +
                    (yesterdayDlxRates * yesterdayDlxOcc.RoomOccupied);

                var adr = totalRevenue / totalOccupancy;
                var revpar = totalRevenue / 71;
                var occupancyRate = totalOccupancy * 100 / 71;

                yesterdayDetails.Add(new Tuple<string, float>("adr", adr));
                yesterdayDetails.Add(new Tuple<string, float>("revpar", revpar));
                yesterdayDetails.Add(new Tuple<string, float>("occ", occupancyRate));
            }

            return yesterdayDetails;
        }

        public async Task<IActionResult> Index()
        {
            var CalendarModel = new List<CalendarEvent>();
            
            // Check if database has existing prediction values
            if (repository.CheckPredictions())
            {
                // If database prediction table is empty
                // Calculate predictions for all room types occupancy and rates

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
                    pred.SelectedRoomRate = 1;
                    pred.AdjSprRoomRate = 0;
                    pred.AdjStdRoomRate = 0;
                    pred.AdjFamRoomRate = 0;
                    pred.AdjSuiteRoomRate = 0;
                    pred.AdjDlxRoomRate = 0;

                    PredictionList.Add(pred);

                    // Calculate the ADR
                    var totalOccupancy = pred.SprOccupancy + pred.StdOccupancy + pred.FamOccupancy + pred.SuiteOccupancy + pred.DlxOccupancy;
                    var totalRevenue = (pred.SprRoomRate * pred.SprOccupancy) + (pred.StdRoomRate * pred.StdOccupancy) +
                        (pred.FamRoomRate * pred.FamOccupancy) + (pred.SuiteRoomRate * pred.SuiteOccupancy) +
                        (pred.DlxRoomRate * pred.DlxOccupancy);
                    var adr = totalRevenue / totalOccupancy;
                    var revpar = totalRevenue / 71;
                    var occupancyRate = totalOccupancy / 71;
                    var percent = Math.Round((adr - DEFAULT_ADR) / DEFAULT_ADR * 100, 0);

                    modal.Date = item.Item1;
                    modal.Adr = adr.ToString();
                    modal.RevPar = revpar.ToString();
                    modal.Occupancy = occupancyRate.ToString("#0.##%");
                    modal.SelectedPlan = 1;

                    if (adr > DEFAULT_ADR)
                        modal.Type = "success";
                    else if (adr == DEFAULT_ADR)
                        modal.Type = "warning";
                    else
                        modal.Type = "danger";

                    if (percent < 0)
                        modal.Percent = percent.ToString() + "%";
                    else
                        modal.Percent = "+" + percent.ToString() + "%";

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
                    var percent = Math.Round((adr - DEFAULT_ADR) / DEFAULT_ADR * 100, 2);

                    modal.Date = pred.Date;
                    modal.Adr = adr.ToString();
                    modal.RevPar = revpar.ToString();
                    modal.Occupancy = occupancyRate.ToString("#0.##%");
                    modal.SelectedPlan = pred.SelectedRoomRate;

                    if (adr > DEFAULT_ADR)
                        modal.Type = "success";
                    else if (adr == DEFAULT_ADR)
                        modal.Type = "warning";
                    else
                        modal.Type = "danger";

                    if (percent < 0)
                        modal.Percent = percent.ToString() + "%";
                    else
                        modal.Percent = "+" + percent.ToString() + "%";

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
        public IActionResult Details(string selected, int id = 0)
        {
            ViewData["id"] = selected;
            var model = new DetailsViewModel();
            var selectedModel = repository.GetPredictionByDate(DateTime.Parse(selected));      

            // Calculate ADR/RevPar/Occupancy rate
            var totalOccupancy = selectedModel.SprOccupancy + selectedModel.StdOccupancy + selectedModel.FamOccupancy +
                selectedModel.SuiteOccupancy + selectedModel.DlxOccupancy;

            var totalRevenue = (selectedModel.SprRoomRate * selectedModel.SprOccupancy) + 
                (selectedModel.StdRoomRate * selectedModel.StdOccupancy) +
                (selectedModel.FamRoomRate * selectedModel.FamOccupancy) + 
                (selectedModel.SuiteRoomRate * selectedModel.SuiteOccupancy) +
                (selectedModel.DlxRoomRate * selectedModel.DlxOccupancy);

            var defaultRevenue = (DEFAULT_SPR * selectedModel.SprOccupancy) +
                (DEFAULT_STD * selectedModel.StdOccupancy) +
                (DEFAULT_FAM * selectedModel.FamOccupancy) +
                (DEFAULT_SUITE * selectedModel.SuiteOccupancy) +
                (DEFAULT_DLX * selectedModel.DlxOccupancy);

            var AdjRevenue = (selectedModel.AdjSprRoomRate * selectedModel.SprOccupancy) +
                (selectedModel.AdjStdRoomRate * selectedModel.StdOccupancy) +
                (selectedModel.AdjFamRoomRate * selectedModel.FamOccupancy) +
                (selectedModel.AdjSuiteRoomRate * selectedModel.SuiteOccupancy) +
                (selectedModel.AdjDlxRoomRate * selectedModel.DlxOccupancy);

            var adr = totalRevenue / totalOccupancy;
            var defaultAdr = defaultRevenue / totalOccupancy;
            var adjAdr = AdjRevenue / totalOccupancy;

            var revpar = totalRevenue / 71;
            var defaultRevpar = defaultRevenue / 71;
            var adjRevpar = adjAdr / 71;

            var occupancyRate = totalOccupancy * 100 / 71;

            var prevMonthDetails = GetPreviousMonthDetails(selected);
            var prevYearDetails = GetPreviousYearDetails(selected);
            var yesterdayDetails = GetYesterdayDetails(selected);

            model.Adr = adr.ToString();
            model.RevPar = revpar.ToString();
            model.Occupancy = occupancyRate.ToString();
            model.Date = DateTime.Parse(selected);
            model.SprRate = selectedModel.SprRoomRate.ToString();
            model.StdRate = selectedModel.StdRoomRate.ToString();
            model.FamRate = selectedModel.FamRoomRate.ToString();
            model.SuiteRate = selectedModel.SuiteRoomRate.ToString();
            model.DlxRate = selectedModel.DlxRoomRate.ToString();
            model.yestAdr = yesterdayDetails[0].Item2.ToString();
            model.yestRevPar = yesterdayDetails[1].Item2.ToString();
            model.yestOccupancy = yesterdayDetails[2].Item2.ToString();
            model.prevMonthAdr = prevMonthDetails[0].Item2.ToString();
            model.prevMonthRevPar = prevMonthDetails[1].Item2.ToString();
            model.prevMonthOccupancy = prevMonthDetails[2].Item2.ToString();
            model.prevYearAdr = prevYearDetails[0].Item2.ToString();
            model.prevYearRevPar = prevYearDetails[1].Item2.ToString();
            model.prevYearOccupancy = prevYearDetails[2].Item2.ToString();
            model.selectedPlan = selectedModel.SelectedRoomRate;
            model.defaultAdr = defaultAdr.ToString();
            model.defaultRevpar = defaultRevpar.ToString();
            model.AdjStd = Convert.ToInt32(selectedModel.AdjStdRoomRate);
            model.AdjSpr = Convert.ToInt32(selectedModel.AdjSprRoomRate);
            model.AdjFam = Convert.ToInt32(selectedModel.AdjFamRoomRate);
            model.AdjSuite = Convert.ToInt32(selectedModel.AdjSuiteRoomRate);
            model.AdjDlx = Convert.ToInt32(selectedModel.AdjDlxRoomRate);
            model.adjAdr = adjAdr.ToString();
            model.adjRevpar = adjRevpar.ToString();
            

            if (id != 0)
            {
                selectedModel.SelectedRoomRate = id;
                repository.UpdatePredictions(selectedModel);
                model.selectedPlan = id;
            }

            return View("Details", model);
        }

        [HttpPost]
        public IActionResult Details(DetailsViewModel model)
        {
            var PredModel = repository.GetPredictionByDate(model.Date);

            PredModel.AdjSprRoomRate = model.AdjSpr;
            PredModel.AdjStdRoomRate = model.AdjStd;
            PredModel.AdjFamRoomRate = model.AdjFam;
            PredModel.AdjSuiteRoomRate = model.AdjSuite;
            PredModel.AdjDlxRoomRate = model.AdjDlx;
            PredModel.SelectedRoomRate = model.selectedPlan;
            repository.UpdatePredictions(PredModel);

            var totalOccupancy = PredModel.SprOccupancy + PredModel.StdOccupancy + PredModel.FamOccupancy +
                PredModel.SuiteOccupancy + PredModel.DlxOccupancy;

            var totalRevenue = (PredModel.AdjSprRoomRate * PredModel.SprOccupancy) +
                (PredModel.AdjStdRoomRate * PredModel.StdOccupancy) +
                (PredModel.AdjFamRoomRate * PredModel.FamOccupancy) +
                (PredModel.AdjSuiteRoomRate * PredModel.SuiteOccupancy) +
                (PredModel.AdjDlxRoomRate * PredModel.DlxOccupancy);

            var adr = totalRevenue / totalOccupancy;
            var revpar = totalRevenue / 71;

            model.adjAdr = adr.ToString();
            model.adjRevpar = revpar.ToString();
            
            return View("Details", model);
        }

        [HttpPost]
        public IActionResult WhatIfAnalysis(DetailsViewModel DetailsModel)
        {
            var model = new WhatIfAnalysisViewModel();

            model.Date = DetailsModel.Date;
            model.StdRate = DetailsModel.AdjStd;
            model.SprRate = DetailsModel.AdjSpr;
            model.FamRate = DetailsModel.AdjFam;
            model.SuiteRate = DetailsModel.AdjFam;
            model.DlxRate = DetailsModel.AdjDlx;
            model.DatesLabel = new List<string>();

            model.DatesLabel.Add(DetailsModel.Date.ToString("dd-MM-yyyy"));
            for(int i = 0; i < 6; i++)
            {
                model.DatesLabel.Add(DetailsModel.Date.AddDays(-(i + 1)).ToString("dd-MM-yyyy"));
            }
            model.DatesLabel.Reverse();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
