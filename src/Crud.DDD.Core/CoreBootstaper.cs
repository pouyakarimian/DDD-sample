using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Crud.DDD.Core.Aggregates.UserAggregate.Services;

namespace Crud.DDD.Core
{
    public static class CoreBootstaper
    {
        public static IServiceCollection RegisterCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserManager, UserManager>();

            return services;
        }
    }
}
