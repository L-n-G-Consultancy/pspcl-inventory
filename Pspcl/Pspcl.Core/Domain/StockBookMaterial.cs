using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class StockBookMaterial
    {
        public int Id { get; set; }
        public int MaterialGroupId { get; set; } // Foreign key
        public int StockIssueBookId { get; set; }  // Foreign key
        public int MaterialId { get; set; }  // Foreign key
        public int Quantity { get; set; }
        public int SerialNumberFrom { get; set; }
        public int SerialNumberTo { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public StockIssueBook StockIssueBook { get; set; }
        public Material Material { get; set; }
    }
}
