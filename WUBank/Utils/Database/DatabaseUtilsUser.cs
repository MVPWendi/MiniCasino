using System.Security.Claims;
using WUBank.Models.DTOs;

namespace WUBank.Utils.Database
{
    /// <summary>
    /// Ещё один класс для работы с базой данных, связан непосредственно с пользователями (вынесен в отдельный класс, на случай большого расширения)
    /// </summary>
    public class DatabaseUtilsUser
    {
        /// <summary>
        /// Вызов хранимой процедуры, "CreateUser", которая создаёт пользователя, если его нет в БД
        /// </summary>
        /// <param name="User"></param>
        public void CreateUserIfNotExist(ClaimsPrincipal User)
        {
            DatabaseUtils.ExecuteCrud("CreateUser", new Dictionary<string, object> {
                {"SteamID", User.ToSteamID()},
                {"Name", User.ToSteamUserName()}
            });
        }
        /// <summary>
        /// Получение пользователя из БД
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public User GetUser(ClaimsPrincipal User)
        {
            return GetUser(User.ToSteamID());
        }
        /// <summary>
        /// Получение пользователя из бд
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
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
        /// <summary>
        /// Обновление баланса пользователя
        /// </summary>
        /// <param name="steamID"></param>
        /// <param name="balance"></param>
        public void UpdateUser(string steamID, decimal balance)
        {
            DatabaseUtils.GetTable($"UPDATE TOP(1) Users SET Balance={balance} WHERE SteamID={steamID}");
        }
    }
}
