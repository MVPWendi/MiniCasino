using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using WUBank.Models;
using static WUBank.Models.SteamUserModel;

namespace WUBank.Utils
{
    /// <summary>
    /// Класс расширения для ClaimsPrincipal
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Переводит ClaimsPrincipal к строке SteamID вида "76561198...."
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ToSteamID(this ClaimsPrincipal User)
        {
            if (User.Identity?.IsAuthenticated == false) throw new Exception("Пользователь не авторизован");

            return User.Claims.ElementAt(0).Value.Replace("https://steamcommunity.com/openid/id/", "");
        }

        /// <summary>
        /// Метод отправляет http запрос на api steam и возвращает имя профиля пользователя
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static string ToSteamUserName(this ClaimsPrincipal User)
        {
            using (HttpClient client = new HttpClient())
            {
                    var SteamID = User.ToSteamID();
                    string uri = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=6C693BCD04975E2A69F2445252F3F19A&steamids={SteamID}";
                    HttpResponseMessage response = client.GetAsync(uri).Result;
                    var SteamInfo = response.Content.ReadAsStringAsync().Result;
                    var info = JsonConvert.DeserializeObject<SteamUser>(SteamInfo);
                    return info==null ? "default" : info.response.players[0].personaname;
            }
        }
    }
}
