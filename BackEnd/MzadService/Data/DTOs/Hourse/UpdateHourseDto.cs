namespace MzadService.Data.DTOs.Hourse
{
    public class UpdateHourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Breed { get; set; }
        public int YearOfBirth { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
    }
}
