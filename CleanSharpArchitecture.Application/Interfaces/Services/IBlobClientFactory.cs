using Azure.Storage.Blobs;

namespace CleanSharpArchitecture.Application.Interfaces.Services
{
    public interface IBlobClientFactory
    {
        BlobContainerClient GetContainerClient(string containerName);
    }
}
