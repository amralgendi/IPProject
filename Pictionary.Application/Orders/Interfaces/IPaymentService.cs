using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Application.Orders.Interfaces;

public interface IPaymentService
{
    Task<string> GeneratePaymentLink(Order order, User user);

    Task<(OrderId OrderId, bool IsSuccess)> GetDataFromCheckoutId(string checkoutId);
}