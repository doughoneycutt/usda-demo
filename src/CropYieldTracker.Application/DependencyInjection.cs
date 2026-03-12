using CropYieldTracker.Application.Abstractions;
using CropYieldTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CropYieldTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IFieldService, FieldService>();
        return services;
    }
}
