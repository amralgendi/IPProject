using Pictionary.Domain.OrderModel;

namespace Pictionary.Application.Orders.Interfaces;

public interface IReceiptGenerator
{
    string GenerateReceipt(Order order);
}