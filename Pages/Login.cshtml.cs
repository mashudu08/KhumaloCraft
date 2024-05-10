using KhumaloCraft.Data;
using KhumaloCraft.Model;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Email { get; set; }

        [BindProperty] public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the same page with validation errors
                return Page();
            }

            // Process the form data (e.g., authentication)
            // DO Login call here
            // DataManager dm = new DataManager();
            // dm.openConnection();
            // User user = dm.LoginUser(Email, Password);
            DbContextOptions<DataAccess> options = new DbContextOptionsBuilder<DataAccess>()
                .UseSqlServer(
                    "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;
            DataAccess da = new DataAccess(options);
            LoginService loginService = new LoginService(da);
            User user = loginService.Login(Email, Password);

            // Store the user role in storage
            if (user != null)
            {
                var role = user.Role;
                HttpContext.Session.SetString("role", role.ToString());
                HttpContext.Session.SetInt32("isLoggedIn", 1);
                Response.Cookies.Append("IsLoggedIn", "true");
                // Redirect the user to another page
                return RedirectToPage("/Index");
            }

            return RedirectToPage("/Index");
        }
    }
}