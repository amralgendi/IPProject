namespace Pictionary.Domain.OrderModel.ValueObjects;

using Pictionary.Domain.Models;

sealed public class PolaroidId : ValueObject
{
    private PolaroidId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PolaroidId Parse(string value)
    {
        return new PolaroidId(Guid.Parse(value));
    }

    public static PolaroidId Create()
    {
        return new PolaroidId(Guid.NewGuid());
    }

    public static PolaroidId Parse(Guid value)
    {
        return new PolaroidId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
