using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UsersRolesApp.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя обязательно")]
        public string? Name { get; set; }

        /// <summary>
        /// Возраст пользователя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Возраст обязателен")]
        [Range(1, 100, ErrorMessage = "Допустимый возраст от 1 до 100")]
        public int Age { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email обязателен")]
        [Remote(action: "CheckEmail", controller: "User", ErrorMessage = "Email уже используется")]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public List<Role>? Roles { get; set; }

        /// <summary>
        /// Таблица ролей и пользователей
        /// </summary>
        public List<UsersRoles>? UsersRoles { get; set; }
    }
}
