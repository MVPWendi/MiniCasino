namespace WUBank.Models.DTOs
{

    /// <summary>
    /// DTO тразакции
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Стим айди игрока
        /// </summary>
        public string SteamID { get; set; }
        /// <summary>
        /// Ставка
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Статус (Выигрыш проигрыш)
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Сумма выигрыша
        /// </summary>
        public decimal? WinAmount { get; set; }
        /// <summary>
        /// уникальный ID транзакции
        /// </summary>
        public int ID { get; set; }
    }
}
