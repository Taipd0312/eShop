namespace Mask.Application.Helpers
{
    public abstract class MaskReponse<T> where T : class
    {
        public int Code { get; set; }

        public T? Data { get; set; }
    }
}
