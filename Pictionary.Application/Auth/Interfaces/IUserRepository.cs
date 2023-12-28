namespace Pictionary.Application.Auth.Interfaces;

using Pictionary.Domain.UserModel;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email);

    Task<IEnumerable<User>> GetAllMembers();

    Task Create(User user);

    Task Update(User user);
}