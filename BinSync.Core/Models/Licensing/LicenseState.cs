using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Licensing
{
    public class LicenseState
    {
        public int Id { get; set; }
        public string LicenseId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; } // "free", "paid"
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string DeviceId { get; set; }
        public string Signature { get; set; }

        public List<LicenseFeature> Features { get; set; } = new();
    }

}
