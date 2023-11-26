using Crud.DDD.Application.Features.User.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crud.DDD.Application
{
    public static class ApplicationBootstraper
    {
        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

            return services;
        }
    }
}
