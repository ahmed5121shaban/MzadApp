using FilterService.Entities.Enums;
using MongoDB.Entities;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace FilterService.Entities
{
    public class Mzad : Entity
    {
        [JsonPropertyName("reservePrice")]
        public int ReservePrice { get; set; } = 0;
        [JsonPropertyName("seller")]
        public string Seller { get; set; }
        [JsonPropertyName("winner")]
        public string Winner { get; set; }
        [JsonPropertyName("soldAmount")]
        public int? SoldAmount { get; set; }
        [JsonPropertyName("currentHighTender")]
        public int? CurrentHighTender { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [JsonPropertyName("mzadEnd")]
        public DateTime MzadEnd { get; set; } = DateTime.UtcNow;
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

    }
}
