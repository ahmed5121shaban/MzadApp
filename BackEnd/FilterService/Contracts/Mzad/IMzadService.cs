using FilterService.Entities;
using MzadService.Data.DTOs.Mzad;

namespace FilterService.Contracts
{
    public interface IMzadService
    {
        public Task<List<Mzad>> SearchMzad(string search);
        public Task DeleteAllAssync();
    }
}
