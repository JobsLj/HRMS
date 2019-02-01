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

namespace HRMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISeederRepository repository;

        public LoginController(ISeederRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var client = new HttpClient();
            string output;

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
                        new KeyValuePair<string, string>("client_id", "49968FB3-3492-4243-9A8D-9EB4DB2A1E8D"),
                        new KeyValuePair<string, string>("client_secret", "kCaQGqLBjF"),
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                        new KeyValuePair<string, string>("scope", "gusto_fos_api"),
                    });

            // Get the response
            HttpResponseMessage response = await client.PostAsync("https://id.uog.gustodian.com/connect/token", requestContent);

            // Get the response content
            HttpContent responseContent = response.Content;

            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                output = await reader.ReadToEndAsync();
            }
            var result = JObject.Parse(output);
            var accessToken = result["access_token"].ToString();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            client.DefaultRequestHeaders.Add("X-TENANT-CODE", "TONYSVILLA");

            if (repository.CheckifEmpty())
            {
                // Get the response content
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                var roomDates = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd");
                var occupancyDates = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd");

                // Room Rates
                var res = await client.GetAsync($"https://api.fos.uog.gustodian.com/v1/analytics/room-rates/actual/by-room-type?range_start={roomDates}&range_end={today}");
                var resContent = res.Content;

                using (var outc = new StreamReader(await resContent.ReadAsStreamAsync()))
                {
                    // Write the output.
                    var streamResult = await outc.ReadToEndAsync();
                    repository.SeedRoomRates(streamResult);
                }

                // Occupancy
                var occRes = await client.GetAsync($"https://api.fos.uog.gustodian.com/v1/analytics/statistics?range_start={occupancyDates}&range_end={today}");
                var occResContent = occRes.Content;

                using (var outc = new StreamReader(await occResContent.ReadAsStreamAsync()))
                {
                    // Write the output.
                    var streamResult = await outc.ReadToEndAsync();
                    repository.SeedOccupancy(streamResult);
                }

                // Occupancy By Room Type
                var occResByRoom = await client.GetAsync($"https://api.fos.uog.gustodian.com/v1/analytics/statistics/by-room-type?range_start={occupancyDates}&range_end={today}");
                var occResByRoomContent = occResByRoom.Content;

                using (var outc = new StreamReader(await occResByRoomContent.ReadAsStreamAsync()))
                {
                    // Write the output.
                    var streamResult = await outc.ReadToEndAsync();
                    repository.SeedRoomTypeOccupancy(streamResult);
                }
            }
            // If there's no more new predictions in the database
            // Call the api and retrieve data
            // Once data retrieved, do prediction for the next 14 days
            else if (repository.CheckIfNeedsPredictions(DateTime.Today) == true)
            {
                // Get the response content
                //var today = DateTime.Today.ToString("yyyy-MM-dd");
                //var startDate = repository.GetLatestDate().AddDays(1).ToString("yyyy-MM-dd");

                //// Room Rates
                //var res = await client.GetAsync($"https://api.fos.uog.gustodian.com/v1/analytics/room-rates/daily?range_start={startDate}&range_end={today}");
                //var resContent = res.Content;

                //using (var outc = new StreamReader(await resContent.ReadAsStreamAsync()))
                //{
                //    // Write the output.
                //    var streamResult = await outc.ReadToEndAsync();
                //    repository.SeedRoomRates(streamResult);
                //}

                //// Occupancy
                //var occRes = await client.GetAsync($"https://api.fos.uog.gustodian.com/v1/analytics/statistics?range_start={startDate}&range_end={today}");
                //var occResContent = occRes.Content;

                //using (var outc = new StreamReader(await occResContent.ReadAsStreamAsync()))
                //{
                //    // Write the output.
                //    var streamResult = await outc.ReadToEndAsync();
                //    repository.SeedOccupancy(streamResult);
                //}

                //// Occupancy By Room Type
                //var occResByRoom = await client.GetAsync($"https://api.fos.uog.gustodian.com/v1/analytics/statistics/by-room-type?range_start={startDate}&range_end={today}");
                //var occResByRoomContent = occResByRoom.Content;

                //using (var outc = new StreamReader(await occResByRoomContent.ReadAsStreamAsync()))
                //{
                //    // Write the output.
                //    var streamResult = await outc.ReadToEndAsync();
                //    repository.SeedRoomTypeOccupancy(streamResult);
                //}

            }

            return RedirectToAction("Index", "Home");
        }
    }
}