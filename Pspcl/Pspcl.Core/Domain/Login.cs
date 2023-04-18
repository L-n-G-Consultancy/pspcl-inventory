using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class Login
    {
        [Key]
        public int Id { get; set; } // primary key

        public User User { get; set; }
        public int UserId { get; set; } // Foreign key to user
        public DateTime? LoginDate { get; set; }
        public string IpAddress { get; set; }
    }
}
