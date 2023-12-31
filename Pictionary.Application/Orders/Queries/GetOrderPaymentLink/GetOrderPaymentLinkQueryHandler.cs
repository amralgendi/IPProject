using Mediator;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Application.Orders.Queries.GetOrderPaymentLink;

public class GetOrderPaymentLinkQueryHandler : IQueryHandler<GetOrderPaymentLinkQuery, string>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IUserRepository _userRepository;

    private readonly IPaymentService _paymentService;

    public GetOrderPaymentLinkQueryHandler(IOrderRepository orderRepository, IPaymentService paymentService, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _paymentService = paymentService;
        _userRepository = userRepository;
    }

    public async ValueTask<string> Handle(GetOrderPaymentLinkQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(OrderId.Parse(query.OrderId))
            ?? throw new Exception("Order not Found");

        var user = await _userRepository.GetById(UserId.Parse(query.UserId))
            ?? throw new Exception("User not Found");

        if(order.UserId.Value.ToString() != query.UserId)
        {
            throw new Exception("Not Authorized!");
        }

        if(order.Status != OrderStatus.PendingPayment)
        {
            throw new Exception("Didn't reach to this step yet!");
        }

        return await _paymentService.GeneratePaymentLink(order, user);
    }
}