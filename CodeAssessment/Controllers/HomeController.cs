using CodeAssessment.Models;
using CodeAssessment.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeAssessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDetailsRepositroy _userDetailsRepositroy;

        public HomeController(ILogger<HomeController> logger, IUserDetailsRepositroy userDetailsRepositroy)
        {
            _logger = logger;
            _userDetailsRepositroy = userDetailsRepositroy;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> userDetailList() 
        {
            var userList = await _userDetailsRepositroy.GetAllUserAsync();
            return View(userList);
        }

        public async Task<IActionResult> ExportCsv()
        {
            var csvBytes = await _userDetailsRepositroy.ExportAllToCsvAsync();
            if (csvBytes == null)
            {
                return NotFound();
            }

            var csvFileName = "UserDetails.csv";
            return File(csvBytes, "text/csv", csvFileName);
        }

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