using WUBank.Models.DTOs;
using WUBank.Utils.Database;

namespace WUBank.Models
{
    public class MiniGame
    {
        public string PlayerSteamID { get; set; }
        public decimal Bet { get; set; }
        public int TotalButtons { get; set; }
        public int RightButton { get; set; }
        public string Tip { get; set; }

        public User User { get; set; }
        private DatabaseUtilsUser DbUtils = new DatabaseUtilsUser();
        public void ChangeBet(decimal newBet)
        {
            if (User.Balance < newBet)
            {
                Tip = "Недостаточно баланса";
                return;

            }
            Tip = "Ставка изменена";
            Bet = newBet;
        }
        public void ChangeButtons(int newButtons)
        {
            Tip = "Изменено кол-во кнопок";
            TotalButtons = newButtons;
        }
        public void GenerateGame()
        {
            RightButton = new Random().Next(0, TotalButtons);
            
        }
        public void Play(int chosenButton)
        {
            if(DbUtils.GetUser(PlayerSteamID).Balance<Bet)
            {
                Tip = "У вас недостаточно баланса";
                return;
            }
            if (chosenButton == RightButton)
            {
                Win();
                
                return;
            }
            Lose();
            return;
        }
        public void Win()
        {
            decimal WinAmount = Bet * TotalButtons - Bet;
            User.Balance += WinAmount;
            DbUtils.UpdateUser(PlayerSteamID, User.Balance);
            GenerateGame();
            Tip = "Вы победили";
            // Add to transactions
        }
        public void Lose()
        {
            User.Balance -= Bet;
            DbUtils.UpdateUser(PlayerSteamID, User.Balance);
            GenerateGame();
            Tip = "Вы проиграли";
            // Add to transactions
        }
    }
}
