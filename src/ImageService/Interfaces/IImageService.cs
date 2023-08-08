namespace ImageService.Interfaces
{
    public interface IImageService
    {
        public Task<byte[]> GetImageAsync(string url);

        public Task<string> UploadImageAsync(MemoryStream file);
    }

    public abstract class ImageService : IImageService
    {
        public virtual Task<byte[]> GetImageAsync(string url)
        {
            throw new NotImplementedException();
        }

        public virtual Task<string> UploadImageAsync(MemoryStream file)
        {
            throw new NotImplementedException();
        }
    }
}
