namespace Pictionary.Domain.AddressModel.ValueObjects;

using Pictionary.Domain.Models;

sealed public class AddressId : ValueObject
{
    private AddressId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AddressId Parse(string value)
    {
        return new AddressId(Guid.Parse(value));
    }

    public static AddressId Parse(Guid value)
    {
        return new AddressId(value);
    }

    public static AddressId Create()
    {
        return new AddressId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
