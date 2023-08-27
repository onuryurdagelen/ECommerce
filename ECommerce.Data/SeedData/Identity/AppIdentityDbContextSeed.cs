using ECommerce.Data.Data.Identity;
using ECommerce.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.SeedData.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            //Check If there's no users already created
            if(!userManager.Users.Any()) 
            {
                var user = new AppUser()
                {
                    DisplayName = "Bob",
                    ProfileImageUrl = "",
                    Email = "bob@test.com",
                    UserName = "bobtest",

                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobby",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };
        
                await userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
