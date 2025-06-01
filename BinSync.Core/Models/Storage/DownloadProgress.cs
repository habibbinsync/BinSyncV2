using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Storage
{
    public class DownloadProgress
    {
        public int Id { get; set; }
        public string DatasetId { get; set; }
        public string Path { get; set; }
        public int ChunkIndex { get; set; }
        public string MessageId { get; set; }
        public string Status { get; set; } // "Pending", "Downloaded", "Verified", "Failed"
        public string ChunkHash { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
