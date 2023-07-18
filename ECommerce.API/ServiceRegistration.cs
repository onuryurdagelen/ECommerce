using ECommerce.Data.Abstracts;
using ECommerce.Data.Concretes;
using ECommerce.Data.Data;
using ECommerce.Data.Extensions;
using ECommerce.Data.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API
{
    public static class ServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlServer(AppSettingsExtension.GetConnectionString("MSSQL"));
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    string[] errors = actionContext.ModelState
                                        .Where(e => e.Value.Errors.Count > 0)
                                        .SelectMany(x => x.Value.Errors)
                                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

        }
    }
}
