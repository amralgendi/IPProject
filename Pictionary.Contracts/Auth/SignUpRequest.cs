namespace Pictionary.Contracts.Auth;

public record SignUpRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword);