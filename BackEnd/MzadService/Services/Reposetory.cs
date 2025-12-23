using Microsoft.EntityFrameworkCore;
using MzadService.Contracts;
using MzadService.Infrastructure;

namespace MzadService.Services
{
    public class Reposetory<T> : IReposetory<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;
        public Reposetory(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Entity not found");

            _entity.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _entity.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
