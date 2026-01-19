namespace MzadService.Application.Contracts
{
    public interface IRepository <T>
    {
        Task<T> GetById (Guid id);
        Task<IEnumerable<T>> GetAll ();
        Task<IQueryable<T>> GetAllAsyncAsQueryable();
        Task<T> Update (T entity);
        Task Delete (Guid id);
        public Task SaveChangesAsync();
    }
}
