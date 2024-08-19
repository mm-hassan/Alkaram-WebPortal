using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;
using WebPortal.Data;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PortalContext _context;  // Add the DbContext here

        public HomeController(ILogger<HomeController> logger, PortalContext context) 
        {
            _context = context;
            _logger = logger;
        }

        // Update the Index action to fetch data from the database
        public async Task<IActionResult> Index()
        {
            return View();
        }




        public async Task<IActionResult> Folders(string division)
        {
            var divisions = _context.Divisions.ToList(); // Retrieve all divisions
            var doucumenttypes = _context.DocumentTypes.ToList(); // Retrieve data for the second model

            var viewModel = new DocumentViewModel
            {
                Divisions = divisions,
                DocumentTypes = doucumenttypes
            };

            ViewBag.Division = division;

            return View(viewModel);
        }







        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> Services()
        {

            return View();

            //var divisions = _context.Divisions.ToList(); // Retrieve all divisions
            //var doucumenttypes = _context.DocumentTypes.ToList(); // Retrieve data for the second model

            //var viewModel = new DocumentViewModel
            //{
            //    Divisions = divisions,
            //    DocumentTypes = doucumenttypes
            //};

            //return View(viewModel);
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
