using Mediator;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.OrderModel;

namespace Pictionary.Application.Addresses.Queries.GetAddresses;

public record GetAddressesQuery(string UserId) : IQuery<IEnumerable<Address>>;