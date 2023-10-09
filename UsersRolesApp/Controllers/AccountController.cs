using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsersRolesApp.Models;

namespace UsersRolesApp.Controllers
{
    /// <summary>
    /// Операции авторизации и аунтификации
    /// </summary>
    /// <returns></returns>
    public class AccountController : Controller
    {
        ApplicationContext db;
        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="loginData">Данные для входа</param>
        /// <returns></returns>
        public IActionResult Login(LoginData loginData)
        {
            User? user = db.Users.FirstOrDefault(u => u.Email == loginData.UserEmail && u.Password == loginData.Password);

            if (user is null)
                return Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = user.Email
            };

            return Json(response);
        }
    }
}
