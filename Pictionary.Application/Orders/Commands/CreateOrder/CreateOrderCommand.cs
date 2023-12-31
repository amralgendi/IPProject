namespace Pictionary.Application.Orders.Commands.CreateOrder;

using Mediator;
using Pictionary.Domain.OrderModel;

public record CreateOrderCommand(
    string UserId,
    IEnumerable<OrderPolaroidCommand> Polaroids) : ICommand<Order>;

public record OrderPolaroidCommand(string Type, int Quantity, CreateImageCommand Image);

public record CreateImageCommand(string Ext, string DataBase64, string FileName);