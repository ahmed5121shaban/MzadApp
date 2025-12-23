using MzadService.Data.DTOs.Mzad;

namespace MzadService.Contracts
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
