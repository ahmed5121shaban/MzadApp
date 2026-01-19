using Microsoft.EntityFrameworkCore;
using MzadService.Application.Contracts;
using MzadService.Infrastructure;

namespace MzadService.Application.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;
        public Repository(ApplicationDbContext context)
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
            return entity;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Entity not found");

            _entity.Remove(entity);
        }

        public async Task<T> Add(T entity)
        {
            await _entity.AddAsync(entity);
            return entity;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAllAsyncAsQueryable()
        {
            return _entity.AsQueryable();
        }
    }
}
