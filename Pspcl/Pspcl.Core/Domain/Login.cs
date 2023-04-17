using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Core.Domain
{
    public class Login
    {
        public int Id { get; set; } // primary key
        public int UserId { get; set; } // Foreign key to user
        public DateTime? LoginDate { get; set; }
        public string IpAddress { get; set; }
        public User User { get; set; }
    }
}
