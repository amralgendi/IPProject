namespace Pictionary.Application.Auth.Queries.LogIn;

using Mediator;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Domain.UserModel;

public class LogInQueryHandler : IQueryHandler<LogInQuery, User>
{
    private readonly IUserRepository _userRepository;

    public LogInQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<User> Handle(LogInQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(query.Email)
            ?? throw new Exception("Invalid Email or Password");
        
        if(!user.Password.Equals(query.Password))
        {
            throw new Exception("Invalid Email or Password");
        }

        return user;
    }
}