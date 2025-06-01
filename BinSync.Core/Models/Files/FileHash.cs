using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.Files
{
    public class FileHash
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Sha256Hash { get; set; }
        public long FileSize { get; set; }
        public DateTime LastVerified { get; set; }
    }
}
