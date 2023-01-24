using WUBank.Models.DTOs;
using WUBank.Utils.Database;

namespace WUBank.Models
{
    /// <summary>
    /// Класс мини игры
    /// </summary>
    public class MiniGame
    {
        /// <summary>
        /// Стимайди игрока
        /// </summary>
        public string PlayerSteamID { get; set; }
        /// <summary>
        /// Ставка
        /// </summary>
        public decimal Bet { get; set; }
        /// <summary>
        /// Кол-во кнопок, из которых нужно выбрать правильную
        /// </summary>
        public int TotalButtons { get; set; }
        /// <summary>
        /// Номер правильной кнопки
        /// </summary>
        public int RightButton { get; set; }
        /// <summary>
        /// Подсказка (пишется после любого действия)
        /// </summary>
        public string Tip { get; set; }
        /// <summary>
        /// Поле для работы с БД
        /// </summary>
        private DatabaseUtilsUser DbUtils = new DatabaseUtilsUser();
        /// <summary>
        /// Метод для изменения ставки 
        /// </summary>
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
        /// <summary>
        /// Метод для изменения количества кнопок
        /// </summary>
        public void ChangeButtons(int newButtons)
        {
            Tip = "Изменено кол-во кнопок";
            TotalButtons = newButtons;
        }
        /// <summary>
        /// Метод генерирующий новую правильную кнопку
        /// </summary>
        public void GenerateGame()
        {
            RightButton = new Random().Next(0, TotalButtons);

        }
        /// <summary>
        /// Метод запуска игры, проверяет нужную ли клавишу нажал пользователь и вызывает метод EndGame
        /// </summary>
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
        /// <summary>
        /// Метод конца игры, производит расчёт, добавляет запись в транзакции и генерирует правильный ответ заново
        /// </summary>
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
                DatabaseUtils.InsertTransaction(win, PlayerSteamID, Bet, WinAmount);
            }
            if (!win)
            {
                user.Balance -= Bet;
                DbUtils.UpdateUser(PlayerSteamID, user.Balance);
                GenerateGame();
                Tip = "Вы проиграли";
                DatabaseUtils.InsertTransaction(win, PlayerSteamID, Bet);
            }

        }
    }
}
