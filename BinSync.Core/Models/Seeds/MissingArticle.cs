using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Seeds
{
    public class MissingArticle
    {
        public int Id { get; set; }
        public string DatasetId { get; set; }
        public string MessageId { get; set; }
        public string Type { get; set; }
        public string ChunkHash { get; set; }
        public string Status { get; set; } // "Reported", "Verified", "Reuploaded"
        public DateTime ReportedAt { get; set; }
    }
}
