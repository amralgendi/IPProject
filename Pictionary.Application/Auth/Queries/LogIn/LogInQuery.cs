namespace Pictionary.Application.Auth.Queries.LogIn;

using Mediator;
using Pictionary.Domain.UserModel;

public record LogInQuery(
    string Email,
    string Password) : IQuery<User>;