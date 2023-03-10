namespace WUBank.Models
{
    /// <summary>
    /// Модель стим пользователя, которую возвращает api steam, нас интересуют только поле personaname
    /// </summary>
    public class SteamUserModel
    {
        public class Player
        {
            public string steamid { get; set; }
            public int communityvisibilitystate { get; set; }
            public int profilestate { get; set; }
            /// <summary>
            /// Имя пользователя  (никнейм)
            /// </summary>
            public string personaname { get; set; }
            public string profileurl { get; set; }
            public string avatar { get; set; }
            public string avatarmedium { get; set; }
            public string avatarfull { get; set; }
            public string avatarhash { get; set; }
            public int personastate { get; set; }
            public string realname { get; set; }
            public string primaryclanid { get; set; }
            public int timecreated { get; set; }
            public int personastateflags { get; set; }
            public string loccountrycode { get; set; }
            public string locstatecode { get; set; }
            public int loccityid { get; set; }
        }

        public class Response
        {
            public List<Player> players { get; set; }
        }

        public class SteamUser
        {
            public Response response { get; set; }
        }
    }
}
