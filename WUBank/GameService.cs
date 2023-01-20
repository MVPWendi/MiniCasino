using WUBank.Models;

namespace WUBank
{
    public class GameService
    {
        public List<MiniGame> Games { get; set; }

        public GameService()
        {
            Games = new List<MiniGame>();
        }
    }
}
