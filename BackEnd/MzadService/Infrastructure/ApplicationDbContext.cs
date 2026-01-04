using Microsoft.EntityFrameworkCore;
using MzadService.Entities;

namespace MzadService.Infrastructure
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Mzad> Mzads { get; set; }
        public DbSet<Horse> Horses { get; set; }

    }
}
