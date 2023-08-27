using ECommerce.Data.Data.Identity;
using ECommerce.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config) 
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("IdentityConnection"), config =>
                {
                    config.MigrationsAssembly("ECommerce.Data");
                });

            });
            services.AddIdentityCore<AppUser>(options =>
            {
                //add identity options here
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            return services;
        }
    }
}
