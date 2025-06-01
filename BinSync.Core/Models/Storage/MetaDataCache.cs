using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Storage
{
    public class MetaDataCache
    {
        public int Id { get; set; }
        public string DatasetId { get; set; }
        public string Path { get; set; } // File/directory path
        public byte[] Data { get; set; } // Encrypted metadata
        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;
    }
}
