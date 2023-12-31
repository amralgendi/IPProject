namespace Pictionary.Application.Orders.Commands.OrderPaid;

using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;

public class OrderPaidCommandHandler : ICommandHandler<OrderPaidCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IPaymentService _paymentService;

    public OrderPaidCommandHandler(IOrderRepository orderRepository, IPaymentService paymentService)
    {
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    public async ValueTask<Order> Handle(OrderPaidCommand command, CancellationToken cancellationToken)
    {
        var (orderId, isSuccess) = await _paymentService.GetDataFromCheckoutId(command.SessionId);

        if(!isSuccess)
        {
            throw new Exception("Payment Not Succeeded");
        }

        var order = await _orderRepository.GetById(orderId)
            ?? throw new Exception("Order not Found");

        if(order.UserId.Value.ToString() != command.UserId)
        {
            throw new Exception("Not Authorized");
        }

        order.SetInitiated();

        await _orderRepository.Update(order);

        return order;
    }
}