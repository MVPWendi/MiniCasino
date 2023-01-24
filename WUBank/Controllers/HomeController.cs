using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WUBank.Models;
using WUBank.Models.DTOs;
using WUBank.Services;
using WUBank.Utils;
using WUBank.Utils.Database;

namespace WUBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseUtilsUser _db;
        private readonly GameService _gameService;
        public HomeController(ILogger<HomeController> logger, DatabaseUtilsUser db, GameService gameService)
        {
            _logger = logger;
            _db = db;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var model = new HomePageModel(_db, _gameService, User);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}