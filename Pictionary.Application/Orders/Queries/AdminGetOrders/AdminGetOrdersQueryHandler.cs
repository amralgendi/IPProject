using Mediator;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel;

namespace Pictionary.Application.Orders.Queries.AdminGetOrders;

public class AdminGetOrdersQueryHandler : IQueryHandler<AdminGetOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public AdminGetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async ValueTask<IEnumerable<Order>> Handle(AdminGetOrdersQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetAll(query.Size, query.Page);
    }
}