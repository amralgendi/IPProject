namespace Pictionary.Application.Addresses.Commands.CreateAddress;

using Mediator;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.UserModel.ValueObjects;

public class CreateAddressCommandHandler : ICommandHandler<CreateAddressCommand, Address>
{
    private readonly IAddressRepository _addressRepository;

    public CreateAddressCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async ValueTask<Address> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
    {
        var address = Address.Create(
            UserId.Parse(command.UserId), "Dummy Name", command.FirstName, command.LastName, command.PhoneNumber,
            command.Street, command.HomeNumber, command.City, command.State);

        await _addressRepository.Create(address);

        return address;
    }
}