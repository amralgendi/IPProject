namespace Pictionary.Application.Auth.Queries.GetUser;

using Mediator;
using Pictionary.Domain.UserModel;

public record GetUserQuery(string Id) : IQuery<User>;