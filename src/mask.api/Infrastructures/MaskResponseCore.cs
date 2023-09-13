namespace Mask.api.Infrastructures
{
    public class MaskResponseCore : ResponseCore
    {
        public MaskResponseCore(Guid requestId, DateTime requestTime, Guid userId, object? value) : base(requestId, requestTime, userId, value, null, null)
        {
        }

        public MaskResponseCore(RequestCore core) : base(core.RequestId, DateTime.UtcNow, core.UserId, null, null, null)
        {
        }
    }
}
