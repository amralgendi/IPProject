using Mediator;
using Pictionary.Domain.OrderModel;

namespace Pictionary.Application.Orders.Commands.AdminSetOrderStatus;

public record AdminSetOrderStatusCommand(string OrderId, string Status) : ICommand<Order>;