using FastighetProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly FastighetProjektDbContext _context;
    public UserRepository(FastighetProjektDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Models.Models.User.User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task<Models.Models.User.User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<Models.Models.User.User> CreateUserAsync(Models.Models.User.User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<Models.Models.User.User> UpdateUserAsync(Models.Models.User.User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}