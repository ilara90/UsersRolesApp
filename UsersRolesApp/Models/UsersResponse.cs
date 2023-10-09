namespace UsersRolesApp.Models
{
    /// <summary>
    /// Модель для получения всех пользвателей, с учетом фильтрации, сортировки, пагинации
    /// </summary>
    public class UsersResponse
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        public IEnumerable<User> Users { get; }

        /// <summary>
        /// Модель для пагинации
        /// </summary>
        public PageResponse PageViewModel { get; }


        /// <summary>
        /// Конструктор для общей модели
        /// </summary>
        public UsersResponse(IEnumerable<User> users, PageResponse pageViewModel)
        {
            Users = users;
            PageViewModel = pageViewModel;
        }
    }
}
