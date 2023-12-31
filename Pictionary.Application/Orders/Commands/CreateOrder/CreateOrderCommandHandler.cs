namespace Pictionary.Application.Orders.Commands.CreateOrder;

using Mediator;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.Entities;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IAddressRepository _addressRepository;

    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _addressRepository = addressRepository;
        this._mediator = mediator;
    }

    public async ValueTask<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(
            command.Polaroids.Select(p => Polaroid.Create(PolaroidType.Parse(p.Type), Image.Parse(p.Image.FileName, p.Image.Ext, p.Image.DataBase64), p.Quantity)),
            UserId.Parse(command.UserId));

        await _orderRepository.Create(order);

        return order;
    }

    // public static Address  ToOrderAddress(Domain.AddressModel.Address address)
    // {
    //     return Address.Parse(
    //         address.FirstName, 
    //         address.LastName, 
    //         address.PhoneNumber, 
    //         address.Street, 
    //         address.HomeNumber, 
    //         address.City,
    //         address.State);
    // }
}