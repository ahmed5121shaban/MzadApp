using MzadService.Entities;


namespace MzadService.Application.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Entities.Mzad> Mzads { get; }
        IRepository<Horse> Horses { get; }

        Task<int> SaveAsync();
    }
}
