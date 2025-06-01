using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.P2P
{
    public class FailureReport
    {
        public int Id { get; set; }
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string DatasetId { get; set; }
        public string Type { get; set; }  // "RawData", "MetaData", "JournalData"
        public string ChunkHash { get; set; }
        public string Status { get; set; } = "Received"; // "Received", "Verified", "Reuploaded"
        public bool Reported { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string PeerPublicKey { get; set; }
        public string Signature { get; set; }

        // Navigation properties
        public List<FailureReportMessage> Messages { get; set; } = new();
    }

}
