using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.UserModel;

namespace Pictionary.Infrastructure.Persistence;

public class PictionaryDbContext : DbContext
{
    public PictionaryDbContext(DbContextOptions<PictionaryDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<Address> Addresses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(PictionaryDbContext).Assembly);

        modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.IsPrimaryKey())
            .ToList()
            .ForEach(p => p.ValueGenerated = ValueGenerated.Never);

        base.OnModelCreating(modelBuilder);
    }
}