using KhumaloCraft.Data;
using KhumaloCraft.Model;

namespace KhumaloCraft.Services;

public class ProductService
{
    // using dependecy inject to prevent db connection leaks
    private readonly DataAccess _context;

    public ProductService(DataAccess? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));;
    }

    public List<Product> GetProducts()
    {
        return _context.Products.ToList();
    }
    
    public Product CreateProduct(Product product)
    {
        var res = _context.Products.Add(product);
        _context.SaveChanges();
        return res.Entity;
    }
}