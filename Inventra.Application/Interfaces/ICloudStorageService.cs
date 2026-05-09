using Microsoft.AspNetCore.Http;

namespace Inventra.Application.Interfaces
{
    public interface ICloudStorageService
    {
        Task<string?> UploadImageAsync(IFormFile file);
        Task DeleteImageAsync(string imageUrl);
    }
}
