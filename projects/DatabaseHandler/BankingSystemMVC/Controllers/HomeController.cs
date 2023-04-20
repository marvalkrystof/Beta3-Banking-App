using BankingSystemMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankingSystemMVC.Controllers
{
/// <summary>
/// The home controller, serves the basic information of the website
/// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Home page
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returned if user doesn't have permissions to view the content
        /// </summary>
        /// <returns>The View</returns>
        public IActionResult NoPermission()
        {
            return View();
        }
        /// <summary>
        /// Privacy 
        /// </summary>
        /// <returns>The View</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns>View</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}