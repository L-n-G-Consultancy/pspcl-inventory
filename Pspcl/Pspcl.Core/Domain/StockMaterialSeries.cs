using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class StockMaterialSeries
    {
        public int StockMaterialId { get; set; } // Foreign key
        public int SerialNumber { get; set; }
        public bool IsIssued { get; set; }
        public StockMaterial StockMaterial { get; set; }

    }
}
