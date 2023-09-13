using FluentValidation;
using FluentValidation.Results;

namespace Mask.Application.Infrastrucetures
{
    public class MaskValidatorResponse : ValidationException
    {
        public MaskValidatorResponse(string message) : base(message)
        {
        }

        public MaskValidatorResponse(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }

        public MaskValidatorResponse(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
        {
        }

        public int Code { get; set; }

        public MaskActionResponse Action { get; set; }
    }
}
