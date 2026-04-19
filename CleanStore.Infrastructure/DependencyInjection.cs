using CleanStore.Application.AccountContext.Repositories.Abstractions;
using CleanStore.Application.SharedContext.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IAccountRepository, AccountRepository>();

        return services;
    }
}