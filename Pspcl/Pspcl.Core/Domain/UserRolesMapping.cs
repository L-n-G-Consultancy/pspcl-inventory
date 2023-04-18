using Microsoft.AspNetCore.Identity;

namespace Pspcl.Core.Domain
{
    public class UserRolesMapping:IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
