using Azure.Storage.Blobs;
using CleanSharpArchitecture.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace CleanSharpArchitecture.Infrastructure.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobClientFactory _blobClientFactory;

        public BlobService(IBlobClientFactory blobClientFactory)
        {
            _blobClientFactory = blobClientFactory;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File must not be null or empty.", nameof(file));

            var blobContainerClient = _blobClientFactory.GetContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(Guid.NewGuid() + Path.GetExtension(file.FileName));

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }
    }
}
