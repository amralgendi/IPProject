namespace Pictionary.Application.Addresses.Interfaces;

using Pictionary.Domain.AddressModel;
using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public interface IAddressRepository
{
    Task<Address?> GetById(AddressId id);

    Task<IEnumerable<Address>> GetAllByUserId(UserId userId);

    Task Create(Address Address);

    Task Update(Address Address);

    Task Delete(AddressId id);
}