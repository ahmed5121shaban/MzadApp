using MzadService.Application.Contracts;
using MzadService.Entities;
using MzadService.Infrastructure;

namespace MzadService.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Entities.Mzad> Mzads { get; }
        public IRepository<Horse> Horses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Mzads = new Repository<Entities.Mzad>(_context);
            Horses = new Repository<Horse>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

