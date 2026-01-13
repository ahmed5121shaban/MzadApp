using FilterService.Entities;
using FilterService.Infrastructure.HttpClients;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Text.Json;

namespace FilterService.Extentions
{
    public static class MongoDbInit
    {
        public static async Task InitMongoDb(string databaseName, string connectionString)
        {
            try
            {
                await DB.InitAsync(databaseName, MongoClientSettings.FromConnectionString(connectionString));
            }catch(Exception ex)
            {
                Console.WriteLine($"Error in MongoDB Initialization: {ex.Message}");
            }
        }
        public static async Task MongoIndexes(IServiceProvider serviceProvider)
        {
            try
            {
                await DB.Index<Mzad>()
                .Key(m => m.Seller, KeyType.Text)
                .Key(m => m.Winner, KeyType.Text)
                .Key(m => m.ID, KeyType.Text)
                .Key(m => m.CreatedAt, KeyType.Descending)
                .Key(m => m.ReservePrice, KeyType.Descending)
                .Key(m => m.Status, KeyType.Descending)
                .CreateAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in MongoDB Indexes Creation: {ex.Message}");
            }
            
            await SeedingDataAsync(serviceProvider);
        }
        private static async Task SeedingDataAsync(IServiceProvider serviceProvider)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var httpClient = scope.ServiceProvider.GetRequiredService<MzadServiceClient>();
                var mzads =  await httpClient.GetMzadDataAsync();
                if(mzads is null || mzads.Count == 0)
                    return;
                await DB.SaveAsync(mzads);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error with seeding the data ...{ex.Message}");
            }

        }
    }
}
