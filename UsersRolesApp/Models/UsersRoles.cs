namespace UsersRolesApp.Models
{
    /// <summary>
    /// Модель, которая позволяет соеденить пользователей и роли, которые имеют пользователи в базе данных
    /// </summary>
    public class UsersRoles
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Id роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role? Role { get; set; }
    }
}
