using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class Stock
    {
        [Key]
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


