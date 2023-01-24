using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WUBank.Controllers;
using WUBank.Models.DTOs;
using WUBank.Utils;
using WUBank.Utils.Database;

namespace WUBank.Models
{
    public class HomePageModel
    {
        public User User { get; set; }
        public MiniGame Game { get; set; }

        private readonly DatabaseUtilsUser _db;
        private readonly GameService _gameService;
        public HomePageModel(DatabaseUtilsUser db, GameService gameService, ClaimsPrincipal User)
        {
            _db = db;
            _gameService = gameService;
            if (User.Identity.IsAuthenticated)
            {
                this.User = _db.GetUser(User);
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
                this.Game = game;
            }            
        }
    }
}
