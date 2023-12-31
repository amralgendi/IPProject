namespace Pictionary.Application.Orders.Commands.OrderPaid;

using Mediator;
using Pictionary.Domain.OrderModel;

public record OrderPaidCommand(string UserId, string SessionId) : ICommand<Order>;