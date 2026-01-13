using FilterService.Application.DTOs.Mzad;

namespace FilterService.Application.DTOs.Hourse
{
    public class HourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Breed { get; set; }
        public int YearOfBirth { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public MzadDto Mzad { get; set; }
        public Guid MzadId { get; set; }
    }
}
