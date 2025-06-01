using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Licensing
{
    public class LicenseFeature
    {
        public int Id { get; set; }
        public int LicenseStateId { get; set; }
        public string Name { get; set; } // "zstd_1_22", "private_seed_management", etc.

        public LicenseState LicenseState { get; set; }
    }
}
