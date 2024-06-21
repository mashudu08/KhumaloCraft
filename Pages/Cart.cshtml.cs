using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages
{
    public class CartListModel : PageModel
    {
        public List<Cart> CartItems { get; set; }

        public int CartCount { get; set; }
        public async Task OnGetAsync()
        {
        CartCount = Request.Cookies["cartCount"] != null ? int.Parse(Request.Cookies["cartCount"]) : 0;
            int userId = int.Parse(Request.Cookies["UserId"]);
            DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
                .UseSqlServer(
                    "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;
            DataAccess da = new DataAccess(options);
            CartService service = new CartService(da);
            CartItems = await service.GetCart(userId);
        }

        public async Task<IActionResult> OnPostClearCart()
        {
            int userId = int.Parse(Request.Cookies["UserId"]);
            DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
                .UseSqlServer(
                    "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;
            DataAccess da = new DataAccess(options);
            CartService cartService = new CartService(da);
            await cartService.ClearCart(userId);
            
            // Retrieve cart items
            var cartItems = await cartService.GetCart(userId);
            // Create order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Product.Availability, // Adjust as needed
                    Price = ci.Product.Price
                }).ToList()
            };
            int cartCount =  0;
            Response.Cookies.Append("cartCount", cartCount.ToString());
            return RedirectToPage("/Index");
        }
    }
}