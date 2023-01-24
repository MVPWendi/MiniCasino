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
        private DatabaseUtilsUser DbUtils = new DatabaseUtilsUser();

        public void ChangeBet(decimal newBet)
        {
            var User = DbUtils.GetUser(PlayerSteamID);
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
            if (DbUtils.GetUser(PlayerSteamID).Balance < Bet)
            {
                Tip = "У вас недостаточно баланса";
                return;
            }
            if (chosenButton == RightButton)
            {
                EndGame(true);

                return;
            }
            EndGame(false);
            return;
        }
        private void EndGame(bool win)
        {
            var user = DbUtils.GetUser(PlayerSteamID);
            if (win)
            {
                decimal WinAmount = Bet * TotalButtons - Bet;

                user.Balance += WinAmount;
                DbUtils.UpdateUser(PlayerSteamID, user.Balance);
                GenerateGame();
                Tip = "Вы победили";
                DatabaseUtils.GetTable($"INSERT INTO TRANSACTIONS (ID, Date, SteamID, Amount, Status, WinAmount) VALUES ((SELECT ISNULL(MAX(id) + 1, 0) FROM Transactions), GETDATE(), {PlayerSteamID}, {Bet}, N\'Выигрыш\', {WinAmount})");

            }
            if (!win)
            {
                user.Balance -= Bet;
                DbUtils.UpdateUser(PlayerSteamID, user.Balance);
                GenerateGame();
                Tip = "Вы проиграли";
                DatabaseUtils.GetTable($"INSERT INTO TRANSACTIONS (ID, Date, SteamID, Amount, Status) VALUES ((SELECT ISNULL(MAX(id) + 1, 0) FROM Transactions), GETDATE(), {PlayerSteamID}, {Bet}, N\'Проигрыш\')");

            }

        }
    }
}
