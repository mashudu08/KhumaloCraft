using KhumaloCraft.Data;
using KhumaloCraft.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCraft.Pages
{
    public class MyWorkModel : PageModel
    {
        private readonly DataManager _dataManager;

        public MyWorkModel(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public List<Product> Products { get; set; }

        public void OnGet()
        {
            Products = _dataManager.GetAllProducts();
        }
    }
}
