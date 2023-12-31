namespace Pictionary.Domain.Models;

public abstract class ValueObject
{
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }

    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            ValueObject valueObject => GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents()),
            // string str => Equals(str),
            _ => false,
        };
    }

    // protected abstract bool Equals(string str);

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }
}
