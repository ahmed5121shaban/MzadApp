namespace MzadService.Contracts
{
    public interface IRepository <T>
    {
        Task<T> GetById (Guid id);
        Task<IEnumerable<T>> GetAll ();
        Task<T> Update (T entity);
        Task Delete (Guid id);
        public Task SaveChangesAsync();
    }
}
