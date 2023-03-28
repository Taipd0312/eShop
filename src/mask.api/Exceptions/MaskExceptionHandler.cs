using Mask.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace Mask.api.Exceptions
{
    public class MaskExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<MaskExceptionHandler> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public MaskExceptionHandler(ILogger<MaskExceptionHandler> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var exception = context.Exception;

                int statusCode;
                string content = string.Empty;

                switch (true)
                {
                    case bool _ when exception is SqlException:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        content = "Something went wrong with our connection. Pleases try again later.";
                        break;

                    case bool _ when exception is InvalidOperationException:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        content = "Something went wrong while processing your request. Pleases try again later.";
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        content = "Something went wrong while processing your request. Pleases try again later.";
                        break;
                }

                _logger.LogError($"GlobalExceptionFilter: Error in {context.ActionDescriptor.DisplayName}. {exception.Message}. {exception.StackTrace}");

                MaskExceptionResponse appApiResult = new MaskExceptionResponse()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = exception.Message,
                    StackTrace = _hostEnvironment.IsDevelopment() ? exception.StackTrace! : null
                };

                var result = new ObjectResult(appApiResult)
                {
                    StatusCode = statusCode
                };

                result.Formatters.Insert(0, new MaskExceptionObjectCollectionConverter());

                context.Result = result;
            }
        }
    }

    public interface IMaskExceptionObjectCollectionConverter : IOutputFormatter
    {
    }

    public class MaskExceptionObjectCollectionConverter : OutputFormatter, IMaskExceptionObjectCollectionConverter
    {
        public MaskExceptionObjectCollectionConverter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return base.CanWriteResult(context);
        }

        public override async Task WriteAsync(OutputFormatterWriteContext context)
        {
            await WriteResponseBodyAsync(context);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            await context.HttpContext.Response.WriteAsJsonAsync(context.Object, options: new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
        }
    }
}
