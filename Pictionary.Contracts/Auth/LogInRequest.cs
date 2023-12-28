namespace Pictionary.Contracts.Auth;

public record LogInRequest(
    string Email,
    string Password);