namespace Pictionary.Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

public class AddressRepository : IAddressRepository
{
    private readonly PictionaryDbContext _dbContext;

    public AddressRepository(PictionaryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Address order)
    {
        await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(AddressId id)
    {
        var address = await _dbContext.FindAsync<Address>(id)
            ?? throw new Exception("Address not Found!");

        _dbContext.Remove(address);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Address>> GetAllByUserId(UserId userId)
    {
        return await _dbContext.Addresses.Where(o => o.UserId == userId).ToListAsync();
    }

    public async Task<Address?> GetById(AddressId id)
    {
        return await _dbContext.FindAsync<Address>(id);
    }

    public async Task Update(Address order)
    {
        await _dbContext.SaveChangesAsync();
    }
}