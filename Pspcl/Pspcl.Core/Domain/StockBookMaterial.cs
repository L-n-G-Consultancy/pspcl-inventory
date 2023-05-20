using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class StockBookMaterial
    {
        [Key]
        public int Id { get; set; }
        public int MaterialGroupId { get; set; } // Foreign key
        public int StockIssueBookId { get; set; }  // Foreign key
        public int MaterialId { get; set; }  // Foreign key
        public int Quantity { get; set; }



        // public MaterialGroup MaterialGroup { get; set; }
        /// <summary>
        //public StockIssueBook StockIssueBook { get; set; }
        /// </summary>
        ///         
        /// //public Material Material { get; set; }

    }
}
