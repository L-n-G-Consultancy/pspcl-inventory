using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Pspcl.Core.Domain
{
    public class UserRolesMapping
    {
        public int Id { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key to User
        public int RoleId { get; set; } // Foreign key to Roles
        public User User { get; set; }

    }
}
