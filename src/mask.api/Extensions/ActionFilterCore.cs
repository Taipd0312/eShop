using Mask.api.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mask.api.Extensions
{
    public class ActionFilterCore : IActionFilter
    {
        private MaskRequestCore _request = new MaskRequestCore();
        private MaskResponseCore? _response;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var responseObject = (ObjectResult)context.Result;
                _response = new MaskResponseCore(_request);
                _response.Result = responseObject.Value;
                // Modify the originalValue or create a new one.
                context.Result = new ObjectResult(_response)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _request = new MaskRequestCore();
        }
    }
}
