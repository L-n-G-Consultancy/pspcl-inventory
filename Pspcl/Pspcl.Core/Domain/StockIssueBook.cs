using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class StockIssueBook
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string SerialNumber { get; set; }
        public int DivisionId { get; set; } // Foreign key
        public int SubDivisionId { get; set; }  // Foreign key
        public int CircleId { get; set; }  // Foreign key
        public string JuniorEngineerName { get; set; }
        public Division Division { get; set; }
        public SubDivision SubDivision { get; set; }
        public Circle Circle { get; set; }

    }
}
