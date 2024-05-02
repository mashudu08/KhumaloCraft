using KhumaloCraft.Data;
using KhumaloCraft.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCraft.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
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
            DataManager dm = new DataManager();
            dm.openConnection();
            User user = dm.LoginUser(Email, Password);


            // Redirect the user to another page
            return RedirectToPage("/Index");
        }
    }
}
