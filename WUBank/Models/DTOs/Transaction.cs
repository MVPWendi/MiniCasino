namespace WUBank.Models.DTOs
{
    public class Transaction
    {
        public DateTime date { get; set; }
        public string SteamID { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public decimal? WinAmount { get; set; }
        public int ID { get; set; }
    }
}
