namespace WUBank.Models.DTOs
{
    /// <summary>
    /// DTO пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Стим айди пользователя
        /// </summary>
        public string SteamID { get; set; }
        /// <summary>
        /// Никнейм пользователя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Баланс пользователя
        /// </summary>
        public decimal Balance { get; set; }
    }
}
