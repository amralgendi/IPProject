using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pictionary.Domain.OrderModel;
using Pictionary.Domain.OrderModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Infrastructure.Persistence.Configs;

public class OrderConfigs : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ConfigureOrdersTable(builder);
    }

    private void ConfigureOrdersTable(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => OrderId.Parse(value));

        builder.Property(m => m.CreatedAt);

        builder.Property(m => m.ExpectedDeliveryAt);

        builder.OwnsMany(m => m.Polaroids, ib =>
        {
            ib.ToTable("Polaroids");

            ib.WithOwner().HasForeignKey("OrderId");

            ib.Property(m => m.Id)
                .HasColumnName("Id")
                .HasConversion(
                    id => id.Value,
                    value => PolaroidId.Parse(value));

            ib.Property(m => m.Price)
                .HasConversion(
                    p => p.ToString(),
                    value => decimal.Parse(value));

            ib.Property(m => m.Quantity);

            ib.Property(m => m.Type)
                .HasConversion(
                    type => type.Value,
                    value => PolaroidType.Parse(value));

            ib.OwnsOne(m => m.Image, ib =>
            {
                ib.Property(m => m.Name);
                ib.Property(m => m.Ext);
                ib.Property(m => m.Data);
            });
        });

        builder.Property(m => m.TotalPrice)
            .HasConversion(
                p => p.ToString(),
                value => decimal.Parse(value));
        builder.Property(m => m.UserId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Parse(value));
        builder.Property(m => m.Status)
            .HasConversion(
                status => status.Value,
                value => OrderStatus.Parse(value));
        builder.OwnsOne(m => m.Address, ib => 
        {
            ib.Property(m => m.FirstName);
            ib.Property(m => m.LastName);
            ib.Property(m => m.PhoneNumber);
            ib.Property(m => m.Street);
            ib.Property(m => m.HomeNumber);
            ib.Property(m => m.City);
            ib.Property(m => m.State);
        });
    }
}