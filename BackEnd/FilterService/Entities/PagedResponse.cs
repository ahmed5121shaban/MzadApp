namespace FilterService.Entities
{
    public class PagedResponse<T>
    {
        public long TotalCount { get; set; }
        public int PageCount { get; set; }
        public T Results { get; set; }
    }
}
