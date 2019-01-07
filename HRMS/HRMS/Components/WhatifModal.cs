using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Components
{
    public class WhatifModal : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}