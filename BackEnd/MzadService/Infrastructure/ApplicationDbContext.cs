using MassTransit;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the OutBox pattern entities
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddTransactionalOutboxEntities();
            modelBuilder.AddInboxStateEntity();
        }
    }
}
