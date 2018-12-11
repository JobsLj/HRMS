using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMS.Models;

namespace HRMS.Components
{
    public class CalendarModal : ViewComponent
    {
        public IViewComponentResult Invoke(string date)
        {
            DailyPredictionModel model = new DailyPredictionModel();
            model.Id = Convert.ToInt32(date);
            return View(model);
        }
    }
}
