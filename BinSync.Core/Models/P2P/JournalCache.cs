using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.P2P
{
    public class JournalCache
    {
        public int Id { get; set; }
        public string DatasetId { get; set; }
        public byte[] JournalData { get; set; } // Compressed/encrypted
        public DateTime LastUpdated { get; set; }
        public bool IsPrivate { get; set; }
    }
}
