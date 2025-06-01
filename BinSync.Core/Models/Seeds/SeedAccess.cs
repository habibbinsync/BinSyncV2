using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Seeds
{
    public class SeedAccess
    {
        public int Id { get; set; }
        public int SeedId { get; set; }
        public string PeerPublicKey { get; set; }
        public string Permissions { get; set; } // "read", "write", "admin"
        public Seed Seed { get; set; }
    }
}
