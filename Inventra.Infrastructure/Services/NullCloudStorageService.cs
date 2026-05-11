using Inventra.Application.Interfaces;

namespace Inventra.Infrastructure.Services
{
    public class NullCloudStorageService : ICloudStorageService
    {
        public Task<string?> UploadImageAsync(Stream content, string fileName, string contentType)
            => Task.FromResult<string?>(null);

        public Task DeleteImageAsync(string imageUrl)
            => Task.CompletedTask;
    }
}
