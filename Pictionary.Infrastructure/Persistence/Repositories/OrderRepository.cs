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

    public async Task<IEnumerable<Order>> GetAll(int size, int page = 1)
    {
        return await _dbContext.Orders.Skip((page - 1) * size).Take(size).ToListAsync();
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
}