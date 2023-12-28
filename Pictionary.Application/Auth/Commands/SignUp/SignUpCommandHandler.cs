namespace Pictionary.Application.Auth.Commands.SignUp;

using Mediator;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Domain.UserModel;

public class SignUpCommandHandler : ICommandHandler<SignUpCommand, User>
{
    private readonly IUserRepository _userRepository;

    public SignUpCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<User> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if(!command.Password.Equals(command.ConfirmPassword))
        {
            throw new Exception("Not the same Password");
        }

        if(await _userRepository.GetByEmail(command.Email) is not null)
        {
            throw new Exception("User with Email already Exists");
        }

        var user = new User()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password,
            PhoneNumber = command.PhoneNumber,
        };

        await _userRepository.Create(user);

        return user;
    }
}