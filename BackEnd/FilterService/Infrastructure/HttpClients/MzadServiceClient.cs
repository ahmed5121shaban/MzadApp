using MongoDB.Entities;

namespace FilterService.Infrastructure.HttpClients
{
    public class MzadServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Asynchronously retrieves a list of Mzad entities that have been updated since the last recorded update date.
        /// </summary>
        /// <remarks>This method queries the configured Mzad service endpoint for entities updated after
        /// the most recent update date found in the database. The returned data reflects the latest changes available
        /// from the service.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see
        /// cref="Entities.Mzad"/> objects representing the updated Mzad data. If no data is available, the list will be
        /// empty.</returns>
        /// <exception cref="ApplicationException">Thrown when an error occurs while fetching Mzad data from the service.</exception>
        public async Task<List<Entities.Mzad>> GetMzadDataAsync()
        {
            try
            {
                var mzadServiceUrl = _configuration["Services:MzadService"];

                string lastUpdateDate = await DB.Find<Entities.Mzad, string>()
                    .Sort(m => m.Descending(m => m.UpdatedAt))
                    .Project(m => m.UpdatedAt.ToString())
                    .ExecuteFirstAsync();

                var response = await _httpClient
                    .GetFromJsonAsync<List<Entities.Mzad>>($"{mzadServiceUrl}/api/mzad?lastUpdatedDate={lastUpdateDate}");

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Log the exception (logging mechanism not shown here)
                throw new ApplicationException("Error fetching Mzad data", ex);
            }

        }
    }
}
