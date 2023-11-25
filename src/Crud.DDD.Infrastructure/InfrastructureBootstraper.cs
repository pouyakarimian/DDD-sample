using Crud.DDD.Infrastructure.Data.Context;
using Crud.DDD.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crud.DDD.Infrastructure
{
    public static class InfrastructureBootstraper
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(configuration["ConnectionStrings:Default"]);
                option.AddInterceptors(new ApplicationDbContextInterceptor());
            });
        }
    }
}
