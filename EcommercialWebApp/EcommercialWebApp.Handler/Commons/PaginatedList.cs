namespace EcommercialWebApp.Handler.Commons
{
    public class PaginatedList<T>
    {
        public ICollection<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPage { get; set; }
    }
}
