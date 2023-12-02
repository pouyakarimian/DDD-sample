using Crud.DDD.Core.Common;
using Crud.DDD.Host.Services;

namespace Crud.DDD.Host.Exctentions
{
    public static class ServiceCollectionExctention
    {
        public static IServiceCollection RegsiterHostModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
