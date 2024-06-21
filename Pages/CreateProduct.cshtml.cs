using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using KhumaloCraft.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages;

public class CreateProduct : PageModel
{
    private readonly IFirebaseStorageService _storageService;
 
    [BindProperty] public string Name { get; set; }
    [BindProperty] public int Price { get; set; }
    [BindProperty] public string Category { get; set; }
    [BindProperty] public string Availability { get; set; }
    [BindProperty] public IFormFile ImageFile { get; set; }
    
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateProduct(IWebHostEnvironment webHostEnvironment, IFirebaseStorageService storageService)
    {
        _webHostEnvironment = webHostEnvironment;
        _storageService = storageService;
    }
    public void OnGet()
    {
        
    }
    public async Task<IActionResult> OnPostAsync()
    {
        // if (!ModelState.IsValid)
        // {
        //     return Page();
        // }

 
        if (ImageFile != null && ImageFile.Length > 0)
        {
            Product product = new Product();
            product.Name = Name;
            product.Category = Category;
            product.Price = Price;
            product.Availability = Availability;
            var photoUri = await _storageService.UploadFile(product.Name, ImageFile);
            product.Image = photoUri.ToString();
            DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
                .UseSqlServer(
                    "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;
            DataAccess da = new DataAccess(options);
            ProductService service = new ProductService(da);
            service.CreateProduct(product);
        }
        return RedirectToPage("/Admin");
    }
}