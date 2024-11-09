using LR11.CustomFilters;
using LR11.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LR11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [CustomActionLogFilter("CustomActionLog.txt")]
        public IActionResult Index()
        {
            return View();
        }

        [CustomActionLogFilter("CustomActionLog.txt")]
        public IActionResult Privacy()
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
