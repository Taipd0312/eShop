using System.Globalization;

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
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            // Call the next delegate/middleware in the pipeline.
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
