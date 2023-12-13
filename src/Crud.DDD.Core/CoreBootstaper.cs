using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Crud.DDD.Core.Aggregates.UserAggregate.Services;
using Crud.DDD.Core.Aggregates.ProductAggregate.Services;
using Crud.DDD.Core.Aggregates.CatalogAggregate.Services;

namespace Crud.DDD.Core
{
    public static class CoreBootstaper
    {
        public static IServiceCollection RegisterCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ICatalogManager, CatalogManager>();
            services.AddScoped<IProductManager, ProductManager>();

            return services;
        }
    }
}
