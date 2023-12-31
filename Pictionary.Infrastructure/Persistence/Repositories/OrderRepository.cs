namespace Pictionary.Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Application.Orders.Queries.GetOrders;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public class OrderRepository : IOrderRepository
{
    private readonly PictionaryDbContext _dbContext;

    public OrderRepository(PictionaryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Order order)
    {
        await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetAll(int size = 10, int skip = 0)
    {
        return await _dbContext.Orders.Skip(skip).Take(size).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByUserId(UserId userId)
    {
        return await _dbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
    }

    public async Task<Order?> GetById(OrderId id)
    {
        return await _dbContext.FindAsync<Order>(id);
    }

    public async Task Update(Order order)
    {
        await _dbContext.SaveChangesAsync();
    }

    private static OrderDto ToOrderDto(Order o)
    {
        var address = o.Address is null
            ? null
            : new AddressDto(
                o.Address.FirstName,
                o.Address.LastName,
                o.Address.PhoneNumber,
                o.Address.Street,
                o.Address.HomeNumber,
                o.Address.City,
                o.Address.State);

        return new OrderDto(
            o.Id.Value.ToString(),
            o.TotalPrice,
            o.UserId.Value.ToString(),
            o.Status.Value,
            address,
            o.Polaroids.Select(p => new PolaroidDto(p.Id.Value.ToString(), p.Quantity, p.Price)));
    }
}