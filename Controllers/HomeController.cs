using FeatureManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManagerSnapshot _featureManager;

        public HomeController(ILogger<HomeController> logger, IFeatureManagerSnapshot featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IsPrintEnabled = await _featureManager.IsEnabledAsync(nameof(FeatureFlag.Print)) ? "On" : "Off";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [FeatureGate(RequirementType.Any, FeatureFlag.Print, FeatureFlag.PrintPreview)]
        public IActionResult Print()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
