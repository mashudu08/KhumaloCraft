using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages;

public class ViewItem : PageModel
{
    public Product Product { get; set; }

    private List<Product> _products;


    public async Task OnGetAsync(int id)
    {
        DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
            .UseSqlServer(
                "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            .Options;
        DataAccess da = new DataAccess(options);
        ProductService service = new ProductService(da);
        _products = await service.GetProducts();
        Product = _products.FirstOrDefault(p => p.ProductId == id);
        if (Product == null)
        {
            // Handle the case where the product is not found
            RedirectToPage("/Error");
        }
    }

    public async Task<IActionResult> OnPostAddToCart(int productId)
    {
        DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
            .UseSqlServer(
                "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
            .Options;
        DataAccess da = new DataAccess(options);
        ProductService productService = new ProductService(da);
        CartService service = new CartService(da);
        _products = await productService.GetProducts();
        Product = _products.FirstOrDefault(p => p.ProductId == productId);
        // Logic to add item to cart
        // TODO: DISABLE ADD TO CART BUTTON IF USER IS NOT LOGGED IN OR ELSE THIS WILL BREAK
        int cartCount = Request.Cookies["cartCount"] != null ? int.Parse(Request.Cookies["cartCount"]) : 0;
        int userId = int.Parse(Request.Cookies["UserId"]);
        cartCount++;
        Response.Cookies.Append("cartCount", cartCount.ToString());
        Cart cart = new Cart();
        cart.ProductId = Product.ProductId;
        cart.Product = Product;
        cart.UserId = userId;
 
        await service.CreateCart(cart);
        return RedirectToPage("/Index");
    }
}