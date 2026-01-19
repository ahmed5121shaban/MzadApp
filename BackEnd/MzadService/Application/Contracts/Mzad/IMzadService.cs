using MzadService.Application.Contracts;
using MzadService.Application.DTOs.Mzad;

namespace MzadService.Application.Contracts.Mzad
{
    public interface IMzadService : IBaseService<MzadDto, UpdateMzadDto>
    {
        Task<IEnumerable<MzadDto>> GetAllWithLastUpdatedDate(string lastUpdateDate);
    }
}
