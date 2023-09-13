using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mask.Application.Validators
{
    public class MaskValidationResponse<TValidatorResult> : IResult
    {
        private readonly TValidatorResult _result;

        public MaskValidationResponse(TValidatorResult result)
        {
            _result = result;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.ContentLength = 0;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(_result));
        }
    }
}
