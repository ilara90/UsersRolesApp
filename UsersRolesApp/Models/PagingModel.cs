namespace UsersRolesApp.Models
{
    public class PagingModel<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public T SortingField { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
