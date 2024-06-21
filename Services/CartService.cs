using KhumaloCraft.Data;
using KhumaloCraft.Model;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Services;

public class CartService
{
    private readonly DataAccess _context;

    public CartService(DataAccess? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));;
    }
    
    public async Task<List<Cart>> GetCart(int userId)
    {
        var c = await _context.Carts.Include(c => c.Product).ToListAsync();
        return c.FindAll((c) => c.UserId == userId);
    }

    public async Task<Cart> CreateCart(Cart cart)
    {
        var res = await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
        return res.Entity;
    }
    
    public async Task ClearCart(int userId)
    {
        var cartItemsToDelete = await _context.Carts
            .Where(c => c.UserId == userId)
            .ToListAsync();

        if (cartItemsToDelete.Any())
        {
            _context.Carts.RemoveRange(cartItemsToDelete);
            await _context.SaveChangesAsync();
        }
    }
}