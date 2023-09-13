using System.Text;

namespace Mask.api.Middleware
{
    public class MaskMiddleware
    {
        private readonly RequestDelegate _next;

        public MaskMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }

    public static class MaskMiddlewareExtensions
    {
        public static IApplicationBuilder UseMaskMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MaskMiddleware>();
        }
    }
}
