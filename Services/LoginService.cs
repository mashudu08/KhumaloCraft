using KhumaloCraft.Data;
using KhumaloCraft.Model;

namespace KhumaloCraft.Services;

public class LoginService
{
    // using dependecy inject to prevent db connection leaks
    private readonly DataAccess _context;

    public LoginService(DataAccess? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));;
    }

    public User Login(string email, string password)
    {
        return _context.Users?.FirstOrDefault(user => user.Email == email && user.Password == password)!;
    }
}