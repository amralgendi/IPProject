namespace Pictionary.Application.Orders.Queries.GetOrders;

public record OrderDto(
    string Id,
    decimal TotalPrice,
    string UserId,
    string Status,
    AddressDto? Address,
    IEnumerable<PolaroidDto> Polaroids);

public record PolaroidDto(
    string Id,
    int Quantity,
    decimal Price);

public record AddressDto(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Street,
    string HomeNumber,
    string City,
    string State);