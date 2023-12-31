namespace Pictionary.Domain.OrderModel.Enums;

public enum OrderStatusEnum
{
    PENDING_DETAILS,
    PENDING_PAYMENT,
    INITIATED,
    DELIVERING,
    DELIVERED,
    REFUND_REQUEST,
    REFUNDED,
}