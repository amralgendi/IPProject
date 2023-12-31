namespace Pictionary.Domain.OrderModel;

using Pictionary.Domain.Models;
using Pictionary.Domain.OrderModel.Entities;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel;
using Pictionary.Domain.UserModel.ValueObjects;

public sealed class Order : AggregateRoot<OrderId>
{
    public List<Polaroid> Polaroids { get; private set;  }

    public decimal TotalPrice { get; private set; }

    public Address? Address { get; private set; }

    public UserId UserId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ExpectedDeliveryAt { get; set; }

    private Order(
        OrderId id,
        IEnumerable<Polaroid> polaroids,
        Address? address,
        UserId userId,
        OrderStatus status,
        DateTime createdAt,
        DateTime? expectedDeliveryAt,
        long version) : base(id, version)
    {
        Polaroids = new(polaroids);
        Address = address;
        UserId = userId;
        Status = status;
        CreatedAt = createdAt;
        ExpectedDeliveryAt = expectedDeliveryAt;
        TotalPrice = Polaroids.Sum(p => p.Price);
    }

    public void SetPendingPayment()
    {
        if(Status != OrderStatus.PendingDetails)
        {
            throw new Exception("Order details already set!");
        }

        if(Address is null)
        {
            throw new Exception("Order Address not set yet!");
        }

        Status = OrderStatus.PendingPayment;
    }

    public void SetInitiated()
    {
        if(Status != OrderStatus.PendingPayment)
        {
            throw new Exception("Order already paid for!");
        }

        Status = OrderStatus.Initiated;
        ExpectedDeliveryAt = DateTime.Now.AddDays(7);
    }

    public void AdminSetStatus(OrderStatus status)
    {
        if(Status == OrderStatus.PendingPayment || Status == OrderStatus.PendingDetails)
        {
            throw new Exception("Admin not eligible to set status");
        }

        Status = status;
    }

    public void SetAddress(Address address)
    {
        if(Address is not null)
        {
            throw new Exception("Address it already set");
        }

        Address = address;
    }

    public static Order Parse(
        OrderId id,
        IEnumerable<Polaroid> polaroids,
        Address address,
        UserId userId,
        OrderStatus status,
        DateTime createdAt,
        DateTime? expectedDeliveryAt,
        long version)
    {
        return new(id, polaroids, address, userId, status, createdAt, expectedDeliveryAt, version);
    }

    public static Order Create(
        IEnumerable<Polaroid> polaroids,
        UserId userId)
    {
        return new(OrderId.Create(), polaroids, null, userId, OrderStatus.PendingDetails, DateTime.Now, null, 1);
    }

    #pragma warning disable CS8618
        private Order()
        {
        }
    #pragma warning restore CS8618
}