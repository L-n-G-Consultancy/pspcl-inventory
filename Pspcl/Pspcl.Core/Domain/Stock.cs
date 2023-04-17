using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class Stock
    {
        public int Id { get; set; } // primary key
        public string TransactionId { get; set; }
        public DateTime? GrnDate { get; set; }
        public int GrnNumber { get; set; }
        public string TestReportReference { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Rate { get; set; }
        public string PrefixNumber { get; set; }

    }
}
