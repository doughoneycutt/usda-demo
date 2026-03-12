using CropYieldTracker.Application.Abstractions;
using CropYieldTracker.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CropYieldTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IFieldRepository, InMemoryFieldRepository>();
        return services;
    }
}
