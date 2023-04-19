using Microsoft.AspNetCore.Identity;

namespace Pspcl.Core.Domain
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
