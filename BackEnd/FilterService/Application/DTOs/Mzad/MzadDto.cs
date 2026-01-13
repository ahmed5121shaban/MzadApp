using FilterService.Application.DTOs.Hourse;
using FilterService.Entities.Enums;

namespace FilterService.Application.DTOs.Mzad
{
    public class MzadDto
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighTender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime MzadEnd { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; }
        public HourseDto Horse { get; set; }
    }
}
