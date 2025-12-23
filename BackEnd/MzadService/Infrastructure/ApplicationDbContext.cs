using Microsoft.EntityFrameworkCore;
using MzadService.Entities;

namespace MzadService.Infrastructure
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        DbSet<Mzad> Mzads { get; set; }
        DbSet<Horse> Horses { get; set; }
    }
}
