using Application.Interfaces;
using Infrastructure.Data.Contexts;
using Presentation.Server.Exceptions;
using Presentation.Server.Services;

namespace Server
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServerServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddExceptionHandler<ExceptionHandler>();

            return services;
        }
    }
}
