using ImageService.Configurations;
using ImageService.Interfaces;
using ImageService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageService
{
    public static class ImageExtensions
    {
        public static IServiceCollection AddCloudIMageService(
                this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccountSetting>(
                configuration.GetSection("CloudDinary"));
            return services.AddScoped<IImageService, CloudDinaryService>();
        }
    }
}