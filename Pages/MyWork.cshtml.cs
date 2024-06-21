using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages
{
    public class MyWorkModel : PageModel
    {
        [BindProperty] public List<Product> Products { get; set; }
        public List<Product> SearchResults { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        public async Task OnGetAsync()
        {
            DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
                .UseSqlServer(
                    "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;
            DataAccess da = new DataAccess(options);
            ProductService service = new ProductService(da);
            if (!string.IsNullOrEmpty(Query))
            {
                SearchResults = await service.SearchProductsByName(Query);
            }
            Products = await service.GetProducts();
        }
        
        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            // Simulate an asynchronous operation, such as adding an item to a cart in a database
            await Task.Delay(100);

            // Logic to add item to cart
            return RedirectToPage("/Cart");
        }    }
}
