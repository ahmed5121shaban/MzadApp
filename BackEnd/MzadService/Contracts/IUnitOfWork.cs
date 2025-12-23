using MzadService.Entities;


namespace MzadService.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Entities.Mzad> Mzads { get; }
        IRepository<Entities.Horse> Horses { get; }

        Task<int> SaveAsync();
    }
}
