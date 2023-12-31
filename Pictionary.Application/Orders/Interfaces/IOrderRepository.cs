namespace Pictionary.Application.Orders.Interfaces;

using Pictionary.Application.Orders.Queries.GetOrders;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public interface IOrderRepository
{
    Task<Order?> GetById(OrderId id);

    Task<IEnumerable<Order>> GetAllByUserId(UserId userId);

    Task<IEnumerable<Order>> GetAll(int size = 10, int page = 0);

    Task Create(Order Order);

    Task Update(Order Order);
}