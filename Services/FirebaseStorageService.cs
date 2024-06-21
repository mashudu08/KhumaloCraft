using Firebase.Storage;
using Google.Cloud.Storage.V1;
using KhumaloCraft.Services.interfaces;

namespace KhumaloCraft.Data;
public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly StorageClient _storageClient;
    private const string BucketName = "khumalocraft-4e50e.appspot.com";
    public FirebaseStorageService(StorageClient storageClient)
    {
        _storageClient = storageClient;
    }
    public async Task<Uri> UploadFile(string name, IFormFile file)
    {
        var randomGuid = Guid.NewGuid();
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var blob = await _storageClient.UploadObjectAsync(BucketName, 
            $"{name}-{randomGuid}", file.ContentType, stream);
        var photoUri = new Uri(blob.MediaLink);
        return photoUri;
    }
}