using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pspcl.Core.Domain
{
    public class User: IdentityUser
    {
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}
