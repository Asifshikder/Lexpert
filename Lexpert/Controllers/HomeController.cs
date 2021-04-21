using Lexpert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            var ffprod = db.Product.Where(s => s.IsFeature == true).ToList();
            return View(ffprod);
        }

        public IActionResult Privacy()
        {
            var model = db.Privacy.FirstOrDefault();
            return View(model);
        }
        public IActionResult Contact()
        {
            var model = db.ContactUs.FirstOrDefault();
            return View(model);
        }
        public IActionResult Aboutus()
        {
            var model = db.AboutUs.FirstOrDefault();
            return View(model);
        }
        public IActionResult Search(string search)
        {
            var prod = db.Product.Where(s=>s.ProductName.Contains(search)||s.Description.Contains(search)).ToList();
            ViewBag.keyword = search;
            return View(prod);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
