using CodeAssessment.Models;
using CodeAssessment.Repositories;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Text;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                int newUserId = await _userDetailsRepositroy.CreateUserAsync(userDetail);
                return RedirectToAction(nameof(Index));
            }

            return View(userDetail);
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

        public async Task<IActionResult> ExportToCsv()
        {
            var userDetailList = await _userDetailsRepositroy.GetAllUserAsync();
            if (userDetailList == null)
            {
                return NotFound(); // No Data to export
            }

            //Prepare CSV data
            var csvData = new StringBuilder();
            csvData.AppendLine("Id, Name, Date of Brith, Mobile Number, Email");
            foreach(var item in userDetailList)
            {
                csvData.AppendLine($"{item.Id}, {item.Name}, {item.DateofBirth.ToString("yyyy-mm-dd")}, {item.MobileNumber}, {item.Email}");

            }

            //Generate CSV file
            var csvFileName = "UserDetail.csv";
            var csvBytes = Encoding.UTF8.GetBytes(csvData.ToString());
            var memoryStream = new MemoryStream(csvBytes);

            return File(memoryStream, "text/csv", csvFileName);

        }
    }
}