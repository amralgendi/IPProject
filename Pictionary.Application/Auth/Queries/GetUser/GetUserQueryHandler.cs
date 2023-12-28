namespace Pictionary.Application.Auth.Queries.LogIn;

using Mediator;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Auth.Queries.GetUser;
using Pictionary.Domain.UserModel;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(query.Email)
            ?? throw new Exception("User not Found");

        return user;
    }
}