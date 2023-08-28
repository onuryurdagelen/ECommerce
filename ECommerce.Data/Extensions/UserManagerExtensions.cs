using ECommerce.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByUserClaimsPrincipleWithAddress(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            string email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users
                .Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Email == email);
        }
        public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager,
            ClaimsPrincipal user)
        {
            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}
