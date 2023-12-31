using Mediator;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Application.Orders.Queries.GetOrder;

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, Order>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async ValueTask<Order> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(OrderId.Parse(query.OrderId))
            ?? throw new Exception("Not Found!");

        if(order.UserId.Value.ToString() != query.UserId)
        {
            throw new Exception("Not Authorized");
        }

        return order;
    }
}