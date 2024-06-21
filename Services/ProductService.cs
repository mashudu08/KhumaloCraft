using KhumaloCraft.Data;
using KhumaloCraft.Model;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Services;

public class ProductService
{
    // using dependecy inject to prevent db connection leaks
    private readonly DataAccess _context;

    public ProductService(DataAccess? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
    
    public Product CreateProduct(Product product)
    {
        var res = _context.Products.Add(product);
        _context.SaveChanges();
        return res.Entity;
    }
    
    public async Task<List<Product>> SearchProductsByName(string name)
    {
        return await _context.Products
            .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
            .ToListAsync();
    }
}