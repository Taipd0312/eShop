using Azure;
using FluentValidation;
using Mask.Application.Behaviors;
using Mask.Application.Caching;
using Mask.Application.CQRSs;
using Mask.Application.Interfaces;
using Mask.Application.Interfaces.ProductTypes;
using Mask.Application.Queries;
using Mask.Application.Services;
using Mask.Application.Services.ProductTypes;
using Mask.Application.UnitOfWorks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mask.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection RegisterApplicationModule(this IServiceCollection services, IConfiguration config)
        {
            // Register validator rules
            services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);

            // Add services
            services.AddTransient(typeof(IMaskService<,,>), typeof(MaskService<,,>));
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Add Caching
            services.AddDistributedMemoryCache();
            services.Configure<CacheSettings>(config.GetSection("CacheSettings"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            // Add CQRS
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR( typeof(BaseMaskCoreGetAllEntityQueryHandler<,,>).GetTypeInfo().Assembly);

            return services;
        }
    }
}
