using System.ComponentModel.DataAnnotations;

namespace UsersRolesApp.Models
{
    /// <summary>
    /// Модель для fавторизации
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя обязательно")]
        public string UserEmail { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }
    }
}
