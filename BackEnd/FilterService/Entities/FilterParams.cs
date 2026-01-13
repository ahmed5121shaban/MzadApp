namespace FilterService.Entities
{
    public class FilterParams
    {
        public string Search { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        public string FilterBy { get; set; }
        public string Winner { get; set; }
        public string Seller { get; set; }
    }
}
