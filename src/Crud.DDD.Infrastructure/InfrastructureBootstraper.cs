using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Infrastructure.Data.Context;
using Crud.DDD.Infrastructure.Data.Interceptors;
using Crud.DDD.Infrastructure.Data.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crud.DDD.Infrastructure
{
    public static class InfrastructureBootstraper
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:Default"]);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
