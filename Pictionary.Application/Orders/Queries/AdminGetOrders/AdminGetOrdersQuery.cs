using Mediator;
using Pictionary.Domain.OrderModel;

namespace Pictionary.Application.Orders.Queries.AdminGetOrders;

public record AdminGetOrdersQuery(int Size = 10, int Skip = 0) : IQuery<IEnumerable<Order>>;