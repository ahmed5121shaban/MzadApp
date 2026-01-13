using FilterService.Data;
using FilterService.Entities;
using MzadService.Data.DTOs.Mzad;

namespace FilterService.Contracts
{
    public interface IMzadService
    {
        public Task<PagedResponse<IEnumerable<Mzad>>> SearchMzad(string search, int pageSize, int pageNumber);
        public Task DeleteAllAssync();
    }
}
