using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Licensing
{
    public class UsageTracking
    {
        public int Id { get; set; }
        public string LicenseId { get; set; }
        public long UploadBytes { get; set; }
        public long DownloadBytes { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
