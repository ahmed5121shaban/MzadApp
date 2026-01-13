namespace FilterService.Infrastructure.HttpClients
{
    public class MzadServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

        public async Task<List<Entities.Mzad>> GetMzadDataAsync()
        {
            try
            {
                var mzadServiceUrl = _configuration["Services:MzadService"];
                var response = await _httpClient.GetFromJsonAsync<List<Entities.Mzad>>($"{mzadServiceUrl}/api/mzad");
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
