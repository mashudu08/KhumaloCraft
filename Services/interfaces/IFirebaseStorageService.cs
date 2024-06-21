using Google.Cloud.Storage.V1;

namespace KhumaloCraft.Services.interfaces;

public interface IFirebaseStorageService
{
    public Task<Uri> UploadFile(string name, IFormFile file);
}