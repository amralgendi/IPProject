using Mediator;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Application.Addresses.Queries.GetAddresses;

public class GetAddressesQueryHandler : IQueryHandler<GetAddressesQuery, IEnumerable<Address>>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressesQueryHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async ValueTask<IEnumerable<Address>> Handle(GetAddressesQuery query, CancellationToken cancellationToken)
    {
        Console.WriteLine(query.UserId);

        return await _addressRepository.GetAllByUserId(UserId.Parse(query.UserId));
    }
}