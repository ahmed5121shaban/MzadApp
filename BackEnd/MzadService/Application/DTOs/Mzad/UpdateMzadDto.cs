using MzadService.Application.DTOs.Hourse;
using MzadService.Entities.Enums;

namespace MzadService.Application.DTOs.Mzad
{
    public class UpdateMzadDto
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighTender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime MzadEnd { get; set; }
        public Status Status { get; set; }
        public UpdateHourseDto Horse { get; set; }
    }
}
