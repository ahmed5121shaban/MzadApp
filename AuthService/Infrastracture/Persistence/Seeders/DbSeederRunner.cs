using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastracture.Persistence.Seeders
{
    public class DbSeederRunner 
    {
        private readonly IEnumerable<IDbSeeder> _dbSeeders;
        private readonly ApplicationDbContext _context;
        public DbSeederRunner(IEnumerable<IDbSeeder> dbSeeders, ApplicationDbContext context)
        {
            _dbSeeders = dbSeeders;
            _context = context;
        }

        public async Task Run(CancellationToken ct = default)
        {
            await _context.Database.MigrateAsync(ct);  // apply pending migrations first

            foreach (var item in _dbSeeders.OrderBy(d => d.Order))
            {
                await item.SeedAsync(_context, ct);
            }
        }
    }
}
