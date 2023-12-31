namespace Pictionary.Application.Auth.Interfaces;

using Pictionary.Domain.UserModel;
using Pictionary.Domain.UserModel.ValueObjects;

public interface IUserRepository
{
    Task<User?> GetById(UserId id);

    Task<User?> GetByEmail(string email);

    Task<IEnumerable<User>> GetAllMembers();

    Task Create(User user);

    Task Update(User user);
}