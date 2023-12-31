using Pictionary.Domain.AddressModel;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.Constants;
using Pictionary.Domain.OrderModel.Enums;
using Pictionary.Domain.UserModel;

namespace Pictionary.Mvc.Models;

public class CheckoutViewModel
{
    public CheckoutViewModel()
    {
    }

    public CheckoutViewModel(Order order, IEnumerable<Address> addresses)
    {
        Order = order;
        Addresses = new(addresses);
        AddressRequest = new(addresses.Any(), addresses.FirstOrDefault()?.Id?.Value.ToString(), null, null, null, null, null, null, null, null);
    }

    public Order Order { get; set; } = null!;

    public List<Address> Addresses { get; set; } = null!;

    public CreateOrderAddressRequest AddressRequest { get; set; } = null!;
}

public record CreateOrderAddressRequest(
    bool IsExistingAddress,
    string? AddressName,
    string? AddressId,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    string? Street,
    string? HomeNumber,
    string? City,
    string? State);
