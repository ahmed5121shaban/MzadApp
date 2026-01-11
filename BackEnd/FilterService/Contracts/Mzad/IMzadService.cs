using MzadService.Data.DTOs.Mzad;

namespace FilterService.Contracts
{
    public interface IMzadService
    {
        public Task<List<MzadDto>> SearchMzad(string search);
    }
}
