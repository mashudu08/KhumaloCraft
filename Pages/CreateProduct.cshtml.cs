using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages;

public class CreateProduct : PageModel
{
    [BindProperty] public Product Product { get; set; }
    
    [BindProperty]
    public IFormFile ImageFile { get; set; }
    
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateProduct(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public void OnGet()
    {
        
    }
    public IActionResult OnPost(Product product)
    {
        // if (!ModelState.IsValid)
        // {
        //     return Page();
        // }

        if (ImageFile != null && ImageFile.Length > 0)
        {
            // Initialize Firebase Admin SDK
            GoogleCredential credential = GoogleCredential.FromFile("path/to/firebase-admin-sdk.json");
            var storage = StorageClient.Create(credential);

            // Get the unique file name
            var uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(ImageFile.FileName);

            // Upload the file to Firebase Storage
            using (var memoryStream = new MemoryStream())
            {
                ImageFile.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                storage.UploadObject("gs://khumalocraft-4e50e.appspot.com", "uploads/" + uniqueFileName, null, memoryStream);
            }

            // Set the image path in the Product object
            Product.Image = "https://storage.googleapis.com/gs://khumalocraft-4e50e.appspot.com/uploads/" + uniqueFileName;
            DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
                .UseSqlServer(
                    "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;
            DataAccess da = new DataAccess(options);
            ProductService service = new ProductService(da);
            service.CreateProduct(Product);
        }
        return RedirectToPage("/Admin");
    }
}