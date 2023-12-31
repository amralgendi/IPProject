using Pictionary.Domain.Models;

namespace Pictionary.Domain.OrderModel.ValueObjects;

public sealed class Address : ValueObject
{
    private Address(string firstName, string lastName, string phoneNumber, string street, string homeNumber, string city, string state)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Street = street;
        HomeNumber = homeNumber;
        City = city;
        State = state;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string PhoneNumber { get; }

    public string Street { get; }

    public string HomeNumber { get; }

    public string City { get; }

    public string State { get; }

    public static Address Parse(string firstName, string lastName, string phoneNumber, string street, string homeNumber, string city, string state)
    {
        return new(firstName, lastName, phoneNumber, street, homeNumber, city, state);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return PhoneNumber;
        yield return Street;
        yield return HomeNumber;
        yield return City;
        yield return State;
    }
}