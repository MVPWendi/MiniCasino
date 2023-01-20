using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using WUBank.Models;
using static WUBank.Models.SteamUserModel;

namespace WUBank.Utils
{
    public static class UserExtensions
    {
        public static string ToSteamID(this ClaimsPrincipal User)
        {
            if (User.Identity?.IsAuthenticated == false) throw new Exception("Пользователь не авторизован");

            return User.Claims.ElementAt(0).Value.Replace("https://steamcommunity.com/openid/id/", "");
        }


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
