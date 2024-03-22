﻿using Gestalt.Example.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gestalt.Example.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            _Logger = logger;
        }

        private readonly ILogger<HomeController> _Logger;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();
    }
}