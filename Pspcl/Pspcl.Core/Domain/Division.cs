using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class Division 
    {
        [Key]
        public int Id { get; set; } // primary key

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public SubDivision SubDivision { get; set; }

        public int SubDivisionId { get; set; } // Foreign key to SubDivision

    }
}
