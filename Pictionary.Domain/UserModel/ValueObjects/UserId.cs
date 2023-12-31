namespace Pictionary.Domain.UserModel.ValueObjects;

using Pictionary.Domain.Models;

sealed public class UserId : ValueObject
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static UserId Parse(string value)
    {
        return new UserId(Guid.Parse(value));
    }

    public static UserId Parse(Guid value)
    {
        return new UserId(value);
    }

    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
