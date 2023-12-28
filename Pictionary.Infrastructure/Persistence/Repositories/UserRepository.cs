namespace Pictionary.Infrastructure.Persistence.Repositories;

using Pictionary.Application.Auth.Interfaces;
using Pictionary.Domain.UserModel;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users;

    public UserRepository()
    {
        _users = new();
    }

    public Task Create(User user)
    {
        if(_users.Any(u => user.Email == u.Email))
        {
            throw new Exception("User Exists");
        }

        _users.Add(user);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<User>> GetAllMembers()
    {
        await Task.CompletedTask;
        return _users.FindAll(u => u.Role == "Member");
    }

    public async Task<User?> GetByEmail(string email)
    {
        await Task.CompletedTask;
        return _users.Find(u => u.Email == email)
            ?? null;
    }

    public Task Update(User user)
    {
        var index = _users.FindIndex(u => user.Email == u.Email);

        if(index < 0)
        {
            throw new Exception("User not Found");
        }

        _users[index] = user;

        return Task.CompletedTask;    
    }
}