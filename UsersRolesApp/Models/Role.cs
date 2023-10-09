namespace UsersRolesApp.Models
{
    /// <summary>
    /// Роль
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Пользователи у которых есть роль
        /// </summary>
        public List<User>? Users { get; set; }

        /// <summary>
        /// Таблица ролей и пользователей
        /// </summary>
        public List<UsersRoles>? UsersRoles { get; set; }
    }
}
