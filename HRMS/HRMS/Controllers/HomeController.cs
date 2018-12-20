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
    public class HomeController : Controller
    {
        private readonly ISeederRepository repository;

        public HomeController(ISeederRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            if (repository.CheckifEmpty())
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
                client.DefaultRequestHeaders.Add("X-TENANT-CODE", "EDINBURGH");
                var res = await client.GetAsync("https://api.fos.uog.gustodian.com/v1/analytics/room-rates/daily?range_start=2018-11-09&range_end=2018-12-09");

                // Get the response content
                var resContent = res.Content;

                using (var outc = new StreamReader(await resContent.ReadAsStreamAsync()))
                {
                    // Write the output.
                    var test = await outc.ReadToEndAsync();
                    repository.SeedRoomRates(test);
                }

                
            }   
            


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
