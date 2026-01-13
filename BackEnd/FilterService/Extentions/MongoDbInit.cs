using FilterService.Entities;
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
        public static async Task MongoIndexes()
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
            
            await SeedingDataAsync();
        }
        private static async Task SeedingDataAsync()
        {
            try
            {
                long count = await DB.CountAsync<Mzad>();
                if (count > 0)
                    return;
                var mzadData = await File.ReadAllTextAsync("Data/Mzad/Mzad.json");
                var mzads = JsonSerializer.Deserialize<List<Mzad>>(mzadData);
                if (mzads != null && mzads.Count > 0)
                {
                    await DB.SaveAsync(mzads);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error with seeding the data ...{ex.Message}");
            }

        }
    }
}
