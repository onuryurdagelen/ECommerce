using ECommerce.Data.Abstracts;
using ECommerce.Data.Concretes;
using ECommerce.Data.Data;
using ECommerce.Data.Extensions;
using ECommerce.Data.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ECommerce.API
{
    public static class ServiceRegistration
    {
        public static void AddApiServices(this IServiceCollection services,ConfigurationManager config)
        {

            string str = config.GetValue<string>("SqlProvider");

             services.AddControllers()
            .AddJsonOptions(options =>
                {
                  options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                });
            services.AddDbContext<StoreContext>(opt =>
            {
                switch (config.GetValue<string>("SqlProvider"))
                {
                    case "SqlServer":
                        opt.UseSqlServer(config.GetConnectionString("SqlServer"), config =>
                        {
                            config.MigrationsAssembly("ECommerce.Data");
                        });
                        break;
                }
                //opt.UseSqlServer(AppSettingsExtension.GetConnectionString("MSSQL"));
            });
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                ConfigurationOptions options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
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
            services.AddAuthentication();
            services.AddAuthorization();
        }
    }
}
