using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Storage
{
    public class UploadProgress
    {
        public int Id { get; set; }
        public string DatasetId { get; set; }
        public string MessageId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; } // "Pending", "Uploaded", "Failed"
        public string Newsgroup { get; set; }
        public string Subject { get; set; }
        public string IdSalt { get; set; }
        public long BytesUploaded { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
