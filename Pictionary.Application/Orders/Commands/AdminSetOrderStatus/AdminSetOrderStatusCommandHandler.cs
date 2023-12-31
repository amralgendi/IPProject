namespace Pictionary.Application.Orders.Commands.AdminSetOrderStatus;

using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;

public class AdminSetOrderStatusCommandHandler : ICommandHandler<AdminSetOrderStatusCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    public AdminSetOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async ValueTask<Order> Handle(AdminSetOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(OrderId.Parse(command.OrderId))
            ?? throw new Exception("Order not Found");

        var newStatus = OrderStatus.Parse(command.Status);

        order.AdminSetStatus(newStatus);

        await _orderRepository.Update(order);
        
        return order;
    }
}