using Mask.Application.Infrastrucetures;

namespace Mask.api.Infrastructures
{
    public abstract class ResponseCore : RequestCore
    {
        public TimeSpan Duration { get; set; }

        public object? Result { get; set; }

        public MaskExceptionResponse? Validator { get; set; }

        public MaskExceptionResponse? Error { get; set; }

        protected ResponseCore(Guid requestId, DateTime requestTime, Guid userId,  object? value, MaskExceptionResponse? validator, MaskExceptionResponse? error)
            : base(requestId, requestTime, userId)
        {
            Duration = (DateTime.UtcNow - requestTime).Duration();
            Result = value;
            Validator = validator;
            Error = error;
        }
    }
}
