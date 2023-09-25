using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(int id);
}