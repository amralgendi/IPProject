namespace Pictionary.Application.Addresses.Commands.CreateAddress;

using Mediator;
using Pictionary.Domain.AddressModel;

public record CreateAddressCommand(
    string UserId,
    string Street,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string HomeNumber,
    string State,
    string City) : ICommand<Address>;
