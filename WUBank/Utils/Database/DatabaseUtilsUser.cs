using System.Security.Claims;
using WUBank.Models.DTOs;

namespace WUBank.Utils.Database
{
    public class DatabaseUtilsUser
    {

        public void CreateUserIfNotExist(ClaimsPrincipal User)
        {
            DatabaseUtils.ExecuteCrud("CreateUser", new Dictionary<string, object> {
                {"SteamID", User.ToSteamID()},
                {"Name", User.ToSteamUserName()}
            });
        }


        public User GetUser(ClaimsPrincipal User)
        {
            return GetUser(User.ToSteamID());
        }
        public User GetUser(string steamID)
        {
            var row = DatabaseUtils.GetTable($"SELECT TOP(1) * FROM Users WHERE SteamID={steamID}").FirstOrDefault();
            User user = new User();
            if (row != null)
            {
                user.SteamID = row.TryGetValue("SteamID", out var ID) ? (string)ID : throw new NullReferenceException();
                user.Balance = row.TryGetValue("Balance", out var balance) ? (decimal)balance : throw new NullReferenceException();
                user.UserName = row.TryGetValue("UserName", out var name) ? (string)name : throw new NullReferenceException();
            }
            return user;
        }


        public void UpdateUser(string steamID, decimal balance)
        {
            DatabaseUtils.GetTable($"UPDATE TOP(1) Users SET Balance={balance} WHERE SteamID={steamID}");
        }
    }
}
