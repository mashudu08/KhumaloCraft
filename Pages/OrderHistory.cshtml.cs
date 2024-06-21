// using Microsoft.AspNetCore.Mvc.RazorPages;
// using KhumaloCraft.Data;
// using KhumaloCraft.Model;
// namespace KhumaloCraft.Pages;
// using Microsoft.EntityFrameworkCore;
//
// public class OrderHistory : PageModel
// {
//     public void OnGet()
//     {
//         
//     }
//     public List<Order> Orders { get; set; }
//
//     public async Task OnGetAsync()
//     {
//         int userId = int.Parse(Request.Cookies["UserId"]);
//         DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
//             .UseSqlServer(
//                 "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
//             .Options;
//         DataAccess da = new DataAccess(options);
//         Orders = await da.Orders
//             .Where(o => o.UserId == userId)
//             .Include(o => o.OrderItems)
//             .ThenInclude(oi => oi.Product)
//             .ToListAsync();
//     }
// }