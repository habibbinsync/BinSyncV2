using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Seeds
{
    public class Seed
    {
        public int Id { get; set; }
        public string SeedId { get; set; }
        public string DatasetId { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsFinalized { get; set; }
        public string IndexKeyHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<SeedAccess> AllowedPeers { get; set; } = new();
    }
}
