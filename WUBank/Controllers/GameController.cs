using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WUBank.Models;
using WUBank.Models.DTOs;
using WUBank.Services;
using WUBank.Utils;
using WUBank.Utils.Database;

namespace WUBank.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseUtilsUser _db;
        private readonly GameService _gameService;
        public GameController(ILogger<HomeController> logger, DatabaseUtilsUser db, GameService gservice)
        {
            _logger = logger;
            _db = db;
            _gameService = gservice;
        }

        [HttpPost]
        public IActionResult GameButton(int button)
        {
            var game = _gameService.Games.Find(x => x.PlayerSteamID == User.ToSteamID());
            if (game != null) 
                game.Play(button);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult ChangeBet(int newBet)
        {
            var game = _gameService.Games.Find(x => x.PlayerSteamID == User.ToSteamID());
            if(game!=null)
                game.ChangeBet(newBet);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult ChangeButtons(int newButtons)
        {
            var game = _gameService.Games.Find(x => x.PlayerSteamID == User.ToSteamID());
            if (game != null)
                game.ChangeButtons(newButtons);
            return RedirectToAction("Index", "Home");
        }
    }
}