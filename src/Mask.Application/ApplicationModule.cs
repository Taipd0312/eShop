using FluentValidation;
using Mask.Application.Caching;
using Mask.Application.Interfaces.ProductTypes;
using Mask.Application.Services.ProductTypes;
using Mask.Application.UnitOfWorks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mask.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection RegisterApplicationModule(this IServiceCollection services, IConfiguration config)
        {
            // Register validator rules
            services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);

            // Add services
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Add Caching
            services.AddDistributedMemoryCache();
            services.Configure<CacheSettings>(config.GetSection("CacheSettings"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            return services;
        }
    }
}
