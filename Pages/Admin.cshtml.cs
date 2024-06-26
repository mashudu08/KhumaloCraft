using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages;

public class Admin : PageModel
{
    public bool IsArtisan { get; set; }
    [BindProperty] public List<Product> Products { get; set; }
    public async Task OnGetAsync()
    {
        DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
            .UseSqlServer(
                "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            .Options;
        DataAccess da = new DataAccess(options);
        ProductService service = new ProductService(da);
        Products = await service.GetProducts();
        IsArtisan = HttpContext.Session.GetString("role") == "Artisan";
    }
}