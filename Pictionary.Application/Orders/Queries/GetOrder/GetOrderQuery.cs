namespace Pictionary.Application.Orders.Queries.GetOrder;

using Mediator;
using Pictionary.Domain.OrderModel;

public record GetOrderQuery(string UserId, string OrderId) : IQuery<Order>;