using Mediator;
using Pictionary.Domain.OrderModel;

namespace Pictionary.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(string UserId) : IQuery<IEnumerable<Order>>;