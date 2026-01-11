using FilterService.Contracts;
using FilterService.Entities;
using Mapster;
using MongoDB.Entities;
using MzadService.Data.DTOs.Mzad;
using System.Threading.Tasks;

namespace FilterService.Services
{
    public class MzadService: IMzadService
    {
        public MzadService()
        {}

        /// <summary>
        /// Search in Mzad Document with Search term
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<List<MzadDto>> SearchMzad(string search)
        {
            var query =  DB.Find<Mzad>().Sort(s => s.Descending(m=> m.CreatedAt));
            if (!string.IsNullOrEmpty(search))
                query.Match(Search.Full, search).SortByTextScore();

            var result = await query.ExecuteAsync();

            return result.Adapt<List<MzadDto>>();
        }
    }
}
