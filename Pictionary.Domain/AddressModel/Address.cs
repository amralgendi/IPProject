using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.Models;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Domain.AddressModel;

public class Address : AggregateRoot<AddressId>
{
    private Address(
        AddressId id,
        UserId userId,
        string name,
        string firstName,
        string lastName,
        string phoneNumber,
        string street,
        string homeNumber,
        string city,
        string state,
        long version) : base(id, version)
    {
        UserId = userId;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Street = street;
        HomeNumber = homeNumber;
        City = city;
        State = state;
    }

    public UserId UserId { get; }

    public string Name { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Street { get; set; }

    public string HomeNumber { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public static Address Parse(
        AddressId id,
        UserId userId,
        string name,
        string firstName,
        string lastName,
        string phoneNumber,
        string street,
        string homeNumber,
        string city,
        string state,
        long version)
    {
        return new(id, userId, name, firstName, lastName, phoneNumber, street, homeNumber, city, state, version);
    }

    public static Address Create(
        UserId userId,
        string name,
        string firstName,
        string lastName,
        string phoneNumber,
        string street,
        string homeNumber,
        string city,
        string state)
    {
        return new(AddressId.Create(), userId, name, firstName, lastName, phoneNumber, street, homeNumber, city, state, 1);
    }

    #pragma warning disable CS8618
        private Address()
        {
        }
    #pragma warning restore CS8618
}