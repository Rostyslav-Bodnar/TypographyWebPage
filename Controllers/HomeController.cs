using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Курсова_робота.Models;
using Курсова_робота.Models.Entities;
using Курсова_робота.Models.ViewModels;

namespace Курсова_робота.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DB database;

        public HomeController(ILogger<HomeController> logger, DB _database)
        {
            database = _database;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var books = database.Editions.ToList();
            var genres = database.Genres.ToList();
            var ads = database.AdvertisingOrders
                .Where(ad => ad.isHavingDesign)
                .ToList();

            var model = new IndexViewModel
            {
                Books = books,
                Genres = genres,
                Ads = ads
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Books()
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
