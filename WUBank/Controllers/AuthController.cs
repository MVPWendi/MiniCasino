using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WUBank.Utils;
using WUBank.Utils.Database;

namespace WUBank.Controllers
{
    /// <summary>
    /// Контроллер Аутентификации через Steam
    /// </summary>
    public class AuthController : Controller
    {

        private DatabaseUtilsUser DBUsers;
        private ILogger<AuthController> _logger;
        public AuthController(DatabaseUtilsUser db, ILogger<AuthController> logger)
        {
            DBUsers = db;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult SignIn(string provider)
        {
            if (string.IsNullOrEmpty(provider)) return BadRequest();

            return Challenge(new AuthenticationProperties { RedirectUri = "Auth/Check" }, provider);
        }
        [HttpGet, HttpPost]
        public IActionResult SignOutUser()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [HttpGet, HttpPost]
        public IActionResult Check()
        {
            //Если пользователя нет, добавляем в бд и перенаправляем на Home/Index
            DBUsers.CreateUserIfNotExist(User);
            return RedirectToAction("Index", "Home");
        }
    }
}
