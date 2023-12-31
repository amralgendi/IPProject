namespace Pictionary.Contracts.Orders;

public record CreateOrderRequest();

public record CreateAddressRequest(
    string FirstName,
    string LastName,
    string Line1,
    string? Line2,
    string City,
    string State,
    string? Floor,
    string? Apt);