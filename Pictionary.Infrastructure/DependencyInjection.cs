namespace Pictionary.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Pictionary.Application.Auth.Interfaces;
using Pictionary.Infrastructure.Persistence.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();

        return services;
    }
}