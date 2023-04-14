namespace Mask.Application.Infrastrucetures
{
    public class MaskApplicationResponse<TSourceOut>
    {
        public int Code { get; set; } = 200;

        public MaskActionResponse Action { get; set; } = MaskActionResponse.Response;

        public string Message { get; set; } = string.Empty;

        public TSourceOut? Data { get; set; }

        public MaskApplicationResponse(string message, TSourceOut data)
        {
            Message = message;
            Data = data;
        }

        public MaskApplicationResponse(TSourceOut data)
        {
            Data = data;
        }

        public MaskApplicationResponse()
        {
        }
    }
}
