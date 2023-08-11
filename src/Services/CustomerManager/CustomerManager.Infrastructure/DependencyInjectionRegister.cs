using CustomerManager.Infrastructure.Data;
using CustomerManager.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManager.Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CustomerManagerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<CustomerManagerDbContext>();

        // Repositories

        return services;
    }
}