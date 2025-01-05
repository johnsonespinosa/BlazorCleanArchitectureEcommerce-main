using Application.Commons.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Commons.Interfaces;
using Application.Commons.Services;


namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(UnhandledExceptionBehaviour<,>));
                configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(AuthorizationBehaviour<,>));
                configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(ValidationBehaviour<,>));
                configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(PerformanceBehaviour<,>));
            });

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
