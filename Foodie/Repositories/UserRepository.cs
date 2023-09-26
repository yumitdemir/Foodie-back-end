using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<User> Create(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
        return user;
    }
}