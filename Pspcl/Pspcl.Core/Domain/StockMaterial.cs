using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class StockMaterial
    {
        public int Id { get; set; }
        public int MaterialGroupId { get; set; } // Foreign key
        public int MaterialId { get; set; } // Foreign key
        public int Quantity { get; set; }
        public int SerialNumberFrom { get; set; }
        public int SerialNumberTo { get; set; }
        public int StockId { get; set; } // Foreign key
        public int DisplayOrder { get; set; }

        public MaterialGroup MaterialGroup { get; set; }
        public Material Material { get; set; }
        public Stock Stock { get; set; }
    }
}
