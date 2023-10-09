using Microsoft.AspNetCore.Mvc.Rendering;

namespace UsersRolesApp.Models
{
    /// <summary>
    /// Модель для фильтрации
    /// </summary>
    public class UserFilter
    {
        /// <summary>
        /// пагинация
        /// </summary>
        public PagingModel<SortUserField>? Paging { get; set; }        

        /// <summary>
        /// введенное имя
        /// </summary>
        public string? NameUser { get; set; }

        /// <summary>
        /// введенный email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// введенный возраст от
        /// </summary>
        public int? AgeFrom { get; set; }

        /// <summary>
        /// введенный возраст по
        /// </summary>
        public int? AgeTo { get; set; }

        /// <summary>
        /// Id выбранной роль
        /// </summary>
        public int? RoleId { get; set; }
    }
}
