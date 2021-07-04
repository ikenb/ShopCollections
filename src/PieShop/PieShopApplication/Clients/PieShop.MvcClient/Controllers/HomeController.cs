using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PieShop.Core.Models.ViewModels;
using PieShop.Data.Repository.Interfaces;
using System.Diagnostics;

namespace PieShop.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPieRepository _pieRepository;

        public HomeController(ILogger<HomeController> logger, IPieRepository pieRepository)
        {
            _logger = logger;
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var homeModel = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            };

            return View(homeModel);
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
