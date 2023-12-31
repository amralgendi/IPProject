namespace Pictionary.Application.Orders.Commands.AddOrderDetails;

using Mediator;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.Entities;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public class AddOrderDetailsCommandHandler : ICommandHandler<AddOrderDetailsCommand, Order>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IAddressRepository _addressRepository;

    private readonly IMediator _mediator;

    public AddOrderDetailsCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _addressRepository = addressRepository;
        this._mediator = mediator;
    }

    public async ValueTask<Order> Handle(AddOrderDetailsCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetById(OrderId.Parse(command.OrderId))
            ?? throw new Exception("Order not Found!");

        var address = command.AddressInfo.IsExistingAddress
            ? await _addressRepository.GetById(AddressId.Parse(command.AddressInfo.AddressId ?? string.Empty))
                ?? throw new Exception("Address not Found!")
            : Domain.AddressModel.Address.Create(
                UserId.Parse(command.UserId),
                command.AddressInfo.AddressDetails!.AddressName,
                command.AddressInfo.AddressDetails.FirstName,
                command.AddressInfo.AddressDetails.LastName,
                command.AddressInfo.AddressDetails.PhoneNumber,
                command.AddressInfo.AddressDetails.Street,
                command.AddressInfo.AddressDetails.HomeNumber,
                command.AddressInfo.AddressDetails.City,
                command.AddressInfo.AddressDetails.State);

        if(!command.AddressInfo.IsExistingAddress)
        {
            await _addressRepository.Create(address);
        }

        order.SetAddress(Address.Parse(
            address.FirstName,
            address.LastName,
            address.PhoneNumber,
            address.Street,
            address.HomeNumber,
            address.City,
            address.State));

        order.SetPendingPayment();

        await _orderRepository.Update(order);

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