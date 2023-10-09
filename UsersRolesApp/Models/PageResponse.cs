namespace UsersRolesApp.Models
{
    /// <summary>
    /// Модель для пагинации
    /// </summary>
    public class PageResponse
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Конструктор для модели пагинации
        /// </summary>
        public PageResponse(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
