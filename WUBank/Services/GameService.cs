using WUBank.Models;

namespace WUBank.Services
{
    /// <summary>
    /// Сервис с состояниями всех игр в текущей сессии
    /// </summary>
    public class GameService
    {
        /// <summary>
        /// Список активных игр
        /// </summary>
        public List<MiniGame> Games { get; set; }

        public GameService()
        {
            Games = new List<MiniGame>();
        }
    }
}
