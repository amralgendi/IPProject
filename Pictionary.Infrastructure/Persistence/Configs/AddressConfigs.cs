using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pictionary.Domain.AddressModel;
using Pictionary.Domain.AddressModel.ValueObjects;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Infrastructure.Persistence.Configs;

public class AddressConfigs : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        ConfigureAddressesTable(builder);
    }

    private void ConfigureAddressesTable(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AddressId.Parse(value));

        builder.Property(m => m.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Parse(value));

        // builder.HasOne(m => m.UserId)
        //     .WithOne()
        //     .HasForeignKey(m => m.UserId);

        builder.Property(m => m.FirstName)
            .HasMaxLength(100);

        builder.Property(m => m.LastName)
            .HasMaxLength(100);

        builder.Property(m => m.PhoneNumber)
            .HasMaxLength(100);

        builder.Property(m => m.HomeNumber)
            .HasMaxLength(100);

        builder.Property(m => m.Street)
            .HasMaxLength(100);

        builder.Property(m => m.City)
            .HasMaxLength(100);

        builder.Property(m => m.State)
            .HasMaxLength(100);
    }
}