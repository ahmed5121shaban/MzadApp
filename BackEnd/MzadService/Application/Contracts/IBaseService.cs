using MzadService.Entities;

namespace MzadService.Application.Contracts
{
    public interface IBaseService<T>
    {
        Task<T> Create(T mzadDto);
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T mzadDto);
        Task Delete(Guid id);
    }
}
