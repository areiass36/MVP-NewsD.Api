namespace NewsD.Services;

public interface IFileUpload
{
    Task<string> UploadFile(string fileName, string containerName, IFormFile file);
}