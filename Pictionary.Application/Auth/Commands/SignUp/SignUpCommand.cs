namespace Pictionary.Application.Auth.Commands.SignUp;

using Mediator;
using Pictionary.Domain.UserModel;

public record SignUpCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword) : ICommand<User>;