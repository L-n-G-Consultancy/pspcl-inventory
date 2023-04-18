using Microsoft.EntityFrameworkCore;

namespace Pspcl.Core.Domain
{
    [Keyless]
    public class StockMaterialSeries
    {
        public StockMaterial StockMaterial { get; set; }
        public int StockMaterialId { get; set; } // Foreign key
        public int SerialNumber { get; set; }
        public bool IsIssued { get; set; }
    }
}