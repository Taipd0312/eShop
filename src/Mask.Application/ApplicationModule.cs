using FluentValidation;
using Mask.Application.Caching;
using Mask.Application.Interfaces;
using Mask.Application.Services;
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

            // Add Caching
            services.AddDistributedMemoryCache();
            services.Configure<CacheSettings>(config.GetSection("CacheSettings"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            return services;
        }
    }
}
