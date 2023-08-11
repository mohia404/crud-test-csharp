using CustomerManager.API.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CustomerManager.API;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ErrorOrProblemDetailsFactory>();

        services.AddControllers();

        return services;
    }
}