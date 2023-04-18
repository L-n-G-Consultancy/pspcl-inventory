using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class StockBookMaterial
    {
        [Key]
        public int Id { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public int MaterialGroupId { get; set; } // Foreign key

        public StockIssueBook StockIssueBook { get; set; }
        public int StockIssueBookId { get; set; }  // Foreign key
        public Material Material { get; set; }
        public int MaterialId { get; set; }  // Foreign key
        public int Quantity { get; set; }
        public int SerialNumberFrom { get; set; }
        public int SerialNumberTo { get; set; }
    }
}
