using CustomerManager.API.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using Mapster;
using MapsterMapper;

namespace CustomerManager.API;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ErrorOrProblemDetailsFactory>();

        services.AddControllers();

        services.AddMappings();

        return services;
    }

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}