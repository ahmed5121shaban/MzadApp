using MzadService.Entities;

namespace MzadService.Application.Contracts
{
    public interface IBaseService<T,TUpdate>
    {
        Task<T> Create(T mzadDto);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<TUpdate> Update(TUpdate mzadDto);
        Task Delete(Guid id);
    }
}
