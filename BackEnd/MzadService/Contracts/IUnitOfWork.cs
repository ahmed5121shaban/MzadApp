using MzadService.Entities;


namespace MzadService.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Entities.Mzad> Mzads { get; }
        IRepository<Horse> Horses { get; }

        Task<int> SaveAsync();
    }
}
