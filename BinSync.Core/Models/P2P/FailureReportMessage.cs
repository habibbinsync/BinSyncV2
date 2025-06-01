using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSync.Core.Models.P2P
{
    public class FailureReportMessage
    {
        public int Id { get; set; }
        public int FailureReportId { get; set; }
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public string IdSalt { get; set; }
        public string Newsgroup { get; set; }

        public FailureReport FailureReport { get; set; }
    }
}
