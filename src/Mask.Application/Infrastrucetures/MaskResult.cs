using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mask.Application.Infrastrucetures
{
    public class MaskResult<TResult> : IResult
    {
        private readonly TResult _result;

        public MaskResult(TResult result)
        {
            _result = result;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(_result));
        }
    }
}
