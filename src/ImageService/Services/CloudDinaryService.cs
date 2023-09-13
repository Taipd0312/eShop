using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ImageService.Configurations;
using Microsoft.Extensions.Options;

namespace ImageService.Services
{
    public class CloudDinaryService : Interfaces.ImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudDinaryService(IOptions<AccountSetting> accountSettingOPtion)
        {
            this._cloudinary = new Cloudinary(new Account(
                accountSettingOPtion.Value.CloundName,
                accountSettingOPtion.Value.APIKey,
                accountSettingOPtion.Value.APISecret));
        }

        public override Task<byte[]> GetImageAsync(string url)
        {
            return base.GetImageAsync(url);
        }

        public override async Task<string> UploadImageAsync(MemoryStream file)
        {
            ImageUploadResult uploadResult = new ImageUploadResult();
            using (MemoryStream stream = file)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(DateTime.UtcNow.ToString("yyyyyMMddhhmmss"), stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult.Url.ToString();
        }
    }

    public class CloudinaryResult
    {
        public int Id { get; set; } = default(int);

        public string ResultAsJson { get; set; } = string.Empty;
    }
}
