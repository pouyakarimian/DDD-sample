using Crud.DDD.Core.Aggregates.UserAggregate.Repositories;
using Crud.DDD.Core.Common;
using Crud.DDD.Infrastructure.Data.Context;
using Crud.DDD.Infrastructure.Data.Interceptors;
using Crud.DDD.Infrastructure.Data.Repositories;
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
            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(configuration["ConnectionStrings:Default"]);
                option.AddInterceptors(new ApplicationDbContextInterceptor());
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            return services;
        }
    }
}
