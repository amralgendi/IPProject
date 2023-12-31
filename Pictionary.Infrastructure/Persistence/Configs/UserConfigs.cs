using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pictionary.Domain.UserModel;
using Pictionary.Domain.UserModel.ValueObjects;

namespace Pictionary.Infrastructure.Persistence.Configs;

public class UserConfigs : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Parse(value));

        builder.Property(m => m.FirstName)
            .HasMaxLength(100);

        builder.Property(m => m.LastName)
            .HasMaxLength(100);

        builder.Property(m => m.PhoneNumber)
            .HasMaxLength(100);

        builder.Property(m => m.Email)
            .HasMaxLength(100);

        builder.Property(m => m.Password)
            .HasMaxLength(100);

        builder.Property(m => m.Role)
            .HasMaxLength(100);
    }
}