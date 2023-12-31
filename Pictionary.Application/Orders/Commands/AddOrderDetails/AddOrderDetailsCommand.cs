namespace Pictionary.Application.Orders.Commands.AddOrderDetails;

using Mediator;
using Pictionary.Domain.OrderModel;

public record AddOrderDetailsCommand(
    string UserId,
    string OrderId,
    CreateOrderAddressCommand AddressInfo) : ICommand<Order>;

public record CreateOrderAddressCommand(
    bool IsExistingAddress,
    string? AddressId,
    AddressDetails? AddressDetails);

public record AddressDetails(
    string AddressName,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Street,
    string HomeNumber,
    string City,
    string State);