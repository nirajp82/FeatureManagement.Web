using FeatureManagement.Web.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IFeatureManagerSnapshot featureManager,
                              IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _featureManager = featureManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            //for (int i = 1; i < 10; i++)
            //{
            //    ViewBag.PrintMessage += (await _featureManager.IsEnabledAsync(nameof(FeatureFlag.Print))).ToString() + ", ";
            //    await Task.Delay(1);
            //}
            ViewBag.IsPrintEnabled = await _featureManager.IsEnabledAsync(nameof(FeatureFlag.Print)) ? "On" : "Off";
            ViewBag.IsAdmin = await _featureManager.IsEnabledAsync(nameof(FeatureFlag.Admin)) ? "Yes" : "No";
            ViewBag.SessionId = _contextAccessor.HttpContext.Session.Id;
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

        [FeatureGate( FeatureFlag.Admin)]
        public IActionResult Admin()
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
