using FilterService.Entities;

namespace FilterService.Application.Contracts.Mzad
{
    public interface IMzadService
    {
        public Task<PagedResponse<IEnumerable<Entities.Mzad>>> SearchMzad(FilterParams filterParams);
        public Task DeleteAllAssync();
    }
}
