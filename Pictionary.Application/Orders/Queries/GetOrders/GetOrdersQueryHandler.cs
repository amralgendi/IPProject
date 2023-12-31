using Mediator;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async ValueTask<IEnumerable<Order>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetAllByUserId(UserId.Parse(query.UserId));
    }
}