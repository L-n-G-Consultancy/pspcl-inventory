using Microsoft.AspNetCore.Identity;

namespace Pspcl.Core.Domain
{
    public class UserRole : IdentityUserRole<int>
    {
        public UserRole()
        {
            User = new User();
            Role = new Role();
        }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
