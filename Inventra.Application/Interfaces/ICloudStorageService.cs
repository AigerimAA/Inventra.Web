using Microsoft.AspNetCore.Http;

namespace Inventra.Application.Interfaces
{
    public interface ICloudStorageService
    {
        Task<string?> UploadImageAsync(Stream content, string fileName, string contentType);
        Task DeleteImageAsync(string imageUrl);
    }
}
