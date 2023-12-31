namespace Pictionary.Application.Addresses.Commands.DeleteAddress;

using Mediator;
using Pictionary.Application.Addresses.Commands.DeleteAddress;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public class DeleteAddressCommandHandler : ICommandHandler<DeleteAddressCommand>
{
    private readonly IAddressRepository _addressRepository;

    public DeleteAddressCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async ValueTask<Unit> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetById(AddressId.Parse(command.AddressId))
            ?? throw new Exception("Address not Found");

        if(address.UserId.Value.ToString() != command.UserId)
        {
            throw new Exception("Not Authorized Exception");
        }

        await _addressRepository.Delete(address.Id);

        return Unit.Value;
    }
}