namespace Mask.api.Routing
{
    public class MaskRoutHandlerFilter : IEndpointFilter
    {
        private ILogger _logger;

        public MaskRoutHandlerFilter(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger("RoutHandler");
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var text = context.HttpContext.GetEndpoint();

            if (text != null && text.Equals("Error").Equals("Error"))
            {
                _logger.LogInformation("Error.");
                return Results.Problem("This is a minimal example of an error.");
            }

            _logger.LogInformation("Success.");
            return await next(context);
        }
    }
}
