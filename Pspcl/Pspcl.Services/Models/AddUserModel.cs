using Microsoft.AspNetCore.Identity;
using Pspcl.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Services.Models
{
    public class AddUserModel : IdentityUser<int>
    {

        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool IsDeleted { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }



    }
    
}
