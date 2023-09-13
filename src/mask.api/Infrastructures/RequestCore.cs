namespace Mask.api.Infrastructures
{
    public abstract class RequestCore
    {
        public Guid RequestId { get; set; } = Guid.NewGuid();

        public DateTime RequestTime { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; } = Guid.Empty;

        protected RequestCore(Guid requestId, DateTime requestTime, Guid userId)
        {
            RequestId = requestId;
            RequestTime = requestTime;
            UserId = userId;
        }

        protected RequestCore(Guid userId)
        {
            UserId = userId;
        }

        protected RequestCore()
        {
        }
    }
}
