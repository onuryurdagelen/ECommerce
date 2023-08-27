
using Microsoft.AspNetCore.Identity;
using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Entities.Identity
{
    public class AppUser:IdentityUser
    {
        public string DisplayName { get; set; }
        public string ProfileImageUrl { get; set; }
        public AppUserStatus AppUserStatus { get; set; } = AppUserStatus.Active;
        public Address Address { get; set; }
    }
    public enum AppUserStatus
    {
        Active = 1,
        Inactive = 0,
    }
}
