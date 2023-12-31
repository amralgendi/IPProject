using Mediator;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.OrderModel.ValueObjects;

namespace Pictionary.Application.Orders.Queries.GenerateReceipt;

public record GenerateReceiptQueryHandler : IQueryHandler<GenerateReceiptQuery, string>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IReceiptGenerator receiptGenerator;

    public GenerateReceiptQueryHandler(IOrderRepository orderRepository, IReceiptGenerator receiptGenerator)
    {
        _orderRepository = orderRepository;
        this.receiptGenerator = receiptGenerator;
    }

    public async ValueTask<string> Handle(GenerateReceiptQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(OrderId.Parse(query.OrderId))
            ?? throw new Exception("Order not Found");

        if(query.UserRole != "Admin" && order.UserId.Value.ToString() != query.UserId)
        {
            throw new Exception("Not Authorized!");
        }

        return receiptGenerator.GenerateReceipt(order);
    }
}