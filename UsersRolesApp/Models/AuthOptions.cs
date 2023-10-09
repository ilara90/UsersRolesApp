using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UsersRolesApp.Models
{
    /// <summary>
    /// Модель для настроек генерации токена
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// издатель токена
        /// </summary>
        public const string ISSUER = "MyAuthServer";

        /// <summary>
        /// потребитель токена
        /// </summary>
        public const string AUDIENCE = "MyAuthClient";

        /// <summary>
        /// ключ для шифрации
        /// </summary>
        const string KEY = "mysupersecret!60q%9CXS0u";

        /// <summary>
        /// Возвращает ключ безопасности, который применяется для генерации токена
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
