using FilterService.Application.Contracts.Mzad;
using FilterService.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MongoDB.Entities;
using System.Threading.Tasks;

namespace FilterService.Application.Services.Mzad
{
    public class MzadService: IMzadService
    {
        public MzadService()
        {}

        /// <summary>
        /// Asynchronously deletes all documents from the Mzad collection.
        /// </summary>
        /// <remarks>If an error occurs during the deletion, the exception is caught and logged to the
        /// console. The method does not throw on failure.</remarks>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public async Task DeleteAllAssync()
        {
            try
            {
                await DB.DropCollectionAsync<Entities.Mzad>();
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
        public async Task<PagedResponse<IEnumerable<Entities.Mzad>>> SearchMzad(FilterParams filterParams)
        {
            var query =  DB.PagedSearch<Entities.Mzad, Entities.Mzad>();

            // Apply Search Text
            MatchSearchText(query, filterParams);

            // Apply Sorting
            ApplySorting(query, filterParams);

            // Apply Pagination
            query.PageNumber(filterParams.PageNumber);
            query.PageSize(filterParams.PageSize);

            var result = await query.ExecuteAsync();

            return new PagedResponse<IEnumerable<Entities.Mzad>>
            {
                TotalCount = result.TotalCount,
                PageCount = result.PageCount,
                Results = result.Results
            };
        }

        #region Helper Functions
        private void MatchSearchText(PagedSearch<Entities.Mzad, Entities.Mzad> query, FilterParams filterParams)
        {
            if (!string.IsNullOrEmpty(filterParams.Search))
                query.Match(Search.Full, filterParams.Search).SortByTextScore();

            if (!string.IsNullOrEmpty(filterParams.Winner))
                query.Match(m => m.Winner == filterParams.Winner);

            if (!string.IsNullOrEmpty(filterParams.Seller))
                query.Match(m => m.Seller == filterParams.Seller);
        }
        private void ApplySorting(PagedSearch<Entities.Mzad, Entities.Mzad> query, FilterParams filterParams)
        {
            query = filterParams.OrderBy switch
            {
                "sellerName" => query.Sort(m => m.Ascending(h => h.Seller)),
                "winnerName" => query.Sort(m => m.Ascending(h => h.Winner)),
                "new" => query.Sort(m => m.Descending(x => x.CreatedAt)),
                _ => query.Sort(m => m.Ascending(x => x.MzadEnd))
            };

            query = filterParams.FilterBy switch
            {
                "finished" => query.Match(m => m.MzadEnd < DateTime.UtcNow),
                "endingSoon" => query.Match(m => m.MzadEnd < DateTime.UtcNow.AddDays(1)
                    && m.MzadEnd > DateTime.UtcNow),
                _ => query.Match(m => m.MzadEnd > DateTime.UtcNow)
            };
        }
        #endregion
    }
}
