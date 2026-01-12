using FilterService.Contracts;
using FilterService.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MongoDB.Entities;
using MzadService.Data.DTOs.Mzad;
using System.Threading.Tasks;

namespace FilterService.Services
{
    public class MzadService: IMzadService
    {
        public MzadService()
        {}

        public async Task DeleteAllAssync()
        {
            try
            {
                await DB.DropCollectionAsync<Mzad>();
            }catch(Exception ex)
            {
                Console.WriteLine($"Error: Delete All Mzad Filter documents: {ex.Message}");
            }
        }

        /// <summary>
        /// Search in Mzad Document with Search term
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<List<Mzad>> SearchMzad(string search)
        {
            var query =  DB.Find<Mzad>().Sort(s => s.Descending(m=> m.CreatedAt));
            if (!string.IsNullOrEmpty(search))
                query.Match(Search.Full, search).SortByTextScore();

            return await query.ExecuteAsync();
        }
    }
}
