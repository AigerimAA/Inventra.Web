using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Inventra.Infrastructure.Services
{
    public class CloudStorageServices
    {
        private readonly Cloudinary _cloudinary;

        public CloudStorageServices(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            if (file.Length == 0)
                return null;

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "inventra"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl?.ToString();
        }
        public async Task DeleteImageAsync(string imageUrl)
        {
            var publicId = ExtractPublicId(imageUrl);
            if (string.IsNullOrEmpty(publicId))
                return;

            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }
        private static string ExtractPublicId(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var segments = uri.Segments;
            var uploadIndex = Array.IndexOf(segments, "upload/");
            if (uploadIndex < 0)
                return string.Empty;

            var publicIdWithExtension = string.Concat(segments[(uploadIndex + 1)..]);
            return Path.GetFileNameWithoutExtension(publicIdWithExtension);
        }
    }
}
