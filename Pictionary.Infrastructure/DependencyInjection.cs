namespace Pictionary.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pictionary.Application.Addresses.Interfaces;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Application.Orders.Interfaces;
using Pictionary.Infrastructure.Persistence;
using Pictionary.Infrastructure.Persistence.Repositories;
using Pictionary.Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        services.AddScoped<IPaymentService, StripeService>();

        services.AddDbContext<PictionaryDbContext>(
            options => options.UseSqlServer("Server=localhost;Database=Pictionary;TrustServerCertificate=Yes;User Id=sa;Password=Pictionary@Secret@password2023"));

        return services;
    }
}