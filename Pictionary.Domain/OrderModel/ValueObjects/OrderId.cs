namespace Pictionary.Domain.OrderModel.ValueObjects;

using Pictionary.Domain.Models;

sealed public class OrderId : ValueObject
{
    private OrderId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static OrderId Parse(string value)
    {
        return new OrderId(Guid.Parse(value));
    }

    public static OrderId Parse(Guid value)
    {
        return new OrderId(value);
    }

    public static OrderId Create()
    {
        return new OrderId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
