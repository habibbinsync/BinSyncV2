using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Files
{
    public class NzbProgress
    {
        public int Id { get; set; }
        public string NzbFileName { get; set; }
        public string MessageId { get; set; }
        public string Status { get; set; } // "Queued", "Downloaded", "Failed"
        public string Newsgroup { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
