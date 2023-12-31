namespace Pictionary.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Domain.UserModel;
using Pictionary.Domain.UserModel.ValueObjects;

public class UserRepository : IUserRepository
{
    private readonly PictionaryDbContext _dbContext;

    public UserRepository(PictionaryDbContext dbContext)
    {
        _dbContext = dbContext;
        Console.WriteLine("Inide User Repository");
        var user = User.CreateAdmin("Salah", "Hamada", "01122334456", "me@mohasalah.com", "checkcheck");
        _dbContext.Add(user);

        _dbContext.SaveChanges();
    }

    public async Task Create(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllMembers()
    {
        return await _dbContext.Users.Where(u => u.Role == "Member").ToListAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

    }

    public async Task<User?> GetById(UserId id)
    {
        return await _dbContext.FindAsync<User>(id);
    }

    public async Task Update(User user)
    {
        await _dbContext.SaveChangesAsync();
    }
}