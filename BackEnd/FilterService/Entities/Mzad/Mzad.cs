using FilterService.Entities.Enums;
using MongoDB.Entities;
using System.Net.NetworkInformation;

namespace FilterService.Entities
{
    public class Mzad : Entity
    {
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighTender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime MzadEnd { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; }

    }
}
