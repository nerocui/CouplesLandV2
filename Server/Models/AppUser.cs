using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Server.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserNickName> UserNickNames { get; set; }
    }
}