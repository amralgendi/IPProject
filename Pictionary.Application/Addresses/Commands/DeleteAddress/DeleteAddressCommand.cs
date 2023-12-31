using Mediator;

namespace Pictionary.Application.Addresses.Commands.DeleteAddress;

public record DeleteAddressCommand(string UserId, string AddressId) : ICommand;