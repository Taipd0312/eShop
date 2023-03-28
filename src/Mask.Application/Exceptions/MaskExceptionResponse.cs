namespace Mask.Application.Exceptions
{
    public class MaskExceptionResponse
    {
        public MaskExceptionResponse()
        {
        }

        public MaskExceptionResponse(int code, string message, string stackTrace)
        {
            Code = code;
            Message = message;
            StackTrace = stackTrace;
        }

        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? StackTrace { get; set; } = string.Empty;
    }
}
