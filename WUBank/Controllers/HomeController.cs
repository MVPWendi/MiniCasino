using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WUBank.Models;
using WUBank.Models.DTOs;
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
            if (User.Identity.IsAuthenticated)
            {
                var model = new HomePageModel
                {
                    User = _db.GetUser(User),
                };

                var game = _gameService.Games.Find(x => x.PlayerSteamID == User.ToSteamID());
                if (game == null)
                {
                    game = new MiniGame
                    {
                        PlayerSteamID = User.ToSteamID(),
                        Bet = 1,
                        TotalButtons = 2
                    };
                    _gameService.Games.Add(game);
                }
                game.GenerateGame();
                model.Game = game;
                return View(model);
            }
            return View();
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