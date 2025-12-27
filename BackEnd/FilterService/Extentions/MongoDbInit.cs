using FilterService.Entities;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Threading.Tasks;

namespace FilterService.Extentions
{
    public static class MongoDbInit
    {
        public static async Task InitMongoDb(string databaseName, string connectionString)
        {
            await DB.InitAsync(databaseName, MongoClientSettings.FromConnectionString(connectionString));
        }
        public static async Task MongoIndexes()
        {
            await DB.Index<Mzad>()
                .Key(m => m.Seller,KeyType.Text)
                .Key(m => m.CreatedAt,KeyType.Descending)
                .Key(m => m.ReservePrice,KeyType.Descending)
                .Key(m => m.Status, KeyType.Descending)
                .CreateAsync();
        }
    }
}
