using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Contracts.Mzads
{
    public class CreatedMzad
    {
        public int ReservePrice { get; set; } = 0;
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighTender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime MzadEnd { get; set; }
        public Status Status { get; set; }
        public CreatedMzad Horse { get; set; }
    }
    public enum Status
    {
        Live,
        Finished,
        ReserveNotMet
    }
}
