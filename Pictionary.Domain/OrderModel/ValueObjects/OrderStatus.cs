namespace Pictionary.Domain.OrderModel.ValueObjects;

using System.Collections.Generic;
using Pictionary.Domain.Models;
using Pictionary.Domain.OrderModel.Enums;

public sealed class OrderStatus : ValueObject
{
    private OrderStatus(OrderStatusEnum type)
    {
        Value = type.ToString();
        EnumValue = type;
    }

    public string Value { get; }

    public OrderStatusEnum EnumValue { get; }

    public static OrderStatus PendingDetails => Parse(OrderStatusEnum.PENDING_DETAILS);

    public static OrderStatus PendingPayment => Parse(OrderStatusEnum.PENDING_PAYMENT);

    public static OrderStatus Initiated => Parse(OrderStatusEnum.INITIATED);

    public static OrderStatus Delivered => Parse(OrderStatusEnum.DELIVERED);

    public static OrderStatus Parse(string type)
    {
        if (!Enum.TryParse(type, out OrderStatusEnum enumType))
        {
            throw new Exception("Can't Parse");
        }

        return new(enumType);
    }

    public static OrderStatus Parse(OrderStatusEnum type)
    {
        return new(type);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}