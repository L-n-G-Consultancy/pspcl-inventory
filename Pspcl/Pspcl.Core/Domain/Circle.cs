using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class Circle 
    {
        public int Id { get; set; } // primary key

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public Division Division { get; set; }

        public int DivisionId { get; set; } // Foreign key to Division
        public SubDivision SubDivision { get; set; }

        public int SubDivisionId { get; set; } // Foreign key to SubDivision

    }
}
