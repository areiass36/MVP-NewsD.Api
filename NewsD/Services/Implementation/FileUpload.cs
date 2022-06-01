using Azure.Storage.Blobs;

namespace NewsD.Services;
public class FileUpload : IFileUpload
{
    private readonly IConfiguration _configuration;
    public FileUpload(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> UploadFile(string fileName, string containerName, IFormFile file)
    {
        if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(containerName) || file == null)
            return "";

        try
        {
            var connectionString = _configuration.GetValue<string>("BlobStorage");
            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);

            using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream);

            return blob.Uri.AbsoluteUri;
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
            throw;
        }
    }
}