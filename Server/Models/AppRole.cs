using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Server.Models
{
    public class AppRole : IdentityRole
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}